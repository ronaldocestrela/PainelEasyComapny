namespace Application.Reports.DTOs;

public class MonthlyStatsDto
{
    public int TotalClicks { get; set; } // Cadastros
    public int TotalFtds { get; set; } // FTD
    public decimal TotalDeposits { get; set; } // CPA (valor total dos depósitos)
    public int CurrentMonth { get; set; }
    public int CurrentYear { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalCpa { get; set; }
    public int TotalCadastros { get; set; } // Novo campo para Cadastros
}