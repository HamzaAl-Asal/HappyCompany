using HappyCompany.Abstraction.Models.Users;
using HappyCompany.App.Services.Users;
using HappyCompany.Common;
using HappyCompany.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HappyCompany.Api.Endpoints.Warehouses
{
    public static class UserEndpointExtensions
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost(Constant.UsersLoginRouteName, HandleVerifyLoginUserAsync)
                .AllowAnonymous()
                .WithTags(Constant.UserTagName);

            builder.MapPut(Constant.EditUserRouteName, HandleEditUserAsync)
                .RequireAuthorization(Constant.UserActionsPolicyName)
                .WithTags(Constant.UserTagName);

            builder.MapPut(Constant.ChangeUserPasswordRouteName, HandleChangePasswordAsync)
                .RequireAuthorization(Constant.UserActionsPolicyName)
                .WithTags(Constant.UserTagName);

            builder.MapDelete(Constant.DeleteUserRouteName, HandleDeleteUserAsync)
                .RequireAuthorization(Constant.UserActionsPolicyName)
                .WithTags(Constant.UserTagName);
        }

        private static async Task<IResult> HandleVerifyLoginUserAsync([FromBody] LoginUserViewModel userViewModel, [FromServices] IUserService userService)
        {
            return await userService.VerifyLoginUser(userViewModel);
        }

        private static async Task<IResult> HandleEditUserAsync([FromRoute] int userId, [FromBody] UserViewModel userViewModel, [FromServices] IUserService userService)
        {
            return await userService.EditUserAsync(userId, userViewModel);
        }

        private static async Task<IResult> HandleChangePasswordAsync([FromBody] ChangePasswordViewModel changePasswordViewModel, [FromServices] IUserService userService)
        {
            return await userService.ChangePasswordAsync(changePasswordViewModel);
        }

        private static async Task<IResult> HandleDeleteUserAsync([FromRoute] int userId, [FromServices] IUserService userService)
        {
            return await userService.DeleteUserAsync(userId);
        }
    }
}
