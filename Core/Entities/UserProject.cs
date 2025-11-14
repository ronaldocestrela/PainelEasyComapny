namespace Core.Entities;

public class UserProject : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public string ProjectId { get; set; } = string.Empty;
    public Project Project { get; set; } = null!;
}