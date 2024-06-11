using HappyCompany.Context;
using HappyCompany.JwtAuthentication.Helpers;

namespace HappyCompany.JwtAuthentication.Extensions
{
    internal class UsersRoleHelper : IUsersRoleHelper
    {
        private readonly HappyCompanyDbContext context;

        public UsersRoleHelper(HappyCompanyDbContext context)
        {
            this.context = context;
        }

        public string GetRoleNameById(int roleId)
        {
            return context.Roles.Find(roleId)!.Name;
        }
    }
}