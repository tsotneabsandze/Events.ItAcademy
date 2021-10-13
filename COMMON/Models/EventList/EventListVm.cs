using System.Collections.Generic;
using Common.Models.Event;

namespace Common.Models.EventList
{
    public class EventListVm
    {
        public ICollection<EventVm> Events { get; set; }
    }
}