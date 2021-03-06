﻿namespace ENews.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadPictureAsync(IFormFile pictureFile)
        {
            byte[] destinationImage;
            using (var memoryStream = new MemoryStream())
            {
                await pictureFile.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            UploadResult uploadResult = null;

            using (var destitanionStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, destitanionStream),
                };
                uploadResult = await this.cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
