using System;

namespace CORE.Entities
{
    public class Event : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CanBeEditedTill { get; set; }
        public byte[] Photo { get; set; }

        public int UserId { get; set; }
    }
}