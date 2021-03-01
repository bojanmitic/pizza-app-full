using pizza_server.DTOs.GalleryImageDTOs;
using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.GalleryImageService
{
    public interface IGalleryImageService
    {
        Task<ServiceResponse<List<GalleryImage>>> GetAll();
        Task<ServiceResponse<GalleryImage>> UploadImage(AddGalleryImageDTO addImageDTO);
        Task<ServiceResponse<List<GalleryImage>>> DeleteImage(int id);
    }
}
