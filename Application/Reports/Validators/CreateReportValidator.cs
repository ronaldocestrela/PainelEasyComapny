using Application.Reports.Commands;
using Application.Reports.DTOs;
using FluentValidation;

namespace Application.Reports.Validators;

public class CreateReportValidator : BaseReportValidator<CreateReportCommand.Command, CreateReportDto>
{
    public CreateReportValidator() : base(x => x.ReportDto)
    {
    }
}