using Application.Core;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;

public class UpdateUserCommand
{
    public class Command : IRequest<Result<bool>>
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class Handler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return Result<bool>.Failure("Usuário não encontrado", 404);
            }

            // Verificar se o email já está em uso por outro usuário
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null && existingUser.Id != request.Id)
            {
                return Result<bool>.Failure("Email já está em uso por outro usuário", 400);
            }

            // Verificar se a role existe
            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                return Result<bool>.Failure("Role inválida", 400);
            }

            // Atualizar dados do usuário
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.UserName = request.Email;
            user.EmailConfirmed = request.IsActive;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return Result<bool>.Failure("Erro ao atualizar usuário: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)), 400);
            }

            // Atualizar role
            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            var roleResult = await userManager.AddToRoleAsync(user, request.Role);
            if (!roleResult.Succeeded)
            {
                return Result<bool>.Failure("Erro ao atualizar role do usuário", 400);
            }

            return Result<bool>.Success(true);
        }
    }
}