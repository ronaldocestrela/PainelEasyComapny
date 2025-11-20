namespace WebCliente.Models
{
    public class CreateReportRequest
    {
        public string ReportDate { get; set; } = string.Empty;
        public string CampaignId { get; set; } = string.Empty;
        public int Currency { get; set; }
        public int Clicks { get; set; }
        public int Ftds { get; set; }
        public decimal Deposits { get; set; }
        public decimal Cpa { get; set; }
        public decimal Revenue { get; set; }
        public int Registrations { get; set; }
    }
}