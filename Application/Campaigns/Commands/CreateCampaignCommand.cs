using Application.Campaigns.DTOs;
using Application.Core;
using AutoMapper;
using Core.Entities;
using MediatR;
using Persistence;

namespace Application.Campaigns.Commands;

public class CreateCampaignCommand
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateCampaignDto CreateCampaignDto { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper, BahiaTimeZone bahiaTimeZone) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var campaign = mapper.Map<Campaign>(request.CreateCampaignDto);
            campaign.CreatedAt = bahiaTimeZone.Now();

            appDbContext.Campaigns.Add(campaign);

            return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Result<string>.Success(campaign.Id.ToString())
                : Result<string>.Failure("Falha ao criar a campanha.", 400);
        }
    }
}
