using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IUploadService
    {
        Task<(string Url, string PublicId)> UploadImageAsync(IFormFile imageFile, string name, string folder);

        Task DestroyImageAsync(string publicId);

        Task<(string Url, string PublicId)> UploadAudioFileAsync(IFormFile audioFile, string name, string folder);

        Task DestroyAudioFileAsync(string publicId);
    }
}
