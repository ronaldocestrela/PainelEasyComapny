namespace WebCliente.Models
{
    public class CreateCampaignRequest
    {
        public string Name { get; set; } = string.Empty;
        public string BookmakerId { get; set; } = string.Empty;
        public string? ProjectId { get; set; }
    }
}