using Application.Campaigns.DTOs;
using Application.Core;
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
    public class Query : PagedQuery, IRequest<PagedResult<ListCampaignDto>>
    {
    }

    public class Handler(AppDbContext appDbContext, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager) : IRequestHandler<Query, PagedResult<ListCampaignDto>>
    {
        public async Task<PagedResult<ListCampaignDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            IQueryable<Campaign> query = appDbContext.Campaigns
                .Include(c => c.Bookmaker)
                .Include(c => c.Project);

            // If user is not Admin, filter by projects they belong to
            if (!roles.Contains("Admin"))
            {
                var userProjectIds = await appDbContext.UserProjects
                    .Where(up => up.UserId == userId)
                    .Select(up => up.ProjectId)
                    .ToListAsync(cancellationToken);

                query = query.Where(c => userProjectIds.Contains(c.ProjectId));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(c => new ListCampaignDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    BookmakerId = c.BookmakerId,
                    ProjectId = c.ProjectId,
                    CreatedAt = c.CreatedAt ?? DateTime.UtcNow,
                    BookmakerName = c.Bookmaker.Name,
                    ProjectName = c.Project != null ? c.Project.Name : string.Empty
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<ListCampaignDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
