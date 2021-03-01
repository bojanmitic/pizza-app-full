using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pizza_server.DTOs.GalleryImageDTOs;
using pizza_server.Models;
using pizza_server.Services.GalleryImageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Controllers
{  
    [ApiController]
    [Route("[controller]")]
    public class GalleryImageController : ControllerBase
    {
        private readonly IGalleryImageService _galleryImageService;

        public GalleryImageController(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GalleryImage>>> GetAll()
        {
            return Ok(await _galleryImageService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GalleryImage>>> AddImage([FromForm] AddGalleryImageDTO addImage)
        {
            ServiceResponse<GalleryImage> response = await _galleryImageService.UploadImage(addImage);

            if(response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GalleryImage>>>> DeleteImage(int id)
        {
            ServiceResponse<List<GalleryImage>> response = await _galleryImageService.DeleteImage(id);

            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
