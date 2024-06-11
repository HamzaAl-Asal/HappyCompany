using HappyCompany.App.Options;
using HappyCompany.App.Services.Mappers.WarehouseProfiles;
using HappyCompany.App.Services.Users;
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
            services.AddScoped<IUserService, UserService>();
           
            services.AddAutoMapper(typeof(WarehouseProfile).Assembly);
            services.AddMemoryCache();
          

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