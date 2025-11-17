using Application.Core;
using Application.Users.DTOs;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public class ListAllUsersQuery
{
    public class Query : IRequest<Result<List<UserDto>>> { }

    public class Handler(UserManager<ApplicationUser> userManager) : IRequestHandler<Query, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var users = await userManager.Users.ToListAsync(cancellationToken);
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "User";

                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Name = $"{user.FirstName} {user.LastName}".Trim(),
                    Email = user.Email!,
                    Role = role,
                    IsActive = user.IsActive
                });
            }

            return Result<List<UserDto>>.Success(userDtos);
        }
    }
}