namespace Application.Bookmakers.DTOs;

public class ListBookmaker : BaseBookmakerDto
{
    public string Id { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
