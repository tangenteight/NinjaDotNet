using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NinjaDotNet.Api.Data
{
    public static class SeedData
    {
        public static async Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("bryan@vesuviustech.com") == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "bryan",
                    Email = "bryan@vesuviustech.com"
                };
                var result = await userManager.CreateAsync(user, "Y&O@Zm&I6.fX");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,"Administrator");
                }
            }

            if (await userManager.FindByEmailAsync("user@gmail.com") == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "user",
                    Email = "user@gmail.com"
                };
                var result = await userManager.CreateAsync(user, "3fa85F64-!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                await roleManager.CreateAsync(role);
            }
        }

    }
}
