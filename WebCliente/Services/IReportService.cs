using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IReportService
    {
        Task<List<ReportDto>?> GetAllReportsAsync();
        Task<bool> CreateReportAsync(CreateReportRequest request);
        Task<ReportListResponse?> GetReportsListAsync(int pageNumber, int pageSize, DateTime? startDate = null, DateTime? endDate = null, string? bookmakerId = null);
        Task<MonthlyStatsResponse?> GetMonthlyStatsAsync();
    }
}