using Application.Bookmakers.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Bookmakers.Queries;

public class ListAllBookMakers
{
    public class Query : IRequest<List<ListBookmaker>>
    {
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper) : IRequestHandler<Query, List<ListBookmaker>>
    {
        public async Task<List<ListBookmaker>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await appDbContext.Bookmakers
                .AsNoTracking()
                .ProjectTo<ListBookmaker>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
