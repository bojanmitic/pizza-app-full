using AutoMapper;
using pizza_server.DTOs;
using pizza_server.DTOs.GalleryImageDTOs;
using pizza_server.DTOs.Meal;
using pizza_server.DTOs.MealSectionDTOs;
using pizza_server.DTOs.User;
using pizza_server.Models;

namespace pizza_server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddMealDTO, Meal>()
                .ForMember(x => x.ImageUrl, options => options.Ignore());
            CreateMap<Meal, GetMealDTO>();
            CreateMap<User, GetChangeUserRoleDTO>();
            CreateMap<AddMealSectionDTO, MealSection>();
            CreateMap<EditMealSectionDTO, MealSection>();
            CreateMap<MealSection, GetMealSectionDTO>();
            CreateMap<Order, GetOrderDTO>();
            CreateMap<AddOrderDTO, Order>();
            CreateMap<AddGalleryImageDTO, GalleryImage>();
        }
    }
}
