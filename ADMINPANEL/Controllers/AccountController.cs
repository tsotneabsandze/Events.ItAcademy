using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Constants;
using Common.Models;
using Common.Models.Login;
using Common.Models.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMINPANEL.Controllers
{
    public class AccountController : Controller
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

            var client = new HttpClient();

            var response = await client
                .PostAsync($"{ApiConstants.BaseApiUrl}/Account/signIn",
                    stringContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var data =
                    JsonConvert.DeserializeObject<AuthResponse>(content);

                HttpContext.Session.SetString("token", data.Token);
                HttpContext.Session.SetString("mail", data.Email);

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