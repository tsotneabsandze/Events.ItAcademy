using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MEDIATOR.Events.Commands.CreateEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class EventsController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateEventCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        
    }
}