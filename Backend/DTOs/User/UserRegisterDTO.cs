using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs.User
{
    public class UserRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public int ZipCode { get; set; }
    }
}
