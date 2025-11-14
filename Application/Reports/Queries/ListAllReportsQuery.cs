using Application.Interfaces;
using Application.Reports.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Core.Entities;

namespace Application.Reports.Queries;

public class ListAllReportsQuery
{
    public class Query : IRequest<List<ListReportDto>>
    {
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager) : IRequestHandler<Query, List<ListReportDto>>
    {
        public async Task<List<ListReportDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            IQueryable<Report> query = appDbContext.Reports
                .Include(r => r.Campaign)
                .ThenInclude(c => c.Bookmaker)
                .Include(r => r.Campaign)
                .ThenInclude(c => c.Project);

            // If user is not Admin, filter by projects they belong to
            if (!roles.Contains("Admin"))
            {
                var userProjectIds = await appDbContext.UserProjects
                    .Where(up => up.UserId == userId)
                    .Select(up => up.ProjectId)
                    .ToListAsync(cancellationToken);

                query = query.Where(r => r.Campaign != null && userProjectIds.Contains(r.Campaign.ProjectId));
            }

            return await query
                .AsNoTracking()
                .ProjectTo<ListReportDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}