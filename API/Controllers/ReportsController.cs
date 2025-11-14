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
    public async Task<ActionResult<List<ListReportDto>>> ListAllReports()
    {
        return await Mediator.Send(new ListAllReportsQuery.Query());
    }
}