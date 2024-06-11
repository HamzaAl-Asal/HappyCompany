using HappyCompany.Common.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HappyCompany.JwtAuthentication.Extensions
{
    internal static class AuthorizationPolicyExtension
    {
        public static void AddPolicyWithRoles(this AuthorizationOptions options, string policyName, params UserRole[] roles)
        {
            options.AddPolicy(policyName, policy =>
            {
                if (roles == null || !roles.Any())
                {
                    throw new InvalidOperationException("Roles must be provided to create a policy.");
                }

                policy.RequireAssertion(context =>
                {
                    var user = context.User;
                    var roleNames = roles.Select(role => role.ToString()).ToArray();

                    return roleNames.Any(role => user.IsInRole(role));
                });
            });
        }
    }
}