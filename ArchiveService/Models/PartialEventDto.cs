using System;

namespace ArchiveService.Models
{
    public class PartialEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsArchived { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }
}