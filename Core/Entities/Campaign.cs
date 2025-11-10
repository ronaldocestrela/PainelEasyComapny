namespace Core.Entities;

public class Campaign : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Relationships
    public string BookmakerId { get; set; } = string.Empty;
    public Bookmaker Bookmaker { get; set; } = null!;
}
