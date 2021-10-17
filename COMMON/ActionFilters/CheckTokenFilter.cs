using System.Threading.Tasks;
using Common.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Common.ActionFilters
{
    public class CheckTokenFilter : IAsyncActionFilter
    {
        private readonly ISessionService _sessionService;
    
        public CheckTokenFilter(ISessionService sessionService)
            => _sessionService = sessionService;
    
    
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (string.IsNullOrEmpty(_sessionService.GetToken()))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "Controller", "Account" },
                        { "Action", "Login" }
                    });
                return;
            }
            await next();
        }
    }
}