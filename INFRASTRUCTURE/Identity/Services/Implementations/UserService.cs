using System.Security.Claims;
using CORE.Interfaces;
using Microsoft.AspNetCore.Http;

namespace INFRASTRUCTURE.Identity.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return _accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }
}