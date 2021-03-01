using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pizza_server.Data;
using pizza_server.DTOs.User;
using pizza_server.Helpers;
using pizza_server.Models;
using System;
using System.Threading.Tasks;

namespace pizza_server.Services.RolesService
{
    public class RolesService : IRolesService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public RolesService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        /// <summary>
        /// Changes user role, only admin can do that
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newUserRole"></param>
        /// <returns>User</returns>
        public async Task<ServiceResponse<GetChangeUserRoleDTO>> ChangeUserRole(int id, string newUserRole)
        {
            ServiceResponse<GetChangeUserRoleDTO> response = new ServiceResponse<GetChangeUserRoleDTO>();

            try
            {
                var adminRole =  GetUser.GetUserRole(_httpContextAccessor);

                if(adminRole != "Admin")
                {
                    response.Success = false;
                    response.Message = "Not authorized.";
                    return response;
                }

                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if(dbUser == null)
                {
                    response.Success = false;
                    response.Message = "User Not Found";
                    return response;
                }

                if(newUserRole == "Operator" || newUserRole == "Admin" || newUserRole =="User")
                {
                    dbUser.Role = newUserRole;
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetChangeUserRoleDTO>(dbUser);

                    return response;
                }

                    response.Success = false;
                    response.Message = "User role not supported";
                    return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Deletes user from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>String</returns>
        public async Task<ServiceResponse<string>> DeleteUser(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                var adminRole = GetUser.GetUserRole(_httpContextAccessor);

                if (adminRole != "Admin")
                {
                    response.Success = false;
                    response.Message = "Not authorized.";
                    return response;
                }

                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (dbUser == null)
                {
                    response.Success = false;
                    response.Message = "User Not Found";
                    return response;
                }

                _context.Users.Remove(dbUser);
                await _context.SaveChangesAsync();

                response.Data = "User succesfully removed.";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
        
        public async Task<ServiceResponse<GetChangeUserRoleDTO>> GetUserByEmail(string email)
        {
            ServiceResponse<GetChangeUserRoleDTO> response = new ServiceResponse<GetChangeUserRoleDTO>();

            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

                if(dbUser == null)
                {
                    response.Success = false;
                    response.Message = "Something went wrong.";
                    return response;
                }

                response.Data = _mapper.Map<GetChangeUserRoleDTO>(dbUser);
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
