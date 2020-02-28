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

        public async Task Invoke(HttpContext context, ILogger logger)
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

        private static Task HandleDomainExceptionAsync(HttpContext context, BaseDomainException ex, ILogger logger = null)
        {
            var serverErrorCode = ex.ErrorCode;
            var message = ex.Message;
            logger?.LogWarning($"User: {context.User.Identity.Name} got an error: {message} with error code: {serverErrorCode}");

            var result = JsonConvert.SerializeObject(new { errorCode = serverErrorCode, error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) ex.HttpStatusCode;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger = null)
        {
            logger?.LogError(ex, $"User: {context.User.Identity.Name} got an error: {ex.Message}");
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.CompleteAsync();
        }
    }
}