using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pizza_server.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        public int UserId { get; set; }
        [Required]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        public DateTime OrderRecivedAt { get; set; }
        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsDelivered { get; set; } = false;
        public bool OnTheWay { get; set; } = false;
        public OrderState OrderState { get; set; } = OrderState.WaitingForPreparation;

        public Order()
        {
            OrderRecivedAt = DateTime.UtcNow;
        }
    }
}
