using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pizza_server.DTOs.MealSectionDTOs;
using pizza_server.Services.MealSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class MealSectionController : ControllerBase
    {
        private readonly IMealSectionService _mealSectionService;

        public MealSectionController(IMealSectionService mealSectionService)
        {
            _mealSectionService = mealSectionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetMealSectionDTO>>> GetAll()
        {
            return Ok(await _mealSectionService.GetAllSections());
        } 

        [HttpPost]
        public async Task<ActionResult<List<GetMealSectionDTO>>> AddSection([FromBody] AddMealSectionDTO addMealSection)
        {
            return Ok(await _mealSectionService.AddMealSection(addMealSection));
        }

        [HttpPut]
        public async Task<ActionResult<GetMealSectionDTO>> UpdateSection([FromBody] EditMealSectionDTO updateMealSection)
        {
            return Ok(await _mealSectionService.EditMealSection(updateMealSection));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetMealSectionDTO>>> DeleteSection(int id)
        {
            return Ok(await _mealSectionService.DeleteMealSection(id));
        }

    }
}
