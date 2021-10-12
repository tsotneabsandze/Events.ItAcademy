using System.Collections.Generic;
using Common.Models.User;

namespace Common.Models.UserList
{
    public class UserListVm
    {
        public ICollection<UserVm> Users { get; set; }
    }
}