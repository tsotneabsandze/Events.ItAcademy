using System;
using Microsoft.AspNetCore.Http;

namespace EVENTS.MVC.ViewModels.CreateEvent
{
    public class CreateEventVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public IFormFile Photo { get; set; }
    }
}