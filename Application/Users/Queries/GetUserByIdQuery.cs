using Application.Core;
using Application.Users.DTOs;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries;

public class GetUserByIdQuery
{
    public class Query : IRequest<Result<UserDto>>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class Handler(UserManager<ApplicationUser> userManager) : IRequestHandler<Query, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return Result<UserDto>.Failure("Usuário não encontrado", 404);
            }

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}".Trim(),
                Email = user.Email!,
                Role = role,
                IsActive = user.IsActive
            };

            return Result<UserDto>.Success(userDto);
        }
    }
}