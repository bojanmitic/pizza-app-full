using pizza_server.DTOs.MealSectionDTOs;
using pizza_server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pizza_server.Services.MealSection
{
    public interface IMealSectionService
    {
        Task<ServiceResponse<List<GetMealSectionDTO>>> GetAllSections();
        Task<ServiceResponse<List<GetMealSectionDTO>>> AddMealSection(AddMealSectionDTO addMealSection);
        Task<ServiceResponse<GetMealSectionDTO>> EditMealSection(EditMealSectionDTO editMealSection);
        Task<ServiceResponse<List<GetMealSectionDTO>>> DeleteMealSection(int id);
    }
}
