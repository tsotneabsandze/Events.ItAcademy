using System;

namespace MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList
{
    public class PartialEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }
}