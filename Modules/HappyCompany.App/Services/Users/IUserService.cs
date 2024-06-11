using HappyCompany.Abstraction.Models.Users;
using Microsoft.AspNetCore.Http;

namespace HappyCompany.App.Services.Users
{
    public interface IUserService
    {
        Task<IResult> VerifyLoginUser(LoginUserViewModel userViewModel);
        Task<IResult> EditUserAsync(int userId, UserViewModel userViewModel);
        Task<IResult> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel);
        Task<IResult> DeleteUserAsync(int userId);
    }
}