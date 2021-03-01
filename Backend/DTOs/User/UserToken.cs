using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs.User
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
