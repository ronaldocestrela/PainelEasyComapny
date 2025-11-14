using Application.Projects.DTOs;
using FluentValidation;

namespace Application.Projects.Validators;

public class BaseProjectValidator<T, TDto> : AbstractValidator<T> where TDto : BaseProjectDto
{
    public BaseProjectValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Name).NotEmpty().WithMessage("O nome do projeto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do projeto não pode exceder 100 caracteres.");

        // RuleFor(x => selector(x).Description).NotEmpty().WithMessage("A descrição do projeto é obrigatória.")
        //     .MaximumLength(500).WithMessage("A descrição do projeto não pode exceder 500 caracteres.");
    }
}