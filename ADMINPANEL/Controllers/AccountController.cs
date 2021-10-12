using System.Threading.Tasks;
using Common.Models;
using Common.Models.Login;
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
        public async Task<IActionResult> Login(LoginVm user)
        {
            if (!ModelState.IsValid) return View(user);

            return Redirect("www.google.com");
        }
    }
}