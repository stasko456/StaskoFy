using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                    Email = adminEmail
                };

                await userManager.CreateAsync(user, "Admin1234*");
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public static async Task SeedArtistsAsync(IServiceProvider serviceProvider, StaskoFyDbContext context)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Ken Carson
            var kenCarson = new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "kenCarson",
                Email = "kenCarson@gmail.com"
            };
            await userManager.CreateAsync(kenCarson, "kCarson1234*");
            await userManager.AddToRoleAsync(kenCarson, "Artist");

            // Future
            var future = new User
            {
                Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),         
                UserName = "future",
                Email = "future@gmail.com"
            };
            await userManager.CreateAsync(future, "Future1234*");
            await userManager.AddToRoleAsync(future, "Artist");

            // Young Thug
            var youngThug = new User
            {
                Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                UserName = "youngThug",
                Email = "youngThug@gmail.com"
            };
            await userManager.CreateAsync(youngThug, "yThug1234*");
            await userManager.AddToRoleAsync(youngThug, "Artist");

            // WestSide Gunn
            var westsideGunn = new User
            {
                Id = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                UserName = "westsideGunn",
                Email = "westsideGunn@gmail.com"
            };
            await userManager.CreateAsync(westsideGunn, "wGunn1234*");
            await userManager.AddToRoleAsync(westsideGunn, "Artist");

            // Tyler The Creator
            var tylerTheCreator = new User
            {
                Id = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                UserName = "tylerTheCreator",
                Email = "tylerTheCreator@gmail.com"
            };
            await userManager.CreateAsync(tylerTheCreator, "tTC1234*");
            await userManager.AddToRoleAsync(tylerTheCreator, "Artist");

            // Destroy Lonely
            var destroyLonely = new User
            {
                Id = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                UserName = "destroyLonely",
                Email = "destroyLonely@gmail.com"
            };
            await userManager.CreateAsync(destroyLonely, "dLonely1234*");
            await userManager.AddToRoleAsync(destroyLonely, "Artist");

            //Joe Bada$$
            var joeyBadass = new User
            {
                Id = Guid.Parse("71111111-1111-1111-1111-111111111111"),
                UserName = "joeyBada$$",
                Email = "joeyBada$$@gmail.com"
            };
            await userManager.CreateAsync(joeyBadass, "jBada$$1234*");
            await userManager.AddToRoleAsync(joeyBadass, "Artist");

            //Billie Essco
            var billiEssco = new User
            {
                Id = Guid.Parse("81111111-1111-1111-1111-111111111111"),
                UserName = "billiEssco",
                Email = "billiEssco@gmail.com"
            };
            await userManager.CreateAsync(billiEssco, "bEssco1234*");
            await userManager.AddToRoleAsync(billiEssco, "Artist");

            //Lil Wayne
            var lilWayne = new User
            {
                Id = Guid.Parse("91111111-1111-1111-1111-111111111111"),
                UserName = "lilWayne",
                Email = "lilWayne@gmail.com"
            };
            await userManager.CreateAsync(lilWayne, "lilWayne1234*");
            await userManager.AddToRoleAsync(lilWayne, "Artist");

            //Lil Wayne
            var homixideGang = new User
            {
                Id = Guid.Parse("10111111-1111-1111-1111-111111111111"),
                UserName = "homixideGang",
                Email = "homixideGang@gmail.com"
            };
            await userManager.CreateAsync(homixideGang, "homiGang1234*");
            await userManager.AddToRoleAsync(homixideGang, "Artist");
        }

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // stasko456
            var stasko456 = new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "stasko456",
                Email = "stdimov2007@gmail.com"
            };
            await userManager.CreateAsync(stasko456, "Stasko1234*");
            await userManager.AddToRoleAsync(stasko456, "User");

            // simon333
            var simon333 = new User
            {
                Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                UserName = "simon333",
                Email = "simon2403e8@gmail.com"
            };
            await userManager.CreateAsync(simon333, "Simon1234*");
            await userManager.AddToRoleAsync(simon333, "User");

            //n_peev07
            var n_peew07 = new User
            {
                Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                UserName = "n_peew07",
                Email = "nikolaPeew@gmail.com"
            };
            await userManager.CreateAsync(n_peew07, "nPeew1234*");
            await userManager.AddToRoleAsync(n_peew07, "User");
        }
    }
}