using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string name);
    }
}
