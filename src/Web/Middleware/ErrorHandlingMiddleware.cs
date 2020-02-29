using System;
using System.Net;
using System.Threading.Tasks;
using Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (BaseDomainException ex)
            {
                await HandleDomainExceptionAsync(context, ex, logger);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleDomainExceptionAsync(HttpContext context, BaseDomainException ex, ILogger<ErrorHandlingMiddleware> logger = null)
        {
            var serverErrorCode = ex.ErrorCode;
            var message = ex.Message;
            var userName = context.User.Identity.Name;

            logger?.LogWarning("User: {UserName} got an error: {Message} with error code: {ServerErrorCode}", userName, message, serverErrorCode);

            var result = JsonConvert.SerializeObject(new { errorCode = serverErrorCode, error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) ex.HttpStatusCode;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger = null)
        {
            var userName = context.User.Identity.Name;
            var message = ex.Message;

            logger?.LogError("User: {UserName} got an error: {Message}", userName, message);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.CompleteAsync();
        }
    }
}