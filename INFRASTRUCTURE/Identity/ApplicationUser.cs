using System.Collections.Generic;
using CORE.Entities;
using Microsoft.AspNetCore.Identity;

namespace INFRASTRUCTURE.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Events = new HashSet<Event>();
        }
        
        public ICollection<Event> Events { get; set; }
    }
}