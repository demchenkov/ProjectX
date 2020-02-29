using Microsoft.AspNetCore.Builder;
using Web.Middleware;

namespace Web.ExtensionMethods
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}