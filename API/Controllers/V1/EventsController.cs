using System.Threading.Tasks;
using CORE.Entities;
using MEDIATOR.Events.Commands.CreateEvent;
using MEDIATOR.Events.Commands.DeleteEvent;
using MEDIATOR.Events.Queries.GetEventsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class EventsController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EventsListVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetEventsListQuery());
            return Ok(vm);
        }
        
        
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateEventCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        
        [HttpDelete("{id:int}")]
        [Authorize("RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEventCommand { Id = id });
            return NoContent();
        }
    }
}