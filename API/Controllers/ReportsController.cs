using Application.Core;
using Application.Reports.Commands;
using Application.Reports.DTOs;
using Application.Reports.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ReportsController : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateReport(CreateReportDto reportDto)
    {
        return HandleResult(await Mediator.Send(new CreateReportCommand.Command { ReportDto = reportDto }));
    }

    [HttpGet("list-all")]
    public async Task<ActionResult<PagedResult<ListReportDto>>> ListAllReports([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new ListAllReportsQuery.Query
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return await Mediator.Send(query);
    }

    [HttpGet("monthly-stats")]
    public async Task<ActionResult<MonthlyStatsDto>> GetMonthlyStats()
    {
        return HandleResult(await Mediator.Send(new GetMonthlyStatsQuery.Query()));
    }
}