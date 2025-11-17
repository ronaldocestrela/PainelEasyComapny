using System;

namespace WebCliente.Models
{
    public class ReportDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateOnly ReportDate { get; set; }
        public int Clicks { get; set; }
        public int Ftds { get; set; }
        public decimal Deposits { get; set; }
        public CurrencyTypeEnum Currency { get; set; }
        public string CampaignId { get; set; } = string.Empty;

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
}