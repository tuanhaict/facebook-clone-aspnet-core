using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Fadebook.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Uploads
{
    public class CloudinaryService : IUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        public CloudinaryService(IConfiguration configuration)
        {
            var cloudinaryConfig = configuration.GetSection("Cloudinary");
            _configuration = configuration;
            var account = new Account
                (
                    cloudinaryConfig.GetValue<string>("CloudName"),
                    cloudinaryConfig.GetValue<string>("ApiKey"),
                    cloudinaryConfig.GetValue<string>("ApiSecret")
                );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImage(IFormFile file, string type)
        {
            if (file == null || file.Length == 0)
                return null;
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = $"{_configuration.GetSection("Cloudinary")["Folder"]}/{type}s"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }
    }
}
