namespace WebCliente.Models
{
    public class CampaignDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BookmakerId { get; set; } = string.Empty;
        public string? ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? BookmakerName { get; set; }
        public string? ProjectName { get; set; }
    }
}