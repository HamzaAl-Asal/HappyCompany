using HappyCompany.App.BackgroundServices;
using HappyCompany.App.Options;
using HappyCompany.App.Services.Mappers;
using HappyCompany.App.Services.Warehouses;
using HappyCompany.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HappyCompany.App
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAppModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddAutoMapper(typeof(WarehouseProfile).Assembly);
            services.AddMemoryCache();

            services.AddSingleton<WarehouseCacheInitializationBackgroundService>()
                   .AddHostedService(provider =>
                   provider.GetRequiredService<WarehouseCacheInitializationBackgroundService>());

            // serilog
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

            // configure options
            services.OptionsConfiguration(configuration);
        }

        private static void OptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheKeyOptions>(configuration
                .GetSection(Constant.AppSettings.CacheKeyOptions));
        }
    }
}