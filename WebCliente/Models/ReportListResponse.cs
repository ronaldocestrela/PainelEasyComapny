namespace WebCliente.Models
{
    public class ReportListResponse
    {
        public List<ReportListItem> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }

    public class ReportListItem
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string CampaignDescription { get; set; } = string.Empty;
        public string BookmakerName { get; set; } = string.Empty;
        public string BookmakerWebsite { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public DateOnly ReportDate { get; set; }
        public int Clicks { get; set; }
        public int Ftds { get; set; }
        public decimal Deposits { get; set; }
        public int Currency { get; set; }
        public string CampaignId { get; set; } = string.Empty;
    }
}