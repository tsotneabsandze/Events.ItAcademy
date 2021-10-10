using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.Middlewares.ExceptionHandling
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next.Invoke(ctx);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(ctx, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext ctx, Exception exception)
        {
            var problem = new ErrorResponse(ctx, exception);
            
            _logger.Log(problem.LogLevel,exception,"{TraceId}, {Title}, {Name}",
                problem.TraceId,problem.Title,problem.Name);
            
            ctx.Response.Clear();
            
            await WriteJsonAsync(ctx, problem);
        }
        
        private async Task WriteJsonAsync(HttpContext ctx, ErrorResponse problem)
        {
            ctx.Response.ContentType = "application/json";
            if (problem.Status != null) 
                ctx.Response.StatusCode = problem.Status.Value;
            
            await ctx.Response.WriteAsync(problem.ToString());
        }
    }
}