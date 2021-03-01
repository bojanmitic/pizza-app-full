using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderRecivedAt { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsDelivered { get; set; } = false;
        public bool OnTheWay { get; set; } = false;
        public OrderState OrderState { get; set; }
    }
}
