using Application.Projects.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.AspNetCore.Identity;
using Core.Entities;

namespace Application.Projects.Queries;

public class ListAllProjectsQuery
{
    public class Query : IRequest<List<ListProjectDto>>
    {
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager) : IRequestHandler<Query, List<ListProjectDto>>
    {
        public async Task<List<ListProjectDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            IQueryable<Project> query = appDbContext.Projects
                .Include(p => p.UserProjects)
                .ThenInclude(up => up.User);

            // If user is not Admin, filter by projects they belong to
            if (!roles.Contains("Admin"))
            {
                query = query.Where(p => p.UserProjects.Any(up => up.UserId == userId));
            }

            return await query
                .AsNoTracking()
                .ProjectTo<ListProjectDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}