using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pizza_server.DTOs.User;
using pizza_server.Models;
using pizza_server.Services.RolesService;
using System.Threading.Tasks;

namespace pizza_server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRolesService _roleService;

        public RoleController(IRolesService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("ChangeUserRole")]
        public async Task<ActionResult<GetChangeUserRoleDTO>> ChangeUserRole([FromBody] AddChangeRoleDTO addRoleChange)
        {
            return Ok(await _roleService.ChangeUserRole(addRoleChange.Id, addRoleChange.NewRole));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            return Ok(await _roleService.DeleteUser(id));
        }

        [HttpGet]
        public async Task<ActionResult<GetChangeUserRoleDTO>> GetUserByEmail(string email)
        {
            ServiceResponse<GetChangeUserRoleDTO> response = await _roleService.GetUserByEmail(email);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
     }
}
