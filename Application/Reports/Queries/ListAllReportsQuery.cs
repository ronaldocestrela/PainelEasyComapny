using Application.Core;
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
    public class Query : PagedQuery, IRequest<PagedResult<ListReportDto>>
    {
        public string? BookmakerId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager, BahiaTimeZone bahiaTimeZone) : IRequestHandler<Query, PagedResult<ListReportDto>>
    {
        public async Task<PagedResult<ListReportDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (!request.StartDate.HasValue) request.StartDate = bahiaTimeZone.GetFirstDayOfCurrentMonth();
            if (!request.EndDate.HasValue) request.EndDate = DateOnly.FromDateTime(bahiaTimeZone.Now().Date);
            if (request.EndDate < request.StartDate)
            {
                return new PagedResult<ListReportDto>
                {
                    Items = [],
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = 0
                };
            }

            var userId = userAccessor.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new PagedResult<ListReportDto>
                {
                    Items = new List<ListReportDto>(),
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = 0
                };
            }

            var roles = await userManager.GetRolesAsync(user);

            // Debug: Log user info
            Console.WriteLine($"User ID: {userId}");
            Console.WriteLine($"User Email: {user.Email}");
            Console.WriteLine($"User Roles: {string.Join(", ", roles)}");

            IQueryable<Report> query = appDbContext.Reports
                .Where(r => r.ReportDate >= request.StartDate && r.ReportDate <= request.EndDate)
                .Include(r => r.Campaign)
                .ThenInclude(c => c.Bookmaker)
                .Include(r => r.Campaign)
                .ThenInclude(c => c.Project)
                .OrderByDescending(r => r.ReportDate);
            
            if (!string.IsNullOrEmpty(request.BookmakerId))
            {
                query = query.Where(r => r.Campaign != null && r.Campaign.BookmakerId == request.BookmakerId);
            }

            // If user is not Admin, filter by projects they belong to
            if (!roles.Contains("Admin"))
            {
                var userProjectIds = await appDbContext.UserProjects
                    .Where(up => up.UserId == userId)
                    .Select(up => up.ProjectId)
                    .ToListAsync(cancellationToken);

                query = query.Where(r => r.Campaign != null && r.Campaign.ProjectId != null && userProjectIds.Contains(r.Campaign.ProjectId));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .AsNoTracking()
                .Skip(request.Skip)
                .Take(request.PageSize)
                .Select(r => new ListReportDto
                {
                    Id = r.Id,
                    ReportDate = r.ReportDate,
                    Clicks = r.Clicks,
                    Ftds = r.Ftds,
                    Deposits = r.Deposits,
                    Currency = r.Currency,
                    Registrations = r.Registrations,
                    CampaignId = r.CampaignId,
                    CreatedAt = r.CreatedAt ?? DateTime.UtcNow,
                    CampaignName = r.Campaign != null ? r.Campaign.Name : string.Empty,
                    CampaignDescription = r.Campaign != null ? r.Campaign.Description : string.Empty,
                    BookmakerName = r.Campaign != null && r.Campaign.Bookmaker != null ? r.Campaign.Bookmaker.Name : string.Empty,
                    BookmakerWebsite = r.Campaign != null && r.Campaign.Bookmaker != null ? r.Campaign.Bookmaker.Website : string.Empty,
                    ProjectName = r.Campaign != null && r.Campaign.Project != null ? r.Campaign.Project.Name : string.Empty,
                    ProjectDescription = r.Campaign != null && r.Campaign.Project != null ? r.Campaign.Project.Description : string.Empty
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<ListReportDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}