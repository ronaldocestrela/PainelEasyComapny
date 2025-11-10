using Application.Bookmakers.Commands;
using Application.Bookmakers.DTOs;
using FluentValidation;

namespace Application.Bookmakers.Validators;

public class CreateBookmakerValidator : BaseBookmakerValidator<CreateBookmakerCommand.Command, CreateBookmakerDto>
{
    public CreateBookmakerValidator() : base(x => x.BookmakerDto)
    {
        RuleFor(x => x.BookmakerDto.Website).NotEmpty().WithMessage("O site da casa de apostas é obrigatório.")
            .MaximumLength(200).WithMessage("O site da casa de apostas não pode exceder 200 caracteres.");
    }
}
