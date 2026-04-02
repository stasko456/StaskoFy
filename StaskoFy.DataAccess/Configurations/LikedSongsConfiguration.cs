using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.DataAccess.Configurations
{
    public class LikedSongsConfiguration : IEntityTypeConfiguration<LikedSongs>
    {
        public void Configure(EntityTypeBuilder<LikedSongs> builder)
        {
            builder.HasData(
                // ken carson's liked songs
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 1, 10) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("06111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 2, 15) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 3, 20) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("25111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 4, 05) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("34111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 4, 12) },

                // future's liked songs
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 1, 05) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("02111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 1, 22) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("04111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 2, 10) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 2, 28) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 3, 15) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("34111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 4, 01) },

                // --- YOUNG THUG (User 03...) ---
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 1, 15) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("03111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 2, 05) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("04111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 2, 20) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("06111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 3, 01) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 3, 10) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 3, 25) },
                new LikedSongs { Id = Guid.NewGuid(), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("25111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2026, 4, 02) }
            );

            // kenCarson's Liked Songs:
            //builder.HasData(
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 7, 6)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 7, 8)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("03111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("04111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 7, 8)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("04111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 9, 16)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("05111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("10111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2025, 1, 30)
            //    },

            //    //future's Liked Songs:
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("06111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2023, 12, 5)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("07111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("05111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 4, 16)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("08111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("20111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 7, 8)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("09111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("19111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 3, 8)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("10111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("27111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 9, 16)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 9, 16)
            //    },

            //    //youngThug's Liked Songs:
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("12111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2024, 11, 26)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("13111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("14111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2023, 1, 29)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("14111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2025, 12, 29)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("15111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2025, 12, 22)
            //    },
            //    new LikedSongs
            //    {
            //        Id = Guid.Parse("16111111-1111-1111-1111-111111111111"),
            //        UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
            //        SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"),
            //        DateAdded = new DateOnly(2023, 10, 29)
            //    });
        }
    }
}
