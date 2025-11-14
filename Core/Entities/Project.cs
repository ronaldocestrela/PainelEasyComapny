namespace Core.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Relationships
    public ICollection<UserProject> UserProjects { get; set; } = [];
    public ICollection<Campaign> Campaigns { get; set; } = [];
}
