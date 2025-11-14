using Application.Bookmakers.Commands;
using Application.Campaigns.Commands;
using Application.Campaigns.DTOs;

namespace Application.Campaigns.Validators;

public class CreateCampaignValidator : BaseCampaignValidator<CreateCampaignCommand.Command, CreateCampaignDto>
{
    public CreateCampaignValidator() : base(x => x.CreateCampaignDto)
    {
    }
}
