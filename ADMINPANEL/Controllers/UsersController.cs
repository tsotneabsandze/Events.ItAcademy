using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ADMINPANEL.ViewModels.EditUser;
using Common.ActionFilters;
using Common.Constants;
using Common.Models.Register;
using Common.Models.User;
using Common.Models.UserList;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMINPANEL.Controllers
{
    public class UsersController : BaseController
    {

        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> Index()
        {
            var token = SessionService.GetToken();

            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var response =
                await Client.GetAsync($"{ApiConstants.BaseApiUrl}/Account");


            var dataString = await response.Content.ReadAsStringAsync();
            var users =
                JsonConvert.DeserializeObject<UserListVm>(dataString);

            return View("ListUsers",users);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var token = SessionService.GetToken();
            
            
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var response = await Client.GetAsync($"{ApiConstants.BaseApiUrl}/Account/{email}");
            if (!response.IsSuccessStatusCode) return View("NotFound", email);

            var stringContent = await response.Content.ReadAsStringAsync();
            var userToDelete = JsonConvert.DeserializeObject<UserVm>(stringContent);

            return View(userToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string email)
        {
            var token = SessionService.GetToken();
            
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            await Client.DeleteAsync(
                $"{ApiConstants.BaseApiUrl}/Account/{email}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var token = SessionService.GetToken();
            
            
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var response = await Client.GetAsync($"{ApiConstants.BaseApiUrl}/Account/GetUserById/{id}");
            if (!response.IsSuccessStatusCode) return View("NotFound", id);

            var stringContent = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserVm>(stringContent);

            var vm = new EditUserVm
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVm vm)
        {
            if (ModelState.IsValid)
            {
                var token = SessionService.GetToken();

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiConstants.Scheme, token);

                var stringContent = new StringContent(JsonConvert.SerializeObject(vm),
                    Encoding.UTF8, ApiConstants.ContentType);

                await Client.PutAsync($"{ApiConstants.BaseApiUrl}/Account/{vm.Id}", stringContent);

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty,
                "invalid update attempt");
            return View(vm);
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var stringContent = new StringContent(JsonConvert.SerializeObject(vm),
                Encoding.UTF8, ApiConstants.ContentType);
            

            var response = await Client
                .PostAsync($"{ApiConstants.BaseApiUrl}/Account/Register",
                    stringContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "invalid attempt");
            return View(vm);
        }
    }
}