using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Models;

namespace INFRASTRUCTURE.Identity.Services.Abstractions
{
    public interface ITokenService
    {
        Task<string> CreateTokeAsync(ApplicationUser user);
    }
}