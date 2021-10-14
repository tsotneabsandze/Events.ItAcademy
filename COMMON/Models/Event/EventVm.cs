using System;

namespace Common.Models.Event
{
    public class EventVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CanBeEditedTill { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public byte[] Photo { get; set; }
        public string UserId { get; set; }
    }
}