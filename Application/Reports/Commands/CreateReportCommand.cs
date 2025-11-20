using Application.Core;
using Application.Reports.DTOs;
using AutoMapper;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Reports.Commands;

public class CreateReportCommand
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateReportDto ReportDto { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper, BahiaTimeZone bahiaTimeZone) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var report = mapper.Map<Report>(request.ReportDto);
            var campaign = await appDbContext.Campaigns.FirstOrDefaultAsync(b => b.Id == report.CampaignId, cancellationToken);
            if(campaign == null) return Result<string>.Failure("Campanha não encontrada.", 404);

            var bookMaker = await appDbContext.Bookmakers.FirstOrDefaultAsync(b => b.Id == campaign.BookmakerId, cancellationToken);
            if (bookMaker == null) return Result<string>.Failure("Bookmaker não encontrado.", 404);

            switch (bookMaker.Name)
            {
                case "BetMGM":
                    {
                        report.Cpa *= 130;
                        report.Revenue *= 0.30m;
                        break;
                    }
                case "Betsson":
                    {
                        report.Cpa *= 160;
                        report.Revenue *= 0.35m;
                        break;
                    }
            }
            report.CreatedAt = bahiaTimeZone.Now();

            appDbContext.Reports.Add(report);

            return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Result<string>.Success(report.Id.ToString())
                : Result<string>.Failure("Falha ao criar o relatório.", 400);
        }
    }
}