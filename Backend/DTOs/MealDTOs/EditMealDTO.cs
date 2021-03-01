using Microsoft.AspNetCore.Http;
using pizza_server.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs.MealDTOs
{
    public class EditMealDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [FileSizeValidator(20)]
        [ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile Image { get; set; }
        public int MealSectionId { get; set; }
    }
}
