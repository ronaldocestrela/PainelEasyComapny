namespace Application.Campaigns.DTOs;

public class ListCampaignDto : BaseCampaignDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string BookmakerName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
}
