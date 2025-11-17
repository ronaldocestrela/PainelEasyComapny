using Application.Core;
using Application.Projects.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Projects.Queries;

public class ListUsersInProjectQuery
{
    public class Query : IRequest<Result<List<UserInProjectDto>>>
    {
        public required string ProjectId { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Query, Result<List<UserInProjectDto>>>
    {
        public async Task<Result<List<UserInProjectDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var project = await appDbContext.Projects.FindAsync(request.ProjectId);
            if (project == null)
                return Result<List<UserInProjectDto>>.Failure("Projeto não encontrado.", 404);

            var users = await appDbContext.UserProjects
                .Where(up => up.ProjectId == request.ProjectId)
                .Include(up => up.User)
                .Select(up => new UserInProjectDto
                {
                    UserId = up.UserId,
                    FirstName = up.User.FirstName,
                    LastName = up.User.LastName,
                    Email = up.User.Email ?? string.Empty
                })
                .ToListAsync(cancellationToken);

            return Result<List<UserInProjectDto>>.Success(users);
        }
    }
}