using HappyCompany.Context.DataAccess.Entities.Users;
using HappyCompany.JwtAuthentication.Exceptions;
using HappyCompany.JwtAuthentication.Helpers;
using HappyCompany.JwtAuthentication.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HappyCompany.JwtAuthentication
{
    internal class TokenGenerator : ITokenGenerator
    {
        private readonly JWTAuthenticationOptions jWTAuthenticationOptions;
        private readonly IUsersRoleHelper usersRoleHelper;

        public TokenGenerator(IOptionsMonitor<JWTAuthenticationOptions> jWTAuthenticationOptions, IUsersRoleHelper usersRoleHelper)
        {
            this.jWTAuthenticationOptions = jWTAuthenticationOptions.CurrentValue;
            this.usersRoleHelper = usersRoleHelper;
        }

        public string GenerateToken(User user)
        {
            if (!user.IsActive)
            {
                throw new InactiveUserException("Your account is disabled. Please contact support.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jWTAuthenticationOptions.SecretKey);
            var roleName = usersRoleHelper.GetRoleNameById(user.Id);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, roleName),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}