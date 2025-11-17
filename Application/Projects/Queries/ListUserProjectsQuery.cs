using Application.Core;
using Application.Projects.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Projects.Queries;

public class ListUserProjectsQuery
{
    public class Query : IRequest<Result<List<ProjectDto>>>
    {
        public required string UserId { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Query, Result<List<ProjectDto>>>
    {
        public async Task<Result<List<ProjectDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var projects = await appDbContext.UserProjects
                .Where(up => up.UserId == request.UserId)
                .Include(up => up.Project)
                .Select(up => new ProjectDto
                {
                    Id = up.Project.Id,
                    Name = up.Project.Name,
                    Description = up.Project.Description
                })
                .ToListAsync(cancellationToken);

            return Result<List<ProjectDto>>.Success(projects);
        }
    }
}