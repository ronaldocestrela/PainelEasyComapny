using Application.Campaigns.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Core.Entities;

namespace Application.Campaigns.Queries;

public class ListAllCampaingsQuery
{
    public class Query : IRequest<List<ListCampaignDto>>
    {
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager) : IRequestHandler<Query, List<ListCampaignDto>>
    {
        public async Task<List<ListCampaignDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            IQueryable<Campaign> query = appDbContext.Campaigns
                .Include(c => c.Bookmaker);

            // If user is not Admin, filter by projects they belong to
            if (!roles.Contains("Admin"))
            {
                var userProjectIds = await appDbContext.UserProjects
                    .Where(up => up.UserId == userId)
                    .Select(up => up.ProjectId)
                    .ToListAsync(cancellationToken);

                query = query.Where(c => userProjectIds.Contains(c.ProjectId));
            }

            return await query
                .AsNoTracking()
                .ProjectTo<ListCampaignDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
