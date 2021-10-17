using CORE.Specifications;
using MEDIATOR.Common.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using MEDIATOR.Events.Commands.CreateEvent;
using MEDIATOR.Events.Commands.DeleteEvent;
using MEDIATOR.Events.Commands.UpdateEvent;
using MEDIATOR.Events.Commands.ApproveEvent;
using MEDIATOR.Events.Commands.ArchiveEvent;
using MEDIATOR.Events.Queries.GetEventsList;
using MEDIATOR.Events.Queries.GetEventDetail;
using MEDIATOR.Events.Queries.GetEventsList.ArchivedEventsList.GetArchivedEvents;
using MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList;
using MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetApprovedEvents;
using MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetSpecificUsersEvents;
using MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetUnapprovedEvents;
using MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetUnarchivedEventsList;


namespace API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class EventsController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Gets archived  list of events",
            Description =
                "returns paginated list of events.you can customize pagination results by including pageSize and pageIndex query parameters in the request"
        )]
        public async Task<ActionResult<PaginatedResult<EventsListVm>>> GetUnarchivedList([FromQuery] SpecParams specParams)
        {
            var vm = await Mediator.Send(new GetUnarchivedEventsListQuery { SpecParams = specParams });
            return Ok(vm);
        }
        
        
        [HttpGet("[action]")]
        [Authorize(policy: "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Gets unarchived list of events"
        )]
        public async Task<ActionResult<PaginatedResult<EventsListVm>>> GetArchivedList()
        {
            var vm = await Mediator.Send(new GetArchivedEventsListQuery());
            return Ok(vm);
        }

        
        [HttpGet("[action]")]
        [Authorize(policy: "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Gets  list of unarchived, approved events")]
        public async Task<ActionResult<EventsListVm>> GetApprovedEvents()
        {
            var vm = await Mediator.Send(new GetApprovedEventsQuery());
            return Ok(vm);
        }

       
        [HttpGet("[action]")]
        [Authorize(policy: "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Gets full list of unarchived, unapproved events")]
        public async Task<ActionResult<EventsListVm>> GetUnapprovedEvents()
        {
            var vm = await Mediator.Send(new GetUnapprovedEventsQuery());
            return Ok(vm);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retrieves events which belong to user with specified id")]
        public async Task<ActionResult<EventsListVm>> GetEventsByUser(string id)
        {
            var vm = await Mediator.Send(new GetSpecificUserEventsQuery { Id = id });
            return vm;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Gets event with specified id")]
        public async Task<ActionResult<EventDto>> Get(int id)
        {
            var vm = await Mediator.Send(new GetEventDetailQuery { Id = id });

            return Ok(vm);
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="command">event data</param>
        /// <remarks>
        /// Sample request
        /// 
        ///     {
        ///         "title": "title of the event",
        ///         "description": "description of the event",
        ///         "starts": "2021-10-15T20:10:57.727Z",
        ///         "ends": "2021-10-15T20:10:57.727Z",
        ///         "photo": byte array
        ///     }
        ///
        /// </remarks>
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Deletes event by passed id value")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEventCommand { Id = id });
            return NoContent();
        }


        /// <summary>
        /// approve event
        /// </summary>
        /// <param name="cmd">data</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "id": 10,
        ///         "canBeEditedTill": "2021-10-15 20:00:00"
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Authorize("RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ApproveEvent([FromBody] ApproveEventCommand cmd)
        {
            await Mediator.Send(cmd);
            return NoContent();
        }


        /// <summary>
        /// update event
        /// </summary>
        /// <param name="id">event id</param>
        /// <param name="cmd">data</param>
        /// <remarks>
        /// Sample request
        /// 
        ///     {
        ///         "id": 29,
        ///         "description": "some text",
        ///         "starts": "2021-10-15T20:05:03.442Z",
        ///         "ends": "2021-10-15T20:05:03.442Z",
        ///         "photo": "byte array",
        ///         "userId": "b1fa6849-6ff1-410c-a8fa-01087089c089"
        ///     }
        ///
        /// </remarks>
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


        #region only for background worker

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IReadOnlyList<PartialEventDto>>> GetFullList()
        {
            var vm = await Mediator.Send(new GetAllEventsListQuery());
            return Ok(vm);
        }

        
        [AllowAnonymous]
        [HttpPut("[action]")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> AddToArchive([FromBody]ArchiveEventCommand cmd)
        {
            await Mediator.Send(cmd);
            return NoContent();
        }


        #endregion
    }
}