using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pizza_server.Data;
using pizza_server.DTOs.MealSectionDTOs;
using pizza_server.Helpers;
using pizza_server.Models;
using pizza_server.Services.MealSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.MealSectionService
{
    public class MealSectionService : IMealSectionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MealSectionService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Add meal section
        /// </summary>
        /// <param name="addMealSection"></param>
        /// <returns>List of meals</returns>
        public async Task<ServiceResponse<List<GetMealSectionDTO>>> AddMealSection(AddMealSectionDTO addMealSection)
        {
            ServiceResponse<List<GetMealSectionDTO>> response = new ServiceResponse<List<GetMealSectionDTO>>();

            try
            {
                var adminRole = GetUser.GetUserRole(_httpContextAccessor);

                if (adminRole != "Admin")
                {
                    response.Success = false;
                    response.Message = "Not authorized.";
                    return response;
                }

                var mealSection = _mapper.Map<Models.MealSection>(addMealSection);

                _context.MealSections.Add(mealSection);
                await _context.SaveChangesAsync();

                var mealSectionsResponse = await _context.MealSections
                     .Include(x => x.Meals)
                    .Select(s => _mapper.Map<GetMealSectionDTO>(s)).ToListAsync();

                response.Data = mealSectionsResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Deletes meal section
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of all meal sections</returns>
        public async Task<ServiceResponse<List<GetMealSectionDTO>>> DeleteMealSection(int id)
        {
            ServiceResponse<List<GetMealSectionDTO>> response = new ServiceResponse<List<GetMealSectionDTO>>();

            try
            {
                var adminRole = GetUser.GetUserRole(_httpContextAccessor);

                if (adminRole != "Admin")
                {
                    response.Success = false;
                    response.Message = "Not authorized.";
                    return response;
                }

                var mealSectionToRemove = await _context.MealSections.FirstOrDefaultAsync(m => m.Id == id);

                if (mealSectionToRemove == null)
                {
                    response.Success = false;
                    response.Message = "Meal not found.";
                    return response;
                }

                _context.MealSections.Remove(mealSectionToRemove);
                await _context.SaveChangesAsync();

                var mealSectionsResponse = await _context.MealSections
                    .Include(x => x.Meals)
                     .Include(x => x.Meals)
                    .Select(s => _mapper.Map<GetMealSectionDTO>(s)).ToListAsync();

                response.Data = mealSectionsResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edites meal section
        /// </summary>
        /// <param name="editMealSection"></param>
        /// <returns>Edited meal section</returns>
        public async Task<ServiceResponse<GetMealSectionDTO>> EditMealSection(EditMealSectionDTO editMealSection)
        {
            ServiceResponse<GetMealSectionDTO> response = new ServiceResponse<GetMealSectionDTO>();

            try
            {
                var adminRole = GetUser.GetUserRole(_httpContextAccessor);

                if (adminRole != "Admin")
                {
                    response.Success = false;
                    response.Message = "Not authorized.";
                    return response;
                }

                var mealSectionDb = await _context.MealSections.FirstOrDefaultAsync(m => m.Id == editMealSection.Id);


                if (mealSectionDb == null)
                {
                    response.Success = false;
                    response.Message = "Meal not found.";
                    return response;
                }


                mealSectionDb = _mapper.Map(editMealSection, mealSectionDb);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetMealSectionDTO>(mealSectionDb);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Gets all meal sections
        /// </summary>
        /// <returns>All meal sections</returns>
        public async Task<ServiceResponse<List<GetMealSectionDTO>>> GetAllSections()
        {
            ServiceResponse<List<GetMealSectionDTO>> response = new ServiceResponse<List<GetMealSectionDTO>>();

            try
            {
                var adminRole = GetUser.GetUserRole(_httpContextAccessor);

                if (adminRole != "Admin")
                {
                    response.Success = false;
                    response.Message = "Not authorized.";
                    return response;
                }

                var mealSectionsResponse = await _context.MealSections
                    .Include(x => x.Meals)
                    .Select(s => _mapper.Map<GetMealSectionDTO>(s)).ToListAsync();

                response.Data = mealSectionsResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
