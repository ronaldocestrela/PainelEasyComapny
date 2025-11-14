using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        // Clean existing data to ensure clean state
        if (context.UserProjects.Any())
        {
            context.UserProjects.RemoveRange(context.UserProjects);
            await context.SaveChangesAsync();
        }

        if (context.Reports.Any())
        {
            context.Reports.RemoveRange(context.Reports);
            await context.SaveChangesAsync();
        }

        if (context.Campaigns.Any())
        {
            context.Campaigns.RemoveRange(context.Campaigns);
            await context.SaveChangesAsync();
        }

        if (context.Projects.Any())
        {
            context.Projects.RemoveRange(context.Projects);
            await context.SaveChangesAsync();
        }

        if (context.Bookmakers.Any())
        {
            context.Bookmakers.RemoveRange(context.Bookmakers);
            await context.SaveChangesAsync();
        }

        // Remove existing users and roles
        if (userManager.Users.Any())
        {
            foreach (var user in userManager.Users.ToList())
            {
                await userManager.DeleteAsync(user);
            }
        }

        if (context.Roles.Any())
        {
            foreach (var role in context.Roles.ToList())
            {
                await roleManager.DeleteAsync(role);
            }
        }

        // Seed Roles
        var roles = new List<ApplicationRole>
        {
            new() { Name = "Admin" },
            new() { Name = "Analyst" },
            new() { Name = "Expert" }
        };

        if (!context.Roles.Any())
        {
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        // Seed Users
        var applicationUsers = new List<ApplicationUser>
        {
            new() {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            },
            new() {
                FirstName = "Analyst",
                LastName = "ONE",
                UserName = "analyst1@analyst.com",
                Email = "analyst1@analyst.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            },
            new() {
                FirstName = "Analyst",
                LastName = "TWO",
                UserName = "analyst2@analyst.com",
                Email = "analyst2@analyst.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            },
            new() {
                FirstName = "Analyst",
                LastName = "THREE",
                UserName = "analyst3@analyst.com",
                Email = "analyst3@analyst.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            },
            new() {
                FirstName = "Expert 1",
                LastName = "ONE",
                UserName = "expert1@expert.com",
                Email = "expert1@expert.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            },
            new() {
                FirstName = "Expert 2",
                LastName = "TWO",
                UserName = "expert2@expert.com",
                Email = "expert2@expert.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            }
        };

        if (!userManager.Users.Any())
        {
            foreach (var user in applicationUsers)
            {
                await userManager.CreateAsync(user, "Hadouken@69");
            }
        }

        // Assign roles to users
        var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
        if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var analyst1 = await userManager.FindByEmailAsync("analyst1@analyst.com");
        if (analyst1 != null && !await userManager.IsInRoleAsync(analyst1, "Analyst"))
        {
            await userManager.AddToRoleAsync(analyst1, "Analyst");
        }

        var analyst2 = await userManager.FindByEmailAsync("analyst2@analyst.com");
        if (analyst2 != null && !await userManager.IsInRoleAsync(analyst2, "Analyst"))
        {
            await userManager.AddToRoleAsync(analyst2, "Analyst");
        }

        var analyst3 = await userManager.FindByEmailAsync("analyst3@analyst.com");
        if (analyst3 != null && !await userManager.IsInRoleAsync(analyst3, "Analyst"))
        {
            await userManager.AddToRoleAsync(analyst3, "Analyst");
        }

        var expert1 = await userManager.FindByEmailAsync("expert1@expert.com");
        if (expert1 != null && !await userManager.IsInRoleAsync(expert1, "Expert"))
        {
            await userManager.AddToRoleAsync(expert1, "Expert");
        }

        var expert2 = await userManager.FindByEmailAsync("expert2@expert.com");
        if (expert2 != null && !await userManager.IsInRoleAsync(expert2, "Expert"))
        {
            await userManager.AddToRoleAsync(expert2, "Expert");
        }

        // Seed Bookmakers
        var bookmakerList = new List<Bookmaker>
        {
            new() {
                Name = "Bet365",
                Website = "https://www.bet365.com",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Betfair",
                Website = "https://www.betfair.com",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "William Hill",
                Website = "https://www.williamhill.com",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Pinnacle",
                Website = "https://www.pinnacle.com",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Betsson",
                Website = "https://www.betsson.com",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Bookmakers.AddRange(bookmakerList);
        await context.SaveChangesAsync();

        // Seed Projects
        var projectList = new List<Project>
        {
            new() {
                Name = "Futebol Brasileiro",
                Description = "Campanhas para jogos do Campeonato Brasileiro",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Futebol Europeu",
                Description = "Campanhas para ligas europeias principais",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Tênis ATP",
                Description = "Campanhas para torneios ATP de tênis",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Basquete NBA",
                Description = "Campanhas para jogos da NBA",
                CreatedAt = DateTime.UtcNow
            },
            new() {
                Name = "Futebol Copa do Mundo",
                Description = "Campanhas especiais para Copa do Mundo 2026",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Projects.AddRange(projectList);
        await context.SaveChangesAsync();

        // Get existing data
        var bookmakers = await context.Bookmakers.ToListAsync();
        var projects = await context.Projects.ToListAsync();

        // Seed Campaigns
        if (!context.Campaigns.Any())
        {
            var campaigns = new List<Campaign>
            {
                new() {
                    Name = "Serie A - Palmeiras x Corinthians",
                    Description = "Campanha para o clássico entre Palmeiras e Corinthians",
                    BookmakerId = bookmakers.First(b => b.Name == "Bet365").Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Brasileiro").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    Name = "Premier League - Arsenal x Chelsea",
                    Description = "Campanha para o derby londrino",
                    BookmakerId = bookmakers.First(b => b.Name == "Betfair").Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Europeu").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    Name = "ATP Finals - Djokovic vs Nadal",
                    Description = "Final do torneio ATP Finals",
                    BookmakerId = bookmakers.First(b => b.Name == "William Hill").Id,
                    ProjectId = projects.First(p => p.Name == "Tênis ATP").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    Name = "NBA - Lakers x Warriors",
                    Description = "Clássico da NBA entre Lakers e Warriors",
                    BookmakerId = bookmakers.First(b => b.Name == "Pinnacle").Id,
                    ProjectId = projects.First(p => p.Name == "Basquete NBA").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    Name = "Copa do Mundo 2026 - Brasil x Argentina",
                    Description = "Jogo decisivo das eliminatórias",
                    BookmakerId = bookmakers.First(b => b.Name == "Betsson").Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Copa do Mundo").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    Name = "Serie A - Flamengo x Vasco",
                    Description = "Clássico dos milhões",
                    BookmakerId = bookmakers.First(b => b.Name == "Bet365").Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Brasileiro").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    Name = "Champions League - Real Madrid x Barcelona",
                    Description = "El Clásico na Champions League",
                    BookmakerId = bookmakers.First(b => b.Name == "Betfair").Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Europeu").Id,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Campaigns.AddRange(campaigns);
            await context.SaveChangesAsync();
        }

        // Get campaigns
        var existingCampaigns = await context.Campaigns.ToListAsync();

        // Seed Reports
        if (!context.Reports.Any())
        {
            var reports = new List<Report>
            {
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7)),
                    Clicks = 1250,
                    Ftds = 23,
                    Deposits = 15750.50m,
                    Currency = CurrencyTypeEnum.Real,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Palmeiras")).Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-6)),
                    Clicks = 980,
                    Ftds = 18,
                    Deposits = 12400.75m,
                    Currency = CurrencyTypeEnum.Real,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Arsenal")).Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)),
                    Clicks = 750,
                    Ftds = 12,
                    Deposits = 8900.00m,
                    Currency = CurrencyTypeEnum.Euro,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Djokovic")).Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-4)),
                    Clicks = 2100,
                    Ftds = 45,
                    Deposits = 28500.25m,
                    Currency = CurrencyTypeEnum.Dollar,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Lakers")).Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-3)),
                    Clicks = 3200,
                    Ftds = 67,
                    Deposits = 45200.80m,
                    Currency = CurrencyTypeEnum.Real,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Copa do Mundo")).Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)),
                    Clicks = 1450,
                    Ftds = 28,
                    Deposits = 18900.60m,
                    Currency = CurrencyTypeEnum.Real,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Flamengo")).Id,
                    CreatedAt = DateTime.UtcNow
                },
                new() {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
                    Clicks = 1850,
                    Ftds = 34,
                    Deposits = 23100.45m,
                    Currency = CurrencyTypeEnum.Euro,
                    CampaignId = existingCampaigns.First(c => c.Name.Contains("Real Madrid")).Id,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Reports.AddRange(reports);
            await context.SaveChangesAsync();
        }

        // Seed User-Project associations
        if (!context.UserProjects.Any())
        {
            var userProjects = new List<UserProject>();

            // Get users by email for associations
            var assocAdmin = await userManager.FindByEmailAsync("admin@admin.com");
            var assocAnalyst1 = await userManager.FindByEmailAsync("analyst1@analyst.com");
            var assocAnalyst2 = await userManager.FindByEmailAsync("analyst2@analyst.com");
            var assocAnalyst3 = await userManager.FindByEmailAsync("analyst3@analyst.com");
            var assocExpert1 = await userManager.FindByEmailAsync("expert1@expert.com");
            var assocExpert2 = await userManager.FindByEmailAsync("expert2@expert.com");

            if (assocAdmin != null && assocAnalyst1 != null && assocAnalyst2 != null && assocAnalyst3 != null && assocExpert1 != null && assocExpert2 != null)
            {
                // Admin has access to all projects
                foreach (var project in projects)
                {
                    userProjects.Add(new UserProject
                    {
                        UserId = assocAdmin.Id,
                        ProjectId = project.Id
                    });
                }

                // Analyst1: Futebol Brasileiro + Tênis ATP
                userProjects.Add(new UserProject
                {
                    UserId = assocAnalyst1.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Brasileiro").Id
                });
                userProjects.Add(new UserProject
                {
                    UserId = assocAnalyst1.Id,
                    ProjectId = projects.First(p => p.Name == "Tênis ATP").Id
                });

                // Analyst2: Futebol Europeu + Basquete NBA
                userProjects.Add(new UserProject
                {
                    UserId = assocAnalyst2.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Europeu").Id
                });
                userProjects.Add(new UserProject
                {
                    UserId = assocAnalyst2.Id,
                    ProjectId = projects.First(p => p.Name == "Basquete NBA").Id
                });

                // Analyst3: Copa do Mundo + Futebol Brasileiro
                userProjects.Add(new UserProject
                {
                    UserId = assocAnalyst3.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Copa do Mundo").Id
                });
                userProjects.Add(new UserProject
                {
                    UserId = assocAnalyst3.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Brasileiro").Id
                });

                // Expert1: Todos os projetos de tênis e basquete
                userProjects.Add(new UserProject
                {
                    UserId = assocExpert1.Id,
                    ProjectId = projects.First(p => p.Name == "Tênis ATP").Id
                });
                userProjects.Add(new UserProject
                {
                    UserId = assocExpert1.Id,
                    ProjectId = projects.First(p => p.Name == "Basquete NBA").Id
                });

                // Expert2: Todos os projetos de futebol
                userProjects.Add(new UserProject
                {
                    UserId = assocExpert2.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Brasileiro").Id
                });
                userProjects.Add(new UserProject
                {
                    UserId = assocExpert2.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Europeu").Id
                });
                userProjects.Add(new UserProject
                {
                    UserId = assocExpert2.Id,
                    ProjectId = projects.First(p => p.Name == "Futebol Copa do Mundo").Id
                });
            }

            context.UserProjects.AddRange(userProjects);
            await context.SaveChangesAsync();
        }
    }
}
