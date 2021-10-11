using System.Security.Claims;

namespace INFRASTRUCTURE.Identity.Services.Abstractions
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}