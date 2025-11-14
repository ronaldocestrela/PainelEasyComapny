using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class BookmakerConfiguration : IEntityTypeConfiguration<Bookmaker>
{
    public void Configure(EntityTypeBuilder<Bookmaker> builder)
    {
        builder.HasIndex(b => b.Name)
            .IsUnique()
            .HasDatabaseName("IX_Bookmaker_Name_Unique");

        builder.Property(b => b.Website)
            .HasMaxLength(500);

        builder.HasMany(b => b.Campaigns)
            .WithOne(c => c.Bookmaker)
            .HasForeignKey(c => c.BookmakerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}