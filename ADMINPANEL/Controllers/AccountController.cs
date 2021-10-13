using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Constants;
using Common.Models;
using Common.Models.Login;
using Common.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMINPANEL.Controllers
{
    public class AccountController : Controller
    {
        
        private static  HttpClient _client;
        private readonly ISessionService _sessionService;

        public AccountController(ISessionService sessionService)
        {
            _sessionService = sessionService;
            _client = new HttpClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var stringContent = new StringContent(JsonConvert.SerializeObject(vm),
                Encoding.UTF8, ApiConstants.ContentType);
            

            var response = await _client
                .PostAsync($"{ApiConstants.BaseApiUrl}/Account/signIn",
                    stringContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var data =
                    JsonConvert.DeserializeObject<AuthResponse>(content);

                _sessionService.SetToken(data.Token);
                _sessionService.SetMail(data.Email);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "invalid login attempt");
            return View(vm);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login), "Account");
        }
    }
}