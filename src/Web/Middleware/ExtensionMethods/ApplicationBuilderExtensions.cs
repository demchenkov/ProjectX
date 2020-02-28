using Microsoft.AspNetCore.Builder;

namespace Web.Middleware.ExtensionMethods
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}