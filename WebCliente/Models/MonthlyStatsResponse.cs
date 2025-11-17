namespace WebCliente.Models
{
    public class MonthlyStatsResponse
    {
        public int TotalClicks { get; set; }
        public int TotalFtds { get; set; }
        public decimal TotalDeposits { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCpa { get; set; }
    }
}