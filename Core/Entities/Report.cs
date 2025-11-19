using Core.Enums;

namespace Core.Entities;

public class Report : BaseEntity
{
    public DateOnly ReportDate { get; set; }
    public int Clicks { get; set; }
    public int Ftds { get; set; }
    public decimal Deposits { get; set; }
    public decimal Revenue { get; set; }
    public decimal Cpa { get; set; }
    public int Registrations { get; set; }
    public CurrencyTypeEnum Currency { get; set; }

    // Relationships
    public string CampaignId { get; set; } = string.Empty;
    public Campaign Campaign { get; set; } = null!;
}
