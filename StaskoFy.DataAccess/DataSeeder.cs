using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.DataAccess
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = new string[] { "Admin", "Artist", "User" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        public static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var adminUsername = "admin";
            var adminEmail = "admin@admin.com";

            var user = await userManager.FindByNameAsync(adminUsername);

            if (user == null)
            {
                user = new User
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    ImageURL = "/images/defaults/default-user-pfp.png",
                };

                await userManager.CreateAsync(user, "Admin1234*");
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public static async Task GiveRolesRemainingUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var allUsers = userManager.Users.ToList();

            int artistCount = 10;
            for (int i = 0; i < allUsers.Count; i++)
            {
                var user = allUsers[i];
                string roleToAssign = i < artistCount ? "Artist" : "User";

                if (!await userManager.IsInRoleAsync(user, roleToAssign))
                {
                    await userManager.AddToRoleAsync(user, roleToAssign);
                }
            }
        }
    }
}
