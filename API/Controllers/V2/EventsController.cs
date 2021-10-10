using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2
{
    [ApiVersion("2.0")]
    public class EventsController : BaseApiController
    {
        /// <summary>
        /// test comment from summary
        /// </summary>
        
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}