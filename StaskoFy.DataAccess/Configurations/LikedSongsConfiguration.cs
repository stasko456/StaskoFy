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
            // stasko456's Liked Songs:
            builder.HasData(
                new LikedSongs
                {
                    Id = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 7, 6)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 7, 8)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("04111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 7, 8)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("04111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 9, 16)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("05111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("10111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2025, 1, 30)
                },

                //simon333's Liked Songs:
                new LikedSongs
                {
                    Id = Guid.Parse("06111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2023, 12, 5)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("07111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("05111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 4, 16)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("08111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("20111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 7, 8)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("09111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("19111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 3, 8)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("10111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("27111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 9, 16)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 9, 16)
                },

                //n_peev07's Liked Songs:
                new LikedSongs
                {
                    Id = Guid.Parse("12111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2024, 11, 26)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("13111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("14111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2023, 1, 29)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("14111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2025, 12, 29)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("15111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("08111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2025, 12, 22)
                },
                new LikedSongs
                {
                    Id = Guid.Parse("16111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"),
                    DateAdded = new DateOnly(2023, 10, 29)
                });
        }
    }
}
