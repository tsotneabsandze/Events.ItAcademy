using Microsoft.AspNetCore.Http;

namespace Common.Services.Abstractions
{
    public interface IImageService
    {
        byte[] GetBytes(IFormFile img);
        string GetImage(byte[] array);
    }
}