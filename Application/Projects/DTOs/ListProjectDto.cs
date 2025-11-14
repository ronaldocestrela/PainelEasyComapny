namespace Application.Projects.DTOs;

public class ListProjectDto : BaseProjectDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}