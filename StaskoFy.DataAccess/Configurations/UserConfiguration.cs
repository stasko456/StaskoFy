using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS = StaskoFy.Models.Entities;

namespace StaskoFy.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<MODELS.User>
    {
        public void Configure(EntityTypeBuilder<MODELS.User> builder)
        {
            PasswordHasher<MODELS.User> passwordHasher = new PasswordHasher<MODELS.User>();

            var usersToSeed = new[]
            {
                // Artists:
                new { Id = Guid.Parse("01111111-1111-1111-1111-111111111111"), UserName = "kenCarson", Email = "kenCarson@gmail.com", Password = "kCarson1234*", SecurityStamp = Guid.Parse("01111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("02111111-1111-1111-1111-111111111111"), UserName = "future", Email = "future@gmail.com", Password = "Future1234*", SecurityStamp = Guid.Parse("02111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("03111111-1111-1111-1111-111111111111"), UserName = "youngThug", Email = "youngThug@gmail.com", Password = "yThug1234*" , SecurityStamp = Guid.Parse("03111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.pnguser-pfp"},
                new { Id = Guid.Parse("04111111-1111-1111-1111-111111111111"), UserName = "westsideGunn", Email = "westsideGunn@gmail.com", Password = "wGunn1234*" , SecurityStamp = Guid.Parse("04111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp .png"},
                new { Id = Guid.Parse("05111111-1111-1111-1111-111111111111"), UserName = "tylerTheCreator", Email = "tylerTheCreator@gmail.com", Password = "ttCreator1234*" , SecurityStamp = Guid.Parse("05111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png" },
                new { Id = Guid.Parse("06111111-1111-1111-1111-111111111111"), UserName = "destroyLonely", Email = "destroyLonely@gmail.com", Password = "dLonely1234*" , SecurityStamp = Guid.Parse("06111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("07111111-1111-1111-1111-111111111111"), UserName = "joeyBada$$", Email = "joeyBada$$@gmail.com", Password = "jBadass1234*" , SecurityStamp = Guid.Parse("07111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("08111111-1111-1111-1111-111111111111"), UserName = "billiEssco", Email = "billiEssco@gmail.com", Password = "bEssco1234*" , SecurityStamp = Guid.Parse("08111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("09111111-1111-1111-1111-111111111111"), UserName = "lilWayne", Email = "lilWayne@gmail.com", Password = "lWayne1234*" , SecurityStamp = Guid.Parse("09111111-1111-1111-1111-111111111111"),ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("10111111-1111-1111-1111-111111111111"), UserName = "homixideGang", Email = "homixideGang@gmail.com", Password = "hGang1234*", SecurityStamp = Guid.Parse("10111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},

                // Users:
                new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), UserName = "stasko456", Email = "stdimov2007@gmail.com", Password = "Stasko1234*", SecurityStamp = Guid.Parse("11111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("12111111-1111-1111-1111-111111111111"), UserName = "simon333", Email = "simon2403e8@gmail.com", Password = "Simon1234*", SecurityStamp = Guid.Parse("12111111-1111-1111-1111-111111111111"), ImageURL = "images/defaults/default-user-pfp.png"},
                new { Id = Guid.Parse("13111111-1111-1111-1111-111111111111"), UserName = "n_peew07", Email = "nikolaPeew@gmail.com", Password = "Nikola1234*", SecurityStamp = Guid.Parse("13111111-1111-1111-1111-111111111111"), ImageURL = "/images/defaults/default-user-pfp.png"}
            };

            var users = new List<User>();

            for (int i = 0; i < usersToSeed.Length; i++)
            {
                MODELS.User user = new()
                {
                    Id = usersToSeed[i].Id,
                    UserName = usersToSeed[i].UserName,
                    Email = usersToSeed[i].Email,
                    SecurityStamp = usersToSeed[i].SecurityStamp.ToString(),
                    ImageURL = usersToSeed[i].ImageURL,
                };

                string password = passwordHasher.HashPassword(user, usersToSeed[i].Password);

                user.PasswordHash = password;
                user.NormalizedEmail = user.Email.Normalize();
                user.NormalizedUserName = user.UserName.Normalize();
                users.Add(user);
            }

            builder.HasData(users);
        }
    }
}
