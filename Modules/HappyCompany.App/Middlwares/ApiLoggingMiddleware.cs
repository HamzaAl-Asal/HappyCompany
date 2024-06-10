using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HappyCompany.App.Middlwares
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ApiLoggingMiddleware> logger;

        public ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var apiName = context.GetEndpoint()?.DisplayName ?? string.Empty;

            logger.LogInformation($"Entering API: {apiName}");

            await next(context);
        }
    }
}