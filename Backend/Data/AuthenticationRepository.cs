using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using pizza_server.DTOs.User;
using pizza_server.Helpers;
using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace pizza_server.Data
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationRepository(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<ResponseLoginDTO>> Login(string email, string password)
        {
            ServiceResponse<ResponseLoginDTO> response = new ServiceResponse<ResponseLoginDTO>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            try
            {
                if(user == null)
                {
                    response.Success = false;
                    response.Message = "Something went wrong";
                }

                else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Something went wrong";
                }
                else
                {
                    var userToken = CreateToken(user);

                    response.Data = new ResponseLoginDTO { 
                        Token = userToken.Token,
                        UserRole = user.Role,
                        ExpiresAt = userToken.ExpiresAt
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {

                if (await UserExists(user.Email))
                {
                    response.Success = false;
                    response.Message = "User Already Exists.";

                    return response;
                }

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                response.Data = "You are succesfully registered. You can login with your credentials now.";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private UserToken CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.StreetAddress, user.StreetAddress),
                new Claim(ClaimTypes.PostalCode, user.ZipCode.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expiration = DateTime.UtcNow.AddHours(1);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserToken()
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresAt = expiration
            };
        }

        public async Task<ServiceResponse<UserToken>> RenewToken()
        {
            ServiceResponse<UserToken> response = new ServiceResponse<UserToken>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUser.GetUserId(_httpContextAccessor));

                if(user == null)
                {
                    response.Success = false;
                    response.Message = "Something went wrong.";
                    return response;
                }

                var userToken = CreateToken(user);

                response.Data = new UserToken
                {
                    Token = userToken.Token,
                    ExpiresAt = userToken.ExpiresAt
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> ForgotPassword(ForgotPassword email)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.Email.ToLower());


                response.Message = "Check your email.";

                if(user != null)
                {
                    var client = new HttpClient();
                    var token = CreateToken(user).Token;
                    //TO DO: change token to link with query string as token 
                    var url = $"https://pizza-app-portal.azurewebsites.net/auth/resetPassword?token={token}";
                    var jsonResult = JsonConvert.SerializeObject(new { url, email = email.Email });
                    StringContent content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
                    var sendEmailUrl = _configuration.GetSection("AppSettings:SentEmailLogicApp").Value;

                    response.Message = "Check your email.";
                   var logicAppResponse = await client.PostAsync(sendEmailUrl, content);
                }

            }
            catch (Exception)
            {
                response.Success = true;
                response.Message = "Check Your email.";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> UpdatePassword(ResetPassword resetPassword)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var jwt = resetPassword.Token;
            if (String.IsNullOrWhiteSpace(jwt))
            {
                response.Success = false;
                response.Message = "Invalid token";
                return response;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                IEnumerable<Claim> claim = token.Claims;

                var tokenExpiration =
                    claim
                .Where(x => x.Type == "exp")
                .FirstOrDefault();

                var millisecondsNow = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var tokenExpirationMilliseconds = long.Parse(tokenExpiration.Value) * 1000;

                if ( millisecondsNow > tokenExpirationMilliseconds)
                {
                    response.Success = false;
                    response.Message = "Link has expired, please repeat process again.";
                    return response;
                }

                var userIdClaim = claim
                .Where(x => x.Type == "email")
                .FirstOrDefault();

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == userIdClaim.Value.ToLower());

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Something went wrong.";
                    return response;
                }

                CreatePasswordHash(resetPassword.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.SaveChangesAsync();

                response.Data = "Password successfully changed.";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
