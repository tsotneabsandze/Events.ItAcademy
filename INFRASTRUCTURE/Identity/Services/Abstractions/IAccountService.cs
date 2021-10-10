using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Models;

namespace INFRASTRUCTURE.Identity.Services.Abstractions
{
    public interface IAccountService
    {
        Task<bool> CreateAsync(ApplicationUser applicationUser,string password, CancellationToken cancellationToken = default);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> DeleteUserAsync(ApplicationUser user);
        Task<bool> SignInAsync(ApplicationUser user,string password);
        Task<IReadOnlyList<string>> GetRolesForUserAsync(ApplicationUser user);
    }
}