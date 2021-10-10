using System.Collections.Generic;
using CORE.Entities;
using Microsoft.AspNetCore.Identity;

namespace INFRASTRUCTURE.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Events = new HashSet<Event>();
        }

        public string Name { get; set; }    
        public string LastName { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}