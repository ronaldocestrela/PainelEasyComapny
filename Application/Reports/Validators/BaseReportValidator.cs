using Application.Reports.DTOs;
using FluentValidation;

namespace Application.Reports.Validators;

public class BaseReportValidator<T, TDto> : AbstractValidator<T> where TDto : BaseReportDto
{
    public BaseReportValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).ReportDate).NotEmpty().WithMessage("A data do relatório é obrigatória.");

        RuleFor(x => selector(x).Clicks).GreaterThanOrEqualTo(0).WithMessage("Os cliques devem ser maiores ou iguais a zero.");

        RuleFor(x => selector(x).Ftds).GreaterThanOrEqualTo(0).WithMessage("Os FTDs devem ser maiores ou iguais a zero.");

        RuleFor(x => selector(x).Deposits).GreaterThanOrEqualTo(0).WithMessage("Os depósitos devem ser maiores ou iguais a zero.");

        RuleFor(x => selector(x).CampaignId).NotEmpty().WithMessage("O ID da campanha é obrigatório.");
    }
}