using Application.UserInfo;
using Core.Entities;

namespace Application.Interfaces;

public interface IUserAccessor
{
    string GetUserId();
    Task<UserInfoDto> GetUserAsync();
    Task<ApplicationUser> GetUserWithPhotosAsync();
    string GetUserRoleNameByUserIdAsync(string userId);
}
