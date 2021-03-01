using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs.User
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
