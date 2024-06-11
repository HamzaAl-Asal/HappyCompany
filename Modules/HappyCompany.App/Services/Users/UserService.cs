using HappyCompany.Abstraction.Models.Users;
using HappyCompany.Common.Enums;
using HappyCompany.Context;
using HappyCompany.JwtAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HappyCompany.App.Services.Users
{
    public class UserService : IUserService
    {
        private readonly HappyCompanyDbContext context;
        private readonly ITokenGenerator tokenGenerator;
        private readonly ILogger<UserService> logger;

        public UserService(HappyCompanyDbContext context, ITokenGenerator tokenGenerator, ILogger<UserService> logger)
        {
            this.context = context;
            this.tokenGenerator = tokenGenerator;
            this.logger = logger;
        }

        public async Task<IResult> VerifyLoginUser(LoginUserViewModel userViewModel)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => (u.Email == userViewModel.Email)
            && (u.Password == userViewModel.Password));

            if (user == null)
            {
                logger.LogError("Invalid email or password: {Email}", userViewModel.Email);

                return Results.NotFound(new { message = "Invalid Email or password" });
            }

            var token = tokenGenerator.GenerateToken(user);

            user.Password = string.Empty;

            logger.LogInformation("User logged in with Email: {Email}", user.Email);

            return Results.Ok(new { user = user, token = token });
        }

        public async Task<IResult> EditUserAsync(int userId, UserViewModel userViewModel)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                logger.LogError("User not found with Id: {Id}", userId);
               
                return Results.NotFound(new { message = "User not found!" });
            }

            user.Email = userViewModel.Email;
            user.FullName = userViewModel.FullName;
            user.RoleId = userViewModel.RoleId;
            user.IsActive = userViewModel.IsActive;

            await context.SaveChangesAsync();

            logger.LogInformation("User edited: {Id}", user.Id);

            return Results.Ok(new { message = "User updated successfully!" });
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
        {
            var user = await context.Users.FindAsync(changePasswordViewModel.Id);
            if (user == null)
            {
                logger.LogError("User not found with Id: {Id}", changePasswordViewModel.Id);
                
                return Results.NotFound(new { message = "User not found!" });
            }

            user.Password = changePasswordViewModel.NewPassword;

            await context.SaveChangesAsync();

            logger.LogInformation("Password changed for user: {Id}", user.Id);

            return Results.Ok(new { message = "Password changed successfully!" });
        }

        public async Task<IResult> DeleteUserAsync(int userId)
        {
            var user = await context.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                logger.LogError("User not found: {Id}", userId);
                
                return Results.NotFound(new { message = "User not found!" });
            }

            if (user.Role.Name == nameof(UserRole.Admin))
            {
                logger.LogWarning("Attempt to delete Admin user: {Id}", userId);
                
                return Results.Forbid();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            logger.LogInformation("User deleted: {Id}", user.Id);

            return Results.Ok(new { message = "User deleted successfully!" });
        }
    }
}