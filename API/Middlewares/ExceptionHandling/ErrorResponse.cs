using System;
using System.Net;
using CORE.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Middlewares.ExceptionHandling
{
    public class ErrorResponse : ProblemDetails
    {
        public string Name { get; set; } = "UnhandledError";

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                    return (string)traceId;

                return default;
            }
            set => Extensions["TraceId"] = value;
        }

        public LogLevel LogLevel { get; set; }

        public ErrorResponse(HttpContext ctx, Exception exception)
        {
            TraceId = ctx.TraceIdentifier;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = ctx.Request.Path;

            BindException((dynamic)exception);
        }

        private void BindException(ResourceNotFoundException ex)
        {
            Name = HttpStatusCode.NotFound.ToString();
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(IdentifierMismatchException ex)
        {
            Name = HttpStatusCode.BadRequest.ToString();
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(UnauthorizedException ex)
        {
            Name = HttpStatusCode.Unauthorized.ToString();
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(InvalidDateException ex)
        {
            Name = HttpStatusCode.BadRequest.ToString();
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(ResourceCanNotBeEditedException ex)
        {
            Name = HttpStatusCode.BadRequest.ToString();
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(InvalidCrudOperationException ex)
        {
            Name = HttpStatusCode.BadRequest.ToString();
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(InvalidArchivingException ex)
        {
            Name = HttpStatusCode.BadRequest.ToString();
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ex.Message;
            LogLevel = LogLevel.Information;
        }

        private void BindException(Exception exception)
        {
        }


        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}