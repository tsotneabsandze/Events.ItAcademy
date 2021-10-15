using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Common.Services.Abstractions;


namespace Common.Services.Implementations
{
    public class ImageService : IImageService
    {
        public byte[] GetBytes(IFormFile img)
        {
            using var ms = new MemoryStream();
            img.CopyTo(ms);
            return ms.ToArray();
        }

        public string GetImage(byte[] array)
        {
            var base64 = Convert.ToBase64String(array);
            return $"data:image;base64,{base64}";
        }
    }
}