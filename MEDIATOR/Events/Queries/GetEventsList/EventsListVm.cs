using System.Collections.Generic;
using MEDIATOR.Common.Models;

namespace MEDIATOR.Events.Queries.GetEventsList
{
    public class EventsListVm
    {
        public ICollection<EventDto> Events { get; set; }
    }
}