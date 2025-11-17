using Application.Core;
using Application.Interfaces;
using Application.Reports.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Core.Entities;

namespace Application.Reports.Queries;

public class GetMonthlyStatsQuery
{
    public class Query : IRequest<Result<MonthlyStatsDto>>
    {
    }

    public class Handler(AppDbContext appDbContext, IUserAccessor userAccessor, UserManager<ApplicationUser> userManager) : IRequestHandler<Query, Result<MonthlyStatsDto>>
    {
        public async Task<Result<MonthlyStatsDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<MonthlyStatsDto>.Failure("Usuário não encontrado.", 404);
            }

            var roles = await userManager.GetRolesAsync(user);
            var isAdmin = roles.Contains("Admin");

            // Get current month and year
            var now = DateTime.UtcNow;
            var currentMonth = now.Month;
            var currentYear = now.Year;

            IQueryable<Report> query = appDbContext.Reports
                .Include(r => r.Campaign)
                .Where(r => r.ReportDate.Year == currentYear && r.ReportDate.Month == currentMonth);

            // If user is not Admin, filter by projects they belong to
            if (!isAdmin)
            {
                var userProjectIds = await appDbContext.UserProjects
                    .Where(up => up.UserId == userId)
                    .Select(up => up.ProjectId)
                    .ToListAsync(cancellationToken);

                query = query.Where(r => r.Campaign != null && r.Campaign.ProjectId != null && userProjectIds.Contains(r.Campaign.ProjectId));
            }

            var stats = await query
                .GroupBy(r => 1) // Group all results together
                .Select(g => new MonthlyStatsDto
                {
                    TotalClicks = g.Sum(r => r.Clicks),
                    TotalFtds = g.Sum(r => r.Ftds),
                    TotalDeposits = g.Sum(r => r.Deposits),
                    TotalRevenue = g.Sum(r => r.Revenue),
                    TotalCpa = g.Sum(r => r.Cpa),
                    CurrentMonth = currentMonth,
                    CurrentYear = currentYear
                })
                .FirstOrDefaultAsync(cancellationToken);

            // If no data found, return zeros
            if (stats == null)
            {
                stats = new MonthlyStatsDto
                {
                    TotalClicks = 0,
                    TotalFtds = 0,
                    TotalDeposits = 0,
                    TotalRevenue = 0,
                    TotalCpa = 0,
                    CurrentMonth = currentMonth,
                    CurrentYear = currentYear
                };
            }

            return Result<MonthlyStatsDto>.Success(stats);
        }
    }
}