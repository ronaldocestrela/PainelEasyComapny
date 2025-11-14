using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Relationships
    public ICollection<UserProject> UserProjects { get; set; } = [];
}
