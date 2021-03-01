using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public virtual Meal Meal { get; set; }
        public int MealId { get; set; }
        public int Quantity { get; set; }
    }
}
