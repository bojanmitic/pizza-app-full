using pizza_server.DTOs.User;
using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.RolesService
{
    public interface IRolesService
    {
        Task<ServiceResponse<GetChangeUserRoleDTO>> ChangeUserRole(int id, string newUserRole);
        Task<ServiceResponse<string>> DeleteUser(int id);
        Task<ServiceResponse<GetChangeUserRoleDTO>> GetUserByEmail(string email);
    }
}
