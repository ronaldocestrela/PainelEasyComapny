using Core.Enums;

namespace Application.Reports.DTOs;

public class BaseReportDto
{
    public DateOnly ReportDate { get; set; }
    public int Clicks { get; set; }
    public int Ftds { get; set; }
    public decimal Deposits { get; set; }
    public CurrencyTypeEnum Currency { get; set; }
    public string CampaignId { get; set; } = string.Empty;
    public int Registrations { get; set; }
}