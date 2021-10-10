using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Models;

namespace INFRASTRUCTURE.Identity.Services.Abstractions
{
    public interface IAccountService
    {
        Task<string> CreateAsync(ApplicationUser user, string password,
            CancellationToken cancellationToken = default);

        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> DeleteUserAsync(ApplicationUser user);
        Task<bool> SignInAsync(ApplicationUser user, string password);
        Task<IReadOnlyList<string>> GetRolesForUserAsync(ApplicationUser user);
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}