using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.Property(r => r.Deposits)
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.HasIndex(r => r.ReportDate)
            .HasDatabaseName("IX_Report_ReportDate");

        builder.HasIndex(r => new { r.CampaignId, r.ReportDate })
            .HasDatabaseName("IX_Report_CampaignId_ReportDate");

        builder.HasIndex(r => r.Currency)
            .HasDatabaseName("IX_Report_Currency");

        builder.HasOne(r => r.Campaign)
            .WithMany()
            .HasForeignKey(r => r.CampaignId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}