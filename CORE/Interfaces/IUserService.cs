using System.Security.Claims;

namespace CORE.Interfaces
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}