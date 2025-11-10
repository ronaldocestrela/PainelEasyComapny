using System.Reflection;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    public required DbSet<Bookmaker> Bookmakers { get; set; }
    public required DbSet<Campaign> Campaigns { get; set; }
    public required DbSet<Project> Projects { get; set; }
    public required DbSet<Report> Reports { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
