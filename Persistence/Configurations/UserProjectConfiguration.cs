using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
{
    public void Configure(EntityTypeBuilder<UserProject> builder)
    {
        builder.HasKey(up => new { up.UserId, up.ProjectId });

        builder.HasOne(up => up.User)
            .WithMany(u => u.UserProjects)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Project)
            .WithMany(p => p.UserProjects)
            .HasForeignKey(up => up.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(up => new { up.UserId, up.ProjectId })
            .IsUnique();
    }
}