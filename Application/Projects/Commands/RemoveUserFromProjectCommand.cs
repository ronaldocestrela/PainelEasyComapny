using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Projects.Commands;

public class RemoveUserFromProjectCommand
{
    public class Command : IRequest<Result<string>>
    {
        public required string UserId { get; set; }
        public required string ProjectId { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userProject = await appDbContext.UserProjects
                .FirstOrDefaultAsync(up => up.UserId == request.UserId && up.ProjectId == request.ProjectId, cancellationToken);

            if (userProject == null)
                return Result<string>.Failure("Associação entre usuário e projeto não encontrada.", 404);

            appDbContext.UserProjects.Remove(userProject);

            return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Result<string>.Success("Usuário removido do projeto com sucesso.")
                : Result<string>.Failure("Falha ao remover usuário do projeto.", 400);
        }
    }
}