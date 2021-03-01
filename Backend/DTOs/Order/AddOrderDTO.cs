using pizza_server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pizza_server.DTOs
{
    public class AddOrderDTO
    {
        [Required]
        public ICollection<OrderItem> OrderItems { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
