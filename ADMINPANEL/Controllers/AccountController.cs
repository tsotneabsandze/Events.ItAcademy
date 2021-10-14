using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Constants;
using Common.Models;
using Common.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMINPANEL.Controllers
{
    public class AccountController : BaseController
    {
        
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
            

            var response = await Client
                .PostAsync($"{ApiConstants.BaseApiUrl}/Account/signIn",
                    stringContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var data =
                    JsonConvert.DeserializeObject<AuthResponse>(content);

                SessionService.SetToken(data.Token);
                SessionService.SetMail(data.Email);

                return RedirectToAction("Index", "Events");
            }

            ModelState.AddModelError(string.Empty, "invalid login attempt");
            return View(vm);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("email");
            return RedirectToAction(nameof(Login), "Account");
        }
    }
}