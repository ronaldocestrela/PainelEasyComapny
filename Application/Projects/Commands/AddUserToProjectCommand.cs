using Application.Core;
using AutoMapper;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Projects.Commands;

public class AddUserToProjectCommand
{
    public class Command : IRequest<Result<string>>
    {
        public required string UserId { get; set; }
        public required string ProjectId { get; set; }
    }

    public class Handler(AppDbContext appDbContext, UserManager<ApplicationUser> userManager) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return Result<string>.Failure("Usuário não encontrado.", 404);

            var project = await appDbContext.Projects.FindAsync(request.ProjectId);
            if (project == null)
                return Result<string>.Failure("Projeto não encontrado.", 404);

            var existingAssociation = await appDbContext.UserProjects
                .FirstOrDefaultAsync(up => up.UserId == request.UserId && up.ProjectId == request.ProjectId, cancellationToken);

            if (existingAssociation != null)
                return Result<string>.Failure("Usuário já está associado a este projeto.", 400);

            var userProject = new UserProject
            {
                UserId = request.UserId,
                ProjectId = request.ProjectId
            };

            appDbContext.UserProjects.Add(userProject);

            return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Result<string>.Success("Usuário adicionado ao projeto com sucesso.")
                : Result<string>.Failure("Falha ao adicionar usuário ao projeto.", 400);
        }
    }
}