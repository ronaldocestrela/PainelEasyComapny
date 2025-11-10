using System.Security.Claims;
using Application.Interfaces;
using Application.UserInfo;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext) : IUserAccessor
{
    public async Task<UserInfoDto> GetUserAsync()
    {
        var user = await appDbContext.Users.FindAsync(GetUserId())
            ?? throw new UnauthorizedAccessException("No user is logged in");
        return new UserInfoDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            ImageUrl = user.ImageUrl,
        };
    }

    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("User not found");
    }

    public string GetUserRoleNameByUserIdAsync(string userId)
    {
        var role = appDbContext.UserRoles.FirstOrDefault(ur => ur.UserId == userId) ?? throw new Exception("User not found");

        var roleName = appDbContext.Roles.FirstOrDefault(r => r.Id == role.RoleId)?.Name ?? "User";

        return roleName;
    }

    public async Task<ApplicationUser> GetUserWithPhotosAsync()
    {
        var userId = GetUserId();

        return await appDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId)
                ?? throw new UnauthorizedAccessException("No user is logged in");
    }
}
