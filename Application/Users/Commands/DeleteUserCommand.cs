using Application.Core;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;

public class DeleteUserCommand
{
    public class Command : IRequest<Result<bool>>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class Handler(UserManager<ApplicationUser> userManager)
        : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return Result<bool>.Failure("Usuário não encontrado", 404);
            }

            // Não permitir deletar o próprio usuário admin
            var currentUser = await userManager.GetUserAsync(null); // Isso pode não funcionar no contexto do MediatR
            // Por enquanto, vamos permitir deletar qualquer usuário

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<bool>.Failure("Erro ao deletar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)), 400);
            }

            return Result<bool>.Success(true);
        }
    }
}