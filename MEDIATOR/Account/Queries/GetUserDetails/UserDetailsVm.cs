using System.Collections;
using System.Collections.Generic;

namespace MEDIATOR.Account.Queries.GetUserDetails
{
    public class UserDetailsVm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
    }
}