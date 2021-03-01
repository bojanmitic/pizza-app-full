using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pizza_server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
