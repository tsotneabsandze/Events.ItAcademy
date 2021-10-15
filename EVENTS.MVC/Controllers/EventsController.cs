using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.ActionFilters;
using Common.Constants;
using Common.Models;
using Common.Models.Event;
using Common.Models.EventList;
using EVENTS.MVC.ViewModels.CreateEvent;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EVENTS.MVC.Controllers
{
    public class EventsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index(int pageSize = 2, int pageIndex = 1)
        {
            var response =
                await Client.GetAsync($"{ApiConstants.BaseApiUrl}/Events?PageIndex={pageIndex}&PageSize={pageSize}");

            var dataString = await response.Content.ReadAsStringAsync();

            var paginatedResult = JsonConvert.DeserializeObject<PaginatedResult<EventListVm>>(dataString);

            var events = paginatedResult.Content;

            @ViewData["Title"] = "Event List";
            @ViewBag.PageCount =
                (int)Math.Ceiling((decimal)(paginatedResult.Count / paginatedResult.PageSize));

            return View("Events", events);
        }

        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> UserEvents()
        {
            var response =
                await Client.GetAsync($"{ApiConstants.BaseApiUrl}/Events/GetEventsByUser/{SessionService.GetId()}");

            var dataString = await response.Content.ReadAsStringAsync();

            var events = JsonConvert.DeserializeObject<EventListVm>(dataString);

            @ViewData["Title"] = "User Events";

            return View("Events", events);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var token = SessionService.GetToken();


            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var response = await Client.GetAsync(
                $"{ApiConstants.BaseApiUrl}/Events/{id}");

            if (!response.IsSuccessStatusCode) return View("NotFound", id);

            var stringContent = await response.Content.ReadAsStringAsync();
            var vm = JsonConvert.DeserializeObject<EventVm>(stringContent);


            if (vm.Photo != default)
                ViewBag.ImgSrc = ImageService.GetImage(vm.Photo);


            return View(vm);
        }


        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var token = SessionService.GetToken();

            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var stringContent = new StringContent(JsonConvert.SerializeObject(
                    new
                    {
                        vm.Title,
                        vm.Description,
                        vm.Starts,
                        vm.Ends,
                        Photo = ImageService.GetBytes(vm.Photo)
                    }),
                Encoding.UTF8, ApiConstants.ContentType);

            var response = await Client
                .PostAsync($"{ApiConstants.BaseApiUrl}/Events",
                    stringContent);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "invalid attempt");
                return View(vm);
            }

            var dataString = await response.Content.ReadAsStringAsync();

            var id = JsonConvert.DeserializeObject<int>(dataString);
            return RedirectToAction(nameof(Details), new { id });
        }


        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> Edit(int id)
        {
            var token = SessionService.GetToken();

            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var response = await Client.GetAsync(
                $"{ApiConstants.BaseApiUrl}/Events/{id}");

            if (!response.IsSuccessStatusCode) return View("NotFound", id);

            var stringContent = await response.Content.ReadAsStringAsync();
            var vm = JsonConvert.DeserializeObject<EventVm>(stringContent);


            return View(vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> Edit(EventVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            vm.UserId = SessionService.GetId();
            var token = SessionService.GetToken();

            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            var stringContent = new StringContent(JsonConvert.SerializeObject(vm),
                Encoding.UTF8, ApiConstants.ContentType);
            var response = await Client.PutAsync($"{ApiConstants.BaseApiUrl}/Events/{vm.Id}", stringContent);

            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Details), new { vm.Id });


            ModelState.AddModelError(string.Empty,
                "invalid  attempt");
            return View(vm);
        }
    }
}