using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SifflForums.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api
{
    public static class AdminSeeder
    {
        private const string AdminUserName = "admin";
        // Development-only credentials; intentionally below the normal password policy
        private const string AdminPassword = "123";

        // Ensures the Admin role and the admin user exist. Safe to run on every startup.
        public static async Task SeedAdminAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                EnsureSucceeded(await roleManager.CreateAsync(new IdentityRole(Roles.Admin)));
            }

            var admin = await userManager.FindByNameAsync(AdminUserName);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = AdminUserName, Email = "admin@siffl.local", EmailConfirmed = true };
                // Hash directly so the weak dev password bypasses the password validators
                admin.PasswordHash = userManager.PasswordHasher.HashPassword(admin, AdminPassword);
                EnsureSucceeded(await userManager.CreateAsync(admin));
            }

            if (!await userManager.IsInRoleAsync(admin, Roles.Admin))
            {
                EnsureSucceeded(await userManager.AddToRoleAsync(admin, Roles.Admin));
            }
        }

        private static void EnsureSucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Seeding admin failed: " + string.Join(" ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
