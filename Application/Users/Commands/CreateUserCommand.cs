using Application.Core;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;

public class CreateUserCommand
{
    public class Command : IRequest<Result<string>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class Handler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Verificar se o email já existe
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Result<string>.Failure("Email já está em uso", 400);
            }

            // Verificar se a role existe
            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                return Result<string>.Failure("Role inválida", 400);
            }

            // Criar o usuário
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Result<string>.Failure("Erro ao criar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)), 400);
            }

            // Atribuir a role
            var roleResult = await userManager.AddToRoleAsync(user, request.Role);
            if (!roleResult.Succeeded)
            {
                // Se falhar ao atribuir role, deletar o usuário criado
                await userManager.DeleteAsync(user);
                return Result<string>.Failure("Erro ao atribuir role ao usuário", 400);
            }

            return Result<string>.Success(user.Id);
        }
    }
}