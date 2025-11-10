using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
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

        var applicationUser = new List<ApplicationUser>
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
            foreach (var user in applicationUser)
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
        

        var experts = new List<Project>
        {
            new()
            {
                Name = "Expert One",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new()
            {
                Name = "Expert Two",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new()
            {
                Name = "Expert Three",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }
        };
        if (!context.Projects.Any())
        {

            context.Projects.AddRange(experts);
            await context.SaveChangesAsync();
        }

        // Criação da relação UserExpertAuthorization
        // if (!context.UserExpertAuthorizations.Any())
        // {
        //     var userExpertAuthorizations = new List<UserExpertAuthorization>
        //     {
        //         new ()
        //         {
        //             UserId = applicationUser.FirstOrDefault(x => x.UserName == "analyst1@analyst.com")!.Id,
        //             ExpertId = experts.FirstOrDefault(x => x.Name == "Expert One")!.Id,
        //         },
        //         new ()
        //         {
        //             UserId = applicationUser.FirstOrDefault(x => x.UserName == "analyst2@analyst.com")!.Id,
        //             ExpertId = experts.FirstOrDefault(x => x.Name == "Expert Two")!.Id,
        //         },
        //         new ()
        //         {
        //             UserId = applicationUser.FirstOrDefault(x => x.UserName == "analyst3@analyst.com")!.Id,
        //             ExpertId = experts.FirstOrDefault(x => x.Name == "Expert Three")!.Id,
        //         },
        //     };

        //     context.UserExpertAuthorizations.AddRange(userExpertAuthorizations);
        //     await context.SaveChangesAsync();
        // }
    }
}
