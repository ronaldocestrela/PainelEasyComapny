using Application.Campaigns.Commands;
using Application.Campaigns.DTOs;
using Application.Campaigns.Queries;
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
    public async Task<ActionResult<List<ListCampaignDto>>> ListAllCampaigns()
    {
        return await Mediator.Send(new ListAllCampaingsQuery.Query());
    }
}
