using Application.Core;
using Application.Reports.DTOs;
using AutoMapper;
using Core.Entities;
using MediatR;
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
            report.CreatedAt = bahiaTimeZone.Now();

            appDbContext.Reports.Add(report);

            return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Result<string>.Success(report.Id.ToString())
                : Result<string>.Failure("Falha ao criar o relatório.", 400);
        }
    }
}