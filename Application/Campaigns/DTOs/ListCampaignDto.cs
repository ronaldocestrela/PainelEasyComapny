namespace Application.Campaigns.DTOs;

public class ListCampaignDto : BaseCampaignDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
