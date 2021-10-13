using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ADMINPANEL.ViewModels.Approval;
using Common.ActionFilters;
using Common.Constants;
using Common.Models.Event;
using Common.Models.EventList;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMINPANEL.Controllers
{
    public class EventsController : BaseController
    {
        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> Index()
        {
            var events = await GetEvents();
            @ViewData["Title"] = "Event List";

            return View("Events", events);
        }

        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> UnapprovedEvents()
        {
            var events = await GetEvents("GetUnapprovedEvents");

            @ViewData["Title"] = "Unapproved Event List";
            return View("Events", events);
        }

        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> ApprovedEvents()
        {
            var events = await GetEvents("GetApprovedEvents");

            @ViewData["Title"] = "Approved Event List";
            return View("Events", events);
        }

        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
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

            return View(vm);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var token = SessionService.GetToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiConstants.Scheme, token);

            await Client.DeleteAsync(
                $"{ApiConstants.BaseApiUrl}/Events/{id}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> Approve(int id)
        {
            var response = await Client.GetAsync(
                $"{ApiConstants.BaseApiUrl}/Events/{id}");

            var content = await response.Content.ReadAsStringAsync();
            var evnt = JsonConvert.DeserializeObject<EventVm>(content);

            ViewBag.Starts = evnt.Starts;
            ViewBag.Ends = evnt.Ends;

            return !response.IsSuccessStatusCode ? View("NotFound", id) : View();
        }

        [HttpPost]
        public async Task<IActionResult> Approve(ApprovalVm vm)
        {
            var date = vm.CanBeEditedTill;

            var b = date > DateTime.Now;
            if (ModelState.IsValid)
            {
                var token = SessionService.GetToken();

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiConstants.Scheme, token);

                var stringContent = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8,
                    ApiConstants.ContentType);

                await Client.PutAsync($"{ApiConstants.BaseApiUrl}/Events",
                    stringContent);

                return RedirectToAction(nameof(Details), new { id = vm.Id });
            }

            ModelState.AddModelError(string.Empty, "invalid attempt");
            return View(vm);
        }

        private async Task<EventListVm> GetEvents(string actionName = default)
        {
            var response =
                await Client.GetAsync($"{ApiConstants.BaseApiUrl}/Events/{actionName}");

            var dataString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EventListVm>(dataString);
        }
    }
}