using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pizza_server.Data;
using pizza_server.DTOs.User;
using pizza_server.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pizza_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthController(IAuthenticationRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegisterDTO user)
        {
            ServiceResponse<string> response = await _authRepo.Register(
                    new User
                    {
                        Email = user.Email,
                        Name = user.Name,
                        ZipCode = user.ZipCode,
                        StreetAddress = user.StreetAddress

                    }, user.Password
                );

            if (!response.Success)
            {
                return BadRequest(response);
            };

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<ResponseLoginDTO>>> Login(UserLoginDTO user)
        {
            ServiceResponse<ResponseLoginDTO> response = await _authRepo.Login(user.Email, user.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost("RenewToken")]
        public async Task<ActionResult<ServiceResponse<UserToken>>> RenewToken()
        {
            ServiceResponse<UserToken> response = await _authRepo.RenewToken();

            if (!response.Success)
            {
                return BadRequest(response);
            };

            return Ok(response);
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPassword email)
        {
           return Ok(await _authRepo.ForgotPassword(email));
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            return Ok(await _authRepo.UpdatePassword(resetPassword));
        }
    }
}
