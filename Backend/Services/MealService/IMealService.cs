using Microsoft.AspNetCore.Mvc;
using pizza_server.DTOs.Meal;
using pizza_server.DTOs.MealDTOs;
using pizza_server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pizza_server.Services.MealService
{
    public interface IMealService
    {
        Task<ServiceResponse<List<GetMealDTO>>> GetMeals();
        Task<ServiceResponse<List<GetMealDTO>>> AddMeal(AddMealDTO meal);
        Task<ServiceResponse<GetMealDTO>> EditMeal(EditMealDTO meal);
        Task<ServiceResponse<List<GetMealDTO>>> DeleteMeal(int id);
    }
}
