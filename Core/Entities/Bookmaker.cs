namespace Core.Entities;

public class Bookmaker : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;

    // Relationships
    public ICollection<Campaign> Campaigns { get; set; } = [];
}
