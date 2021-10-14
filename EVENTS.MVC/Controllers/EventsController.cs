using System.Threading.Tasks;
using Common.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace EVENTS.MVC.Controllers
{
    public class EventsController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View("Events");
        }
        
        [ServiceFilter(typeof(CheckTokenFilter))]
        public async Task<IActionResult> Test()
        {
            return Ok("ooooo");
        }
    }
}