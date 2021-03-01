using Microsoft.AspNetCore.Http;
using pizza_server.Models;
using pizza_server.Validations;

namespace pizza_server.DTOs.Meal
{
    public class AddMealDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [FileSizeValidator(20)]
        [ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile Image { get; set; }
        public int MealSectionId { get; set; }
    }
}
