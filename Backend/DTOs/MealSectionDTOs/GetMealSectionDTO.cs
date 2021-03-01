using System.Collections.Generic;

namespace pizza_server.DTOs.MealSectionDTOs
{
    public class GetMealSectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Models.Meal> Meals { get; set; }
    }
}
