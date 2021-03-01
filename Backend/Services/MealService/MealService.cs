using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pizza_server.Data;
using pizza_server.DTOs.Meal;
using pizza_server.DTOs.MealDTOs;
using pizza_server.Models;
using pizza_server.Services.FileStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "meal";

        public MealService(
            DataContext context, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService fileStorageService
            )
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _fileStorageService = fileStorageService;
        }
        /// <summary>
        /// Adds new meal
        /// </summary>
        /// <param name="addMeal"></param>
        /// <returns>List of all meals</returns>
        public async Task<ServiceResponse<List<GetMealDTO>>> AddMeal(AddMealDTO addMeal)
        {

            ServiceResponse<List<GetMealDTO>> response = new ServiceResponse<List<GetMealDTO>>();

            try
            {
                var meal = _mapper.Map<Meal>(addMeal);

                if(addMeal.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await addMeal.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(addMeal.Image.FileName);
                        ServiceResponse<string> aureImagUrlResponse;

                        try
                        {
                            aureImagUrlResponse = await _fileStorageService.SaveFile(content, extension, containerName, addMeal.Image.ContentType);
                           meal.ImageUrl = aureImagUrlResponse.Data;
                        }
                        catch (Exception ex)
                        {

                            response.Success = false;
                            response.Message = ex.Message;
                        }
                    }
                }

                await _context.AddAsync(meal);
                await _context.SaveChangesAsync();
                var allMeals = await _context.Meals
                    .Select(m => _mapper.Map<GetMealDTO>(m))
                    .ToListAsync();
                response.Data = allMeals;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Deletes meal
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of all meals</returns>
        public async Task<ServiceResponse<List<GetMealDTO>>> DeleteMeal(int id)
        {
            ServiceResponse<List<GetMealDTO>> response = new ServiceResponse<List<GetMealDTO>>();
            try
            {
                var dbMeal = await _context.Meals.FirstOrDefaultAsync(x => x.Id == id);

                if(dbMeal == null)
                {
                    response.Success = false;
                    response.Message = "Meal not found.";
                    return response;
                }


                _context.Remove(dbMeal);
                await _context.SaveChangesAsync();

                await _fileStorageService.DeleteFile(dbMeal.ImageUrl, containerName);

                List<Meal> dbMeals = await _context.Meals.ToListAsync();
                response.Data = _mapper.Map<List<GetMealDTO>>(dbMeals);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edites the meal
        /// </summary>
        /// <param name="editMeal"></param>
        /// <returns>Edited meal</returns>
        public async Task<ServiceResponse<GetMealDTO>> EditMeal(EditMealDTO editMeal)
        {
            ServiceResponse<GetMealDTO> response = new ServiceResponse<GetMealDTO>();

            try
            {
                var mealDb = await _context.Meals.FirstOrDefaultAsync(x => x.Id == editMeal.Id);

                if(mealDb == null)
                {
                    response.Success = false;
                    response.Message = "Meal not found.";
                    return response;
                }

                mealDb = _mapper.Map(editMeal, mealDb);

                if (editMeal.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await editMeal.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(editMeal.Image.FileName);
                     
                        ServiceResponse<string> aureImagUrlResponse;

                        try
                        {
                            aureImagUrlResponse = await _fileStorageService.EditFile(content, extension, containerName,
                                                              mealDb.ImageUrl, editMeal.Image.ContentType);
                            mealDb.ImageUrl = aureImagUrlResponse.Data;
                        }
                        catch (Exception ex)
                        {

                            response.Success = false;
                            response.Message = ex.Message;
                        }
                    }
                }

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetMealDTO>(mealDb);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Gets all meals
        /// </summary>
        /// <returns>All meals</returns>
        public async Task<ServiceResponse<List<GetMealDTO>>> GetMeals()
        {
            ServiceResponse<List<GetMealDTO>> response = new ServiceResponse<List<GetMealDTO>>();

            try
            {
                List<Meal> dbMeals = await _context.Meals
                    .ToListAsync();
                response.Data = _mapper.Map<List<GetMealDTO>>(dbMeals);
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
