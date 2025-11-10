using Application.Bookmakers.DTOs;
using FluentValidation;

namespace Application.Bookmakers.Validators;

public class BaseBookmakerValidator<T, TDto> : AbstractValidator<T> where TDto : BaseBookmakerDto
{
    public BaseBookmakerValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Name).NotEmpty().WithMessage("O nome da casa de apostas é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da casa de apostas não pode exceder 100 caracteres.");
    }
}
