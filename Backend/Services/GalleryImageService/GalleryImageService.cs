using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pizza_server.Data;
using pizza_server.DTOs.GalleryImageDTOs;
using pizza_server.Models;
using pizza_server.Services.FileStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.GalleryImageService
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly string _containerName = "galleryimages";
        private readonly DataContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public GalleryImageService(DataContext context, IFileStorageService fileStorageService, IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GalleryImage>>> DeleteImage(int id)
        {
            ServiceResponse<List<GalleryImage>> response = new ServiceResponse<List<GalleryImage>>();

            try
            {
                var image = await _context.GalleryImages.FirstOrDefaultAsync(i => i.Id == id);

                if(image == null)
                {
                    response.Success = false;
                    response.Message = "Image not found.";
                    return response;
                }

                 _context.GalleryImages.Remove(image);
                 await _context.SaveChangesAsync();

                await _fileStorageService.DeleteFile(image.ImageUrl, _containerName);

                response.Data = await _context.GalleryImages.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GalleryImage>>> GetAll()
        {
            ServiceResponse<List<GalleryImage>> response = new ServiceResponse<List<GalleryImage>>();

            try
            {
                response.Data = await _context.GalleryImages.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GalleryImage>> UploadImage(AddGalleryImageDTO addImageDTO)
        {
            ServiceResponse<GalleryImage> response = new ServiceResponse<GalleryImage>();

            try
            {
                var galleryImage = _mapper.Map<GalleryImage>(addImageDTO);

                if (addImageDTO.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await addImageDTO.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(addImageDTO.Image.FileName);
                        ServiceResponse<string> aureImagUrlResponse;

                        try
                        {
                            aureImagUrlResponse = await _fileStorageService.SaveFile(content, extension, _containerName, addImageDTO.Image.ContentType);
                            galleryImage.ImageUrl = aureImagUrlResponse.Data;
                        }
                        catch (Exception ex)
                        {
                            response.Success = false;
                            response.Message = ex.Message;
                        }
                    }
                }

                await _context.GalleryImages.AddAsync(galleryImage);
                await _context.SaveChangesAsync();
                response.Data = galleryImage;
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
