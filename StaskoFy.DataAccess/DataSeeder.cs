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

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            //var usersToSeed = new[]
            //{
            //    // Artists:
            //    new { Id = Guid.Parse("01111111-1111-1111-1111-111111111111"), UserName = "kenCarson", Email = "kenCarson@gmail.com", Password = "kCarson1234*", Role = "Artist" },
            //    new { Id = Guid.Parse("02111111-1111-1111-1111-111111111111"), UserName = "future", Email = "future@gmail.com", Password = "Future1234*", Role = "Artist" },
            //    new { Id = Guid.Parse("03111111-1111-1111-1111-111111111111"), UserName = "youngThug", Email = "youngThug@gmail.com", Password = "yThug1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("04111111-1111-1111-1111-111111111111"), UserName = "westsideGunn", Email = "westsideGunn@gmail.com", Password = "wGunn1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("05111111-1111-1111-1111-111111111111"), UserName = "tylerTheCreator", Email = "tylerTheCreator@gmail.com", Password = "ttCreator1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("06111111-1111-1111-1111-111111111111"), UserName = "destroyLonely", Email = "destroyLonely@gmail.com", Password = "dLonely1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("07111111-1111-1111-1111-111111111111"), UserName = "joeyBada$$", Email = "joeyBada$$@gmail.com", Password = "jBadass1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("08111111-1111-1111-1111-111111111111"), UserName = "billiEssco", Email = "billiEssco@gmail.com", Password = "bEssco1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("09111111-1111-1111-1111-111111111111"), UserName = "lilWayne", Email = "lilWayne@gmail.com", Password = "lWayne1234*", Role = "Artist"},
            //    new { Id = Guid.Parse("10111111-1111-1111-1111-111111111111"), UserName = "homixideGang", Email = "homixideGang@gmail.com", Password = "hGang1234*", Role = "Artist"},

            //    // Users:
            //    new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), UserName = "stasko456", Email = "stdimov2007@gmail.com", Password = "Stasko1234*", Role = "User"},
            //    new { Id = Guid.Parse("12111111-1111-1111-1111-111111111111"), UserName = "simon333", Email = "simon2403e8@gmail.com", Password = "Simon1234*", Role = "User"},
            //    new { Id = Guid.Parse("13111111-1111-1111-1111-111111111111"), UserName = "n_peew07", Email = "nikolaPeew@gmail.com", Password = "Nikola1234*", Role = "User"}
            //};

            //foreach (var u in usersToSeed)
            //{
            //    var userExists = userManager.FindByNameAsync(u.UserName);

            //    if (userExists == null)
            //    {
            //        var user = new User
            //        {
            //            Id = u.Id,
            //            UserName = u.UserName,
            //            Email = u.Email,
            //        };

            //        await userManager.CreateAsync(user, u.Password);
            //        await userManager.AddToRoleAsync(user, u.Role);
            //    }
            //}
        }

        //public static async Task SeedArtistsAsync(IServiceProvider serviceProvider)
        //{
        //    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        //    var context = serviceProvider.GetRequiredService<StaskoFyDbContext>();

        //    var artistUsers = await userManager.GetUsersInRoleAsync("Artist");

        //    foreach (var user in artistUsers)
        //    {
        //        if (!context.Artists.Any(x => x.UserId == user.Id))
        //        {
        //            context.Artists.Add(new Artist
        //            {
        //                Id = Guid.NewGuid(),
        //                UserId = user.Id,
        //            });
        //        }
        //    }

        //    await context.SaveChangesAsync();
        //}
    }
}