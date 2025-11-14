using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(1000);

        builder.HasIndex(c => new { c.Name, c.ProjectId })
            .HasDatabaseName("IX_Campaign_Name_ProjectId");

        builder.HasOne(c => c.Bookmaker)
            .WithMany(b => b.Campaigns)
            .HasForeignKey(c => c.BookmakerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Project)
            .WithMany(p => p.Campaigns)
            .HasForeignKey(c => c.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}