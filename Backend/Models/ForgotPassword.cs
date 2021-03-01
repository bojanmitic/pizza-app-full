using System.ComponentModel.DataAnnotations;

namespace pizza_server.Models
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
