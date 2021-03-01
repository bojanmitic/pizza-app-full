using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.FileStorageService
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _connectionString;

        public FileStorageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorageConnection");
        }
        /// <summary>
        /// Deletes file from azure blob storage
        /// </summary>
        /// <param name="fileRoute"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task DeleteFile(string fileRoute, string containerName)
        {
            try
            {
                if (fileRoute != null)
                {
                    var account = CloudStorageAccount.Parse(_connectionString);
                    var client = account.CreateCloudBlobClient();
                    var container = client.GetContainerReference(containerName);

                    var blobName = Path.GetFileName(fileRoute);
                    var blob = container.GetBlobReference(blobName);

                    await blob.DeleteIfExistsAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Edit file by removing old and saving new on azure blob storeage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="extension"></param>
        /// <param name="containerName"></param>
        /// <param name="fileRoute"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await DeleteFile(fileRoute, containerName);
                var imageUrlResponse = await SaveFile(content, extension, containerName, contentType);
                response.Data = imageUrlResponse.Data;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        /// <summary>
        /// Saves file on Azure blob storage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="extension"></param>
        /// <param name="containerName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> SaveFile(byte[] content, string extension, string containerName, string contentType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var account = CloudStorageAccount.Parse(_connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(containerName);
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

                var fileName = $"{Guid.NewGuid()}{extension}";
                var blob = container.GetBlockBlobReference(fileName);
                await blob.UploadFromByteArrayAsync(content, 0, content.Length);
                blob.Properties.ContentType = contentType;
                await blob.SetPropertiesAsync();

                response.Data = blob.Uri.ToString();

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
