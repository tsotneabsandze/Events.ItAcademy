using System.Threading.Tasks;
using CORE.Specifications;
using MEDIATOR.Common.Models;
using MEDIATOR.Events.Commands.ApproveEvent;
using MEDIATOR.Events.Commands.CreateEvent;
using MEDIATOR.Events.Commands.DeleteEvent;
using MEDIATOR.Events.Commands.UpdateEvent;
using MEDIATOR.Events.Queries.GetEventDetail;
using MEDIATOR.Events.Queries.GetEventsList;
using MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList;
using MEDIATOR.Events.Queries.GetEventsList.GetApprovedEvents;
using MEDIATOR.Events.Queries.GetEventsList.GetSpecificUserEvents;
using MEDIATOR.Events.Queries.GetEventsList.GetUnapprovedEvents;
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
        public async Task<ActionResult<PaginatedResult<EventsListVm>>> GetAll([FromQuery] SpecParams specParams)
        {
            var vm = await Mediator.Send(new GetEventsListQuery { SpecParams = specParams });
            return Ok(vm);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EventsListVm>> GetApprovedEvents(SpecParams specParams)
        {
            var vm = await Mediator.Send(new GetApprovedEventsQuery());
            return Ok(vm);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EventsListVm>> GetUnapprovedEvents()
        {
            var vm = await Mediator.Send(new GetUnapprovedEventsQuery());
            return Ok(vm);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventsListVm>> GetEventsByUser(string id)
        {
            var vm = await Mediator.Send(new GetSpecificUserEventsQuery { Id = id });
            return vm;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventDto>> Get(int id)
        {
            var vm = await Mediator.Send(new GetEventDetailQuery { Id = id });

            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> Create([FromBody] CreateEventCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
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

        [HttpPut]
        [Authorize("RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ApproveEvent([FromBody] ApproveEventCommand cmd)
        {
            await Mediator.Send(cmd);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Basic")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventCommand cmd)
        {
            await Mediator.Send(cmd);
            return NoContent();
        }
    }
}