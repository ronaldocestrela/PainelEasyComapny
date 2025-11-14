using Application.Campaigns.DTOs;
using FluentValidation;

namespace Application.Campaigns.Validators;

public class BaseCampaignValidator<T, TDto> : AbstractValidator<T> where TDto : BaseCampaignDto
{
    public BaseCampaignValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Name)
            .NotEmpty().WithMessage("Campaign name is required.")
            .MaximumLength(100).WithMessage("Campaign name must not exceed 100 characters.");

        RuleFor(x => selector(x).BookmakerId)
            .NotEmpty().WithMessage("Bookmaker ID is required.");
    }
}
