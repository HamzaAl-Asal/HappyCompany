using HappyCompany.Context.DataAccess.Entities.Users;

namespace HappyCompany.JwtAuthentication
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}