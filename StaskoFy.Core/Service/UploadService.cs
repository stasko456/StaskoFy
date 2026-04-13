using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StaskoFy.Core.IService;
using StaskoFy.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary cloudinary;

        public UploadService(IOptions<CloudinarySettings> options)
        {
            var settings = options.Value;

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            );

            this.cloudinary = new Cloudinary(account);
        }

        public async Task<(string Url, string PublicId)> UploadImageAsync(IFormFile imageFile, string name, string folder)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("File is empty!");
            }

            var allowedTypes = new[] { "image/jpg", "image/jpeg", "image/png" };
            if (!allowedTypes.Contains(imageFile.ContentType))
            {
                throw new ArgumentException("Invalid file type");
            }

            var cleanName = Regex.Replace(name, @"[^a-zA-Z0-9_-]", "");
            using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream),
                Folder = folder,
                PublicId = $"{Guid.NewGuid()}_{cleanName}",
                Overwrite = true
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }

        public async Task DestroyImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                return;
            }

            var deletionParams = new DeletionParams(publicId);

            var result = await cloudinary.DestroyAsync(deletionParams);
        }

        public async Task<(string Url, string PublicId)> UploadAudioFileAsync(IFormFile audioFile, string name, string folder)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                throw new ArgumentException("File is empty!");
            }

            var allowedTypes = new[] { "audio/mpeg", "audio/wav", "audio/ogg", "audio/x-m4a", "audio/mp3" };
            if (!allowedTypes.Contains(audioFile.ContentType))
            {
                throw new ArgumentException("Invalid file type");
            }

            var cleanName = Regex.Replace(name, @"[^a-zA-Z0-9_-]", "");
            using var stream = audioFile.OpenReadStream();

            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(name, stream),
                Folder = folder,
                PublicId = $"{Guid.NewGuid()}_{cleanName}",
                Overwrite = true
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }

        public async Task DestroyAudioFileAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                return;
            }

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Video,
            };

            var result = await cloudinary.DestroyAsync(deletionParams);
        }
    }
}
