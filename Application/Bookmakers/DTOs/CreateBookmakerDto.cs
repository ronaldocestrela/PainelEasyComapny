using Application.Core;

namespace Application.Bookmakers.DTOs;

public class CreateBookmakerDto(BahiaTimeZone bahiaTimeZone) : BaseBookmakerDto
{
    public string Website { get; set; } = string.Empty;
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(bahiaTimeZone.Now().Date);
}
