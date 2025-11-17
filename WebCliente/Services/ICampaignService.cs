using WebCliente.Models;

namespace WebCliente.Services
{
    public interface ICampaignService
    {
        Task<CampaignListResponse?> GetAllCampaignsAsync(int pageNumber = 1, int pageSize = 10);
        Task<bool> CreateCampaignAsync(CreateCampaignRequest request);
    }
}