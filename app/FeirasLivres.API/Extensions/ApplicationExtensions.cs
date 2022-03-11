using FeirasLivres.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FeirasLivres.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
