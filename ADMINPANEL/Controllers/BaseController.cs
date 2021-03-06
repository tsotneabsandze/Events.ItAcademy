using System.Net.Http;
using Common.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ADMINPANEL.Controllers
{
    public class BaseController : Controller
    {
        private static  HttpClient _client;
        private  ISessionService _sessionService;

        protected static HttpClient Client =>
            _client ??= new HttpClient();

        protected ISessionService SessionService
            => _sessionService ??= HttpContext.RequestServices.GetService<ISessionService>();
    }
}