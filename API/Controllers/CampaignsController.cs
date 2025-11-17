using Application.Campaigns.Commands;
using Application.Campaigns.DTOs;
using Application.Campaigns.Queries;
using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CampaignsController : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateCampaign(CreateCampaignDto campaignDto)
    {
        return HandleResult(await Mediator.Send(new CreateCampaignCommand.Command { CreateCampaignDto = campaignDto }));
    }

    [HttpGet("list-all")]
    public async Task<ActionResult<PagedResult<ListCampaignDto>>> ListAllCampaigns([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new ListAllCampaingsQuery.Query
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return await Mediator.Send(query);
    }
}
