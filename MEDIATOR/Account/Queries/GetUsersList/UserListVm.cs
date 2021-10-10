using System.Collections;
using System.Collections.Generic;

namespace MEDIATOR.Account.Queries.GetUsersList
{
    public class UserListVm
    {
        public ICollection<UserLookupDto> Users { get; set; }
    }
}