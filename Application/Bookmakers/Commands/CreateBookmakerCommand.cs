using Application.Bookmakers.DTOs;
using Application.Core;
using AutoMapper;
using Core.Entities;
using MediatR;
using Persistence;

namespace Application.Bookmakers.Commands;

public class CreateBookmakerCommand
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateBookmakerDto BookmakerDto { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var bookmaker = mapper.Map<Bookmaker>(request.BookmakerDto);

            appDbContext.Bookmakers.Add(bookmaker);
            
            return await appDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Result<string>.Success("Casa de apostas criada com sucesso.")
                : Result<string>.Failure("Falha ao criar a casa de apostas.", 400);
        }
    }
}
