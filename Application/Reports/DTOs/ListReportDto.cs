namespace Application.Reports.DTOs;

public class ListReportDto : BaseReportDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Campaign information
    public string CampaignName { get; set; } = string.Empty;
    public string CampaignDescription { get; set; } = string.Empty;

    // Bookmaker information
    public string BookmakerName { get; set; } = string.Empty;
    public string BookmakerWebsite { get; set; } = string.Empty;

    // Project information
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectDescription { get; set; } = string.Empty;
}