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
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasData(
            // A GREAT CHAOS (DELUXE)
            new Song
            {
                Id = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                Title = "Green Room",
                Length = new TimeSpan(0, 3, 8),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 10044245,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                Title = "Lose It",
                Length = new TimeSpan(0, 2, 20),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 643545,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },

            new Song
            {
                Id = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                Title = "Me N My Kup",
                Length = new TimeSpan(0, 3, 54),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 234566,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("04111111-1111-1111-1111-111111111111"),
                Title = "Paranoid",
                Length = new TimeSpan(0, 2, 7),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 8384754,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("05111111-1111-1111-1111-111111111111"),
                Title = "ss",
                Length = new TimeSpan(0, 3, 4),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 3647835,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },

            // Super Slimey
            new Song
            {
                Id = Guid.Parse("06111111-1111-1111-1111-111111111111"),
                Title = "No Cap",
                Length = new TimeSpan(0, 2, 24),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 125467,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("07111111-1111-1111-1111-111111111111"),
                Title = "All da Smoke",
                Length = new TimeSpan(0, 3, 24),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 125467,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("08111111-1111-1111-1111-111111111111"),
                Title = "200",
                Length = new TimeSpan(0, 2, 26),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 125467,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("09111111-1111-1111-1111-111111111111"),
                Title = "Cruise Ship",
                Length = new TimeSpan(0, 2, 46),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 125467,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("10111111-1111-1111-1111-111111111111"),
                Title = "Feed Me Dope",
                Length = new TimeSpan(0, 2, 46),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 235678,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Killed Before",
                Length = new TimeSpan(0, 3, 40),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 98765,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },

            // Pray For Paris
            new Song
            {
                Id = Guid.Parse("12111111-1111-1111-1111-111111111111"),
                Title = "No Vacancy",
                Length = new TimeSpan(0, 1, 35),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 123456,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("13111111-1111-1111-1111-111111111111"),
                Title = "327",
                Length = new TimeSpan(0, 5, 49),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 654321,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("14111111-1111-1111-1111-111111111111"),
                Title = "Euro Step",
                Length = new TimeSpan(0, 1, 49),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 234567,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("15111111-1111-1111-1111-111111111111"),
                Title = "Versace",
                Length = new TimeSpan(0, 2, 4),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 345678,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },

            // CALL ME IF YOU GET LOST: THE ESTATE SALE
            new Song
            {
                Id = Guid.Parse("16111111-1111-1111-1111-111111111111"),
                Title = "HOT WIND BLOWS",
                Length = new TimeSpan(0, 2, 35),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 456789,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("17111111-1111-1111-1111-111111111111"),
                Title = "WILSHIRE",
                Length = new TimeSpan(0, 8, 35),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 567890,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("18111111-1111-1111-1111-111111111111"),
                Title = "SAFARI",
                Length = new TimeSpan(0, 2, 57),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 678901,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("19111111-1111-1111-1111-111111111111"),
                Title = "WHAT A DAY",
                Length = new TimeSpan(0, 3, 36),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 789012,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("20111111-1111-1111-1111-111111111111"),
                Title = "DOGTOOTH",
                Length = new TimeSpan(0, 2, 41),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 890123,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Title = "HEAVEN TO ME",
                Length = new TimeSpan(0, 3, 50),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 901234,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },

            // NULL AlbumId songs
            new Song
            {
                Id = Guid.Parse("22111111-1111-1111-1111-111111111111"),
                Title = "Bane",
                Length = new TimeSpan(0, 2, 20),
                ReleaseDate = new DateOnly(2019, 7, 30),
                AlbumId = null,
                Likes = 12345,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("23111111-1111-1111-1111-111111111111"),
                Title = "if looks could kill",
                Length = new TimeSpan(0, 3, 14),
                ReleaseDate = new DateOnly(2023, 3, 3),
                AlbumId = null,
                Likes = 23456,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("24111111-1111-1111-1111-111111111111"),
                Title = "Kat Food",
                Length = new TimeSpan(0, 4, 46),
                ReleaseDate = new DateOnly(2023, 9, 1),
                AlbumId = null,
                Likes = 34567,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },

            // XTENDED
            new Song
            {
                Id = Guid.Parse("25111111-1111-1111-1111-111111111111"),
                Title = "MDMA",
                Length = new TimeSpan(0, 3, 48),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 45678,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {

                Id = Guid.Parse("26111111-1111-1111-1111-111111111111"),
                Title = "Freestyle 2",
                Length = new TimeSpan(0, 2, 18),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 78901,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("27111111-1111-1111-1111-111111111111"),
                Title = "Delinquent",
                Length = new TimeSpan(0, 2, 45),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 56789,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            },
            new Song
            {
                Id = Guid.Parse("28111111-1111-1111-1111-111111111111"),
                Title = "Fashion Habits",
                Length = new TimeSpan(0, 3, 21),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 67890,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png"
            });
        }
    }
}
