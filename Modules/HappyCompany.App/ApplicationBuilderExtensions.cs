using HappyCompany.App.Middlwares;
using Microsoft.AspNetCore.Builder;

namespace HappyCompany.App
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseAppModuleMiddlwares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiLoggingMiddleware>();
        }
    }
}