using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pizza_server.DTOs.Meal;
using pizza_server.DTOs.MealDTOs;
using pizza_server.Services.MealService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pizza_server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetMealDTO>>> GetMeals()
        {
            return Ok(await _mealService.GetMeals());
        } 

        [HttpPost]
        public async Task<ActionResult<List<GetMealDTO>>> AddMeal([FromForm] AddMealDTO addMeal)
        {
            return Ok(await _mealService.AddMeal(addMeal));
        }

        [HttpPut]
        public async Task<ActionResult<GetMealDTO>> UpdateMeal([FromForm] EditMealDTO updateMeal)
        {
            return Ok(await _mealService.EditMeal(updateMeal));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetMealDTO>>> Delete(int id)
        {
            return Ok(await _mealService.DeleteMeal(id));
        }
    }
}
