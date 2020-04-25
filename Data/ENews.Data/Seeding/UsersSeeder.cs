using ENews.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ENews.Common;
using Microsoft.Extensions.DependencyInjection;

namespace ENews.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Users.Any(u => u.Roles.Any(r => r.RoleId == dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.AdministratorRoleName).Id)))
            {
                await SeedAdmin(userManager);
            }
            if (!dbContext.Users.Any(u => u.Roles.Any(r => r.RoleId == dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.ReporterRoleName).Id)))
            {
                await SeedReporter(userManager);
            }
        }

        private static async Task SeedReporter(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                Email = GlobalConstants.ReporterEmailName,
                UserName = GlobalConstants.ReporterUsername,
                FirstName = GlobalConstants.ReporterFirstName,
                LastName = GlobalConstants.ReporterLastName,
                EmailConfirmed = true,
                PhoneNumber = GlobalConstants.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, GlobalConstants.ReporterPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.ReporterRoleName);
            }
        }

        private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                Email = GlobalConstants.AdminEmailName,
                UserName = GlobalConstants.AdminUsername,
                FirstName = GlobalConstants.AdminFirstName,
                LastName = GlobalConstants.AdminLastName,
                EmailConfirmed = true,
                PhoneNumber = GlobalConstants.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, GlobalConstants.AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
