using Microsoft.AspNetCore.Builder;

namespace CMGScriptures.Core.System.ExceptionHandler
{
    public static class ConfigureExceptionHandler
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
        }
    }
}
