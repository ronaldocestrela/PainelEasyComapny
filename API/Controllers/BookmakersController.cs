using Application.Bookmakers.Commands;
using Application.Bookmakers.DTOs;
using Application.Bookmakers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BookmakersController : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateBookmaker(CreateBookmakerDto bookmakerDto)
    {
        return HandleResult(await Mediator.Send(new CreateBookmakerCommand.Command { BookmakerDto = bookmakerDto }));
    }

    [HttpGet("list-all")]
    public async Task<ActionResult<List<ListBookmaker>>> ListAllBookmakers()
    {
        return await Mediator.Send(new ListAllBookMakers.Query());
    }
}
