using System.Net.Http.Json;
using WebCliente.Models;

namespace WebCliente.Services
{
    public class ReportService(IAuthService authService, IConfiguration configuration) : IReportService
    {
        private readonly IAuthService _authService = authService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<ReportDto>?> GetAllReportsAsync()
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.GetAsync($"{baseUrl}/api/reports/list-all");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ReportDto>>();
                }

                Console.WriteLine($"Erro ao buscar reports: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar reports: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateReportAsync(CreateReportRequest request)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.PostAsJsonAsync($"{baseUrl}/api/reports/create", request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Erro ao criar report: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar report: {ex.Message}");
                return false;
            }
        }

        public async Task<ReportListResponse?> GetReportsListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.GetAsync($"{baseUrl}/api/reports/list-all?pageNumber={pageNumber}&pageSize={pageSize}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ReportListResponse>();
                }

                Console.WriteLine($"Erro ao buscar lista de reports: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar lista de reports: {ex.Message}");
                return null;
            }
        }

        public async Task<MonthlyStatsResponse?> GetMonthlyStatsAsync()
        {
            try
            {
                var client = _authService.CreateAuthenticatedClient();
                var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
                var response = await client.GetAsync($"{baseUrl}/api/reports/monthly-stats");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MonthlyStatsResponse>();
                }

                Console.WriteLine($"Erro ao buscar estatísticas mensais: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar estatísticas mensais: {ex.Message}");
                return null;
            }
        }
    }
}