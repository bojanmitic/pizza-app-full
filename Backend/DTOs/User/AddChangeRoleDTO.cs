using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs.User
{
    public class AddChangeRoleDTO
    {
        public int Id { get; set; }
        public string NewRole { get; set; }
    }
}
