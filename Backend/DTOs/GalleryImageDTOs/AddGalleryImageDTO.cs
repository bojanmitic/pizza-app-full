using Microsoft.AspNetCore.Http;
using pizza_server.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.DTOs.GalleryImageDTOs
{
    public class AddGalleryImageDTO
    {
        [FileSizeValidator(20)]
        [ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile Image { get; set; }
    }
}
