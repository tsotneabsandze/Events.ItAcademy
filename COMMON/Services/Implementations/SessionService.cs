using Common.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Common.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _accessor;

        public SessionService(IHttpContextAccessor accessor)
            => _accessor = accessor;


        #region set
        
        public void SetToken(string token)
            => _accessor.HttpContext.Session.SetString("token", token);


        public void SetMail(string email)
            => _accessor.HttpContext.Session.SetString("email", email);

        public void SetIdentifier(string id)
            => _accessor.HttpContext.Session.SetString("id", id);

        
        #endregion
      
        #region get

        public string GetToken()
            => _accessor.HttpContext.Session.GetString("token");

        public string GetMail()
            => _accessor.HttpContext.Session.GetString("email");

        public string GetId()
            => _accessor.HttpContext.Session.GetString("id");

        #endregion
    }
}