using Application.Core;
using Application.Projects.DTOs;
using AutoMapper;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Projects.Commands;

public class CreateProjectCommand
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateProjectDto ProjectDto { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper, BahiaTimeZone bahiaTimeZone) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = mapper.Map<Project>(request.ProjectDto);
            project.CreatedAt = bahiaTimeZone.Now();

            appDbContext.Projects.Add(project);

            try
            {
                return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                    ? Result<string>.Success(project.Id.ToString())
                    : Result<string>.Failure("Falha ao criar o projeto.", 400);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Project_Name_Unique") == true)
                {
                    return Result<string>.Failure("Já existe um projeto com este nome.", 400);
                }
                throw;
            }
        }
    }
}