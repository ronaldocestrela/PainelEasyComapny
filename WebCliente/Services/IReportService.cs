using WebCliente.Models;

namespace WebCliente.Services
{
    public interface IReportService
    {
        Task<List<ReportDto>?> GetAllReportsAsync();
        Task<bool> CreateReportAsync(CreateReportRequest request);
        Task<ReportListResponse?> GetReportsListAsync(int pageNumber, int pageSize);
        Task<MonthlyStatsResponse?> GetMonthlyStatsAsync();
    }
}