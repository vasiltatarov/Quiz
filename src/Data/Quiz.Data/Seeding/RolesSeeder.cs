namespace Quiz.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Quiz.Common;
    using Quiz.Data.Models;

    internal class RolesSeeder : ISeeder
    {
        private const string AdminName = "Vasil@gmail.com";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedUserToRoleAsync(userManager, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserToRoleAsync(UserManager<ApplicationUser> userManager, string roleName)
        {
            var user = await userManager.FindByEmailAsync(AdminName);

            if (user == null)
            {
                return;
            }

            var isInRole = await userManager.IsInRoleAsync(user, roleName);
            if (isInRole)
            {
                return;
            }

            await userManager.AddToRoleAsync(user, roleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
