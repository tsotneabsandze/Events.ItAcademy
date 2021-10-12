using System;

namespace MEDIATOR.Common.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CanBeEditedTill { get; set; }
        public byte[] Photo { get; set; }

        public string UserId { get; set; }
    }
}