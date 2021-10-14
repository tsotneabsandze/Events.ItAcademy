using System.Net.Http;
using Common.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EVENTS.MVC.Controllers
{
    public class BaseController : Controller
    {
        private static  HttpClient _client;
        private  ISessionService _sessionService;
        private IImageService _imageService;

        protected HttpClient Client =>
            _client ??= new HttpClient();

        protected ISessionService SessionService
            => _sessionService ??= HttpContext.RequestServices.GetService<ISessionService>();

        protected IImageService ImageService
            => _imageService ??= HttpContext.RequestServices.GetService<IImageService>();
    }
}