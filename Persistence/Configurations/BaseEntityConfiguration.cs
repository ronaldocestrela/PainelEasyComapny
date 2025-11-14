using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(e => e.CreatedAt)
            .HasDatabaseName($"IX_{typeof(TEntity).Name}_CreatedAt");

        builder.HasIndex(e => e.UpdatedAt)
            .HasDatabaseName($"IX_{typeof(TEntity).Name}_UpdatedAt");
    }
}