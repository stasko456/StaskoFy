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
                new { Id = Guid.Parse("01111111-1111-1111-1111-111111111111"), UserName = "kenCarson", Email = "kenCarson@gmail.com", Password = "kCarson1234*", SecurityStamp = Guid.Parse("01111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/ken_carson_amg20i.jpg", CloudinaryPublicId = "ken_carson_amg20i"},
                new { Id = Guid.Parse("02111111-1111-1111-1111-111111111111"), UserName = "future", Email = "future@gmail.com", Password = "Future1234*", SecurityStamp = Guid.Parse("02111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/future_pbmahw.jpg", CloudinaryPublicId = "future_pbmahw"},
                new { Id = Guid.Parse("03111111-1111-1111-1111-111111111111"), UserName = "youngThug", Email = "youngThug@gmail.com", Password = "yThug1234*" , SecurityStamp = Guid.Parse("03111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698511/young_thug_wz2fln.jpg", CloudinaryPublicId = "young_thug_wz2fln"},
                new { Id = Guid.Parse("04111111-1111-1111-1111-111111111111"), UserName = "westsideGunn", Email = "westsideGunn@gmail.com", Password = "wGunn1234*" , SecurityStamp = Guid.Parse("04111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698510/westside_gunn_vm7xf2.jpg", CloudinaryPublicId = "westside_gunn_vm7xf2"},
                new { Id = Guid.Parse("05111111-1111-1111-1111-111111111111"), UserName = "tylerTheCreator", Email = "tylerTheCreator@gmail.com", Password = "ttCreator1234*" , SecurityStamp = Guid.Parse("05111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698509/tyler_the_creator_i9yhnu.jpg" , CloudinaryPublicId = "tyler_the_creator_i9yhnu"},
                new { Id = Guid.Parse("06111111-1111-1111-1111-111111111111"), UserName = "destroyLonely", Email = "destroyLonely@gmail.com", Password = "dLonely1234*" , SecurityStamp = Guid.Parse("06111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698502/destroy_lonely_hmhymx.jpg", CloudinaryPublicId = "destroy_lonely_hmhymx"},
                new { Id = Guid.Parse("07111111-1111-1111-1111-111111111111"), UserName = "joeyBada$$", Email = "joeyBada$$@gmail.com", Password = "jBadass1234*" , SecurityStamp = Guid.Parse("07111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698505/joey_bada_t4ig6u.jpg", CloudinaryPublicId = "joey_bada_t4ig6u"},
                new { Id = Guid.Parse("08111111-1111-1111-1111-111111111111"), UserName = "billiEssco", Email = "billiEssco@gmail.com", Password = "bEssco1234*" , SecurityStamp = Guid.Parse("08111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698501/billie_essco_tqdmip.jpg", CloudinaryPublicId = "billie_essco_tqdmip"},
                new { Id = Guid.Parse("09111111-1111-1111-1111-111111111111"), UserName = "lilWayne", Email = "lilWayne@gmail.com", Password = "lWayne1234*" , SecurityStamp = Guid.Parse("09111111-1111-1111-1111-111111111111"),ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/lil_wayne_pqbiny.jpg", CloudinaryPublicId = "lil_wayne_pqbiny"},
                new { Id = Guid.Parse("10111111-1111-1111-1111-111111111111"), UserName = "homixideGang", Email = "homixideGang@gmail.com", Password = "hGang1234*", SecurityStamp = Guid.Parse("10111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698504/homixide_gang_anf7iv.jpg", CloudinaryPublicId = "homixide_gang_anf7iv"},

                // Users:
                new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), UserName = "stasko456", Email = "stdimov2007@gmail.com", Password = "Stasko1234*", SecurityStamp = Guid.Parse("11111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/stasko456_hblwlq.jpg", CloudinaryPublicId = "stasko456_hblwlq"},
                new { Id = Guid.Parse("12111111-1111-1111-1111-111111111111"), UserName = "simon333", Email = "simon2403e8@gmail.com", Password = "Simon1234*", SecurityStamp = Guid.Parse("12111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/simon333_fafgdv.jpg", CloudinaryPublicId = "simon333_fafgdv"},
                new { Id = Guid.Parse("13111111-1111-1111-1111-111111111111"), UserName = "n_peew07", Email = "nikolaPeew@gmail.com", Password = "Nikola1234*", SecurityStamp = Guid.Parse("13111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698507/n_peew07_yoj6ay.jpg", CloudinaryPublicId = "n_peew07_yoj6ay"},
                new { Id = Guid.Parse("14111111-1111-1111-1111-111111111111"), UserName = "g_tonev", Email = "gtonev@gmail.com", Password = "Tonev1234*", SecurityStamp = Guid.Parse("14111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/gt_baby_gdk5le.jpg", CloudinaryPublicId = "gt_baby_gdk5le"},
                new { Id = Guid.Parse("15111111-1111-1111-1111-111111111111"), UserName = "niksy_g", Email = "nikolaGragov@gmail.com", Password = "Gargov1234*", SecurityStamp = Guid.Parse("15111111-1111-1111-1111-111111111111"), ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1773127136/adasha_quhjni.png", CloudinaryPublicId = "adasha_quhjni"}
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
                    CloudinaryPublicId = usersToSeed[i].CloudinaryPublicId
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
