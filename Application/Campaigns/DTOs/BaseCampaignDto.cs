namespace Application.Campaigns.DTOs;

public class BaseCampaignDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BookmakerId { get; set; } = string.Empty;
    public string? ProjectId { get; set; }
}
