using HappyCompany.Common;
using HappyCompany.Common.Enums;
using HappyCompany.JwtAuthentication.Extensions;
using HappyCompany.JwtAuthentication.Helpers;
using HappyCompany.JwtAuthentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HappyCompany.JwtAuthentication
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterJWTAuthenticationModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUsersRoleHelper, UsersRoleHelper>();

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                string secretKey = configuration["JWTAuthenticationOptions:SecretKey"];

                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddAuthorization(options =>
            {
                var roles = new[] { UserRole.Admin, UserRole.Management, UserRole.Auditor };

                options.AddPolicyWithRoles(Constant.WarehousePolicyName, roles);
                options.AddPolicyWithRoles(Constant.UserActionsPolicyName, roles);
            });

            services.OptionsConfiguration(configuration);
        }

        private static void OptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTAuthenticationOptions>(configuration
                .GetSection(Constant.AppSettings.JWTAuthenticationOptions));
        }
    }
}