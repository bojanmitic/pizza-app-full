using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Models
{
    public class MealSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
    }
}
