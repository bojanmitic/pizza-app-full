using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.FileStorageService
{
    public interface IFileStorageService
    {
        Task<ServiceResponse<string>> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType);
        Task DeleteFile(string fileRoute, string containerName);
        Task<ServiceResponse<string>> SaveFile(byte[] content, string extension, string containerName, string contentType);
    }
}
