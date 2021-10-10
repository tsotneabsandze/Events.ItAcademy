using CORE.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class EventsController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
           Summary = "test",
           Description = "description")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}