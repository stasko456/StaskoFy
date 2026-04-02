using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
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
                Likes = 6,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg",
                CloudinaryPublicId = "a_great_chaos_deluxe_d2vxhf",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                Title = "Lose It",
                Length = new TimeSpan(0, 2, 20),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 3,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg",
                CloudinaryPublicId = "a_great_chaos_deluxe_d2vxhf",
                Status = UploadStatus.Approved
            },

            new Song
            {
                Id = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                Title = "Me N My Kup",
                Length = new TimeSpan(0, 3, 54),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 2,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg",
                CloudinaryPublicId = "a_great_chaos_deluxe_d2vxhf",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("04111111-1111-1111-1111-111111111111"),
                Title = "Paranoid",
                Length = new TimeSpan(0, 2, 7),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 4,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg",
                CloudinaryPublicId = "a_great_chaos_deluxe_d2vxhf",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("05111111-1111-1111-1111-111111111111"),
                Title = "ss",
                Length = new TimeSpan(0, 3, 4),
                ReleaseDate = new DateOnly(2024, 7, 5),
                AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg",
                CloudinaryPublicId = "a_great_chaos_deluxe_d2vxhf",
                Status = UploadStatus.Approved
            },

            // Super Slimey
            new Song
            {
                Id = Guid.Parse("06111111-1111-1111-1111-111111111111"),
                Title = "No Cap",
                Length = new TimeSpan(0, 2, 24),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 3,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                CloudinaryPublicId = "super_slimey_v5r2c1",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("07111111-1111-1111-1111-111111111111"),
                Title = "All da Smoke",
                Length = new TimeSpan(0, 3, 24),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                CloudinaryPublicId = "super_slimey_v5r2c1",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("08111111-1111-1111-1111-111111111111"),
                Title = "200",
                Length = new TimeSpan(0, 2, 26),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 5,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                CloudinaryPublicId = "super_slimey_v5r2c1",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("09111111-1111-1111-1111-111111111111"),
                Title = "Cruise Ship",
                Length = new TimeSpan(0, 2, 46),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                CloudinaryPublicId = "super_slimey_v5r2c1",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("10111111-1111-1111-1111-111111111111"),
                Title = "Feed Me Dope",
                Length = new TimeSpan(0, 2, 46),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                CloudinaryPublicId = "super_slimey_v5r2c1",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Killed Before",
                Length = new TimeSpan(0, 3, 40),
                ReleaseDate = new DateOnly(2017, 10, 20),
                AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                CloudinaryPublicId = "super_slimey_v5r2c1",
                Status = UploadStatus.Approved
            },

            // Pray For Paris
            new Song
            {
                Id = Guid.Parse("12111111-1111-1111-1111-111111111111"),
                Title = "No Vacancy",
                Length = new TimeSpan(0, 1, 35),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg",
                CloudinaryPublicId = "pray_for_paris_rx4tq8",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("13111111-1111-1111-1111-111111111111"),
                Title = "327",
                Length = new TimeSpan(0, 5, 49),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg",
                CloudinaryPublicId = "pray_for_paris_rx4tq8",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("14111111-1111-1111-1111-111111111111"),
                Title = "Euro Step",
                Length = new TimeSpan(0, 1, 49),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg",
                CloudinaryPublicId = "pray_for_paris_rx4tq8",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("15111111-1111-1111-1111-111111111111"),
                Title = "Versace",
                Length = new TimeSpan(0, 2, 4),
                ReleaseDate = new DateOnly(2020, 4, 17),
                AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg",
                CloudinaryPublicId = "pray_for_paris_rx4tq8",
                Status = UploadStatus.Approved
            },

            // CALL ME IF YOU GET LOST: THE ESTATE SALE
            new Song
            {
                Id = Guid.Parse("16111111-1111-1111-1111-111111111111"),
                Title = "HOT WIND BLOWS",
                Length = new TimeSpan(0, 2, 35),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("17111111-1111-1111-1111-111111111111"),
                Title = "WILSHIRE",
                Length = new TimeSpan(0, 8, 35),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("18111111-1111-1111-1111-111111111111"),
                Title = "SAFARI",
                Length = new TimeSpan(0, 2, 57),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("19111111-1111-1111-1111-111111111111"),
                Title = "WHAT A DAY",
                Length = new TimeSpan(0, 3, 36),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("20111111-1111-1111-1111-111111111111"),
                Title = "DOGTOOTH",
                Length = new TimeSpan(0, 2, 41),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                Title = "HEAVEN TO ME",
                Length = new TimeSpan(0, 3, 50),
                ReleaseDate = new DateOnly(2023, 3, 31),
                AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi",
                Status = UploadStatus.Approved
            },

            // NULL AlbumId songs
            new Song
            {
                Id = Guid.Parse("22111111-1111-1111-1111-111111111111"),
                Title = "Bane",
                Length = new TimeSpan(0, 2, 20),
                ReleaseDate = new DateOnly(2019, 7, 30),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/bane_v4s8f8.jpg",
                CloudinaryPublicId = "bane_v4s8f8",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("23111111-1111-1111-1111-111111111111"),
                Title = "if looks could kill",
                Length = new TimeSpan(0, 3, 14),
                ReleaseDate = new DateOnly(2023, 3, 3),
                AlbumId = null,
                Likes = 5,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/if_looks_could_kill_qesda4.jpg",
                CloudinaryPublicId = "if_looks_could_kill_qesda4",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("24111111-1111-1111-1111-111111111111"),
                Title = "Kat Food",
                Length = new TimeSpan(0, 4, 46),
                ReleaseDate = new DateOnly(2023, 9, 1),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698282/kat_food_z2ime5.jpg",
                CloudinaryPublicId = "kat_food_z2ime5",
                Status = UploadStatus.Approved
            },

            new Song
            {
                Id = Guid.Parse("29111111-1111-1111-1111-111111111111"),
                Title = "Lord Of Chaos",
                Length = new TimeSpan(0, 2, 1),
                ReleaseDate = new DateOnly(2025, 4, 11),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png",
                CloudinaryPublicId = "",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("30111111-1111-1111-1111-111111111111"),
                Title = "Money Spread",
                Length = new TimeSpan(0, 1, 45),
                ReleaseDate = new DateOnly(2025, 4, 11),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png",
                CloudinaryPublicId = "",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                Title = "Trap Jump",
                Length = new TimeSpan(0, 2, 30),
                ReleaseDate = new DateOnly(2025, 4, 11),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png",
                CloudinaryPublicId = "",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("32111111-1111-1111-1111-111111111111"),
                Title = "Blakk Rokkstar",
                Length = new TimeSpan(0, 3, 50),
                ReleaseDate = new DateOnly(2025, 4, 11),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png",
                CloudinaryPublicId = "",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("33111111-1111-1111-1111-111111111111"),
                Title = "LiveLeak",
                Length = new TimeSpan(0, 3, 10),
                ReleaseDate = new DateOnly(2025, 4, 11),
                AlbumId = null,
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "/images/defaults/default-song-cover-art.png",
                CloudinaryPublicId = "",
                Status = UploadStatus.Approved
            },
            // XTENDED
            new Song
            {
                Id = Guid.Parse("25111111-1111-1111-1111-111111111111"),
                Title = "MDMA",
                Length = new TimeSpan(0, 3, 48),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 3,
                GenreId = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg",
                CloudinaryPublicId = "xtended_wemgwk",
                Status = UploadStatus.Approved
            },
            new Song
            {

                Id = Guid.Parse("26111111-1111-1111-1111-111111111111"),
                Title = "Freestyle 2",
                Length = new TimeSpan(0, 2, 18),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg",
                CloudinaryPublicId = "xtended_wemgwk",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("27111111-1111-1111-1111-111111111111"),
                Title = "Delinquent",
                Length = new TimeSpan(0, 2, 45),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg",
                CloudinaryPublicId = "xtended_wemgwk",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("28111111-1111-1111-1111-111111111111"),
                Title = "Fashion Habits",
                Length = new TimeSpan(0, 3, 21),
                ReleaseDate = new DateOnly(2022, 10, 31),
                AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg",
                CloudinaryPublicId = "xtended_wemgwk",
                Status = UploadStatus.Approved
            },
            // ᐸ/3³
            new Song
            {
                Id = Guid.Parse("34111111-1111-1111-1111-111111111111"),
                Title = "risk",
                Length = new TimeSpan(0, 3, 0),
                ReleaseDate = new DateOnly(2025, 9, 22),
                AlbumId = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                Likes = 4,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                CloudinaryPublicId = "broken_hearts_3_xm79ww",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("35111111-1111-1111-1111-111111111111"),
                Title = "no presure",
                Length = new TimeSpan(0, 2, 18),
                ReleaseDate = new DateOnly(2025, 9, 22),
                AlbumId = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                CloudinaryPublicId = "broken_hearts_3_xm79ww",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("36111111-1111-1111-1111-111111111111"),
                Title = "stfu",
                Length = new TimeSpan(0, 3, 21),
                ReleaseDate = new DateOnly(2025, 9, 22),
                AlbumId = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                CloudinaryPublicId = "broken_hearts_3_xm79ww",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("37111111-1111-1111-1111-111111111111"),
                Title = "jumanji",
                Length = new TimeSpan(0, 2, 17),
                ReleaseDate = new DateOnly(2025, 9, 22),
                AlbumId = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                CloudinaryPublicId = "broken_hearts_3_xm79ww",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("38111111-1111-1111-1111-111111111111"),
                Title = "not the mayor",
                Length = new TimeSpan(0, 2, 25),
                ReleaseDate = new DateOnly(2025, 9, 22),
                AlbumId = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                CloudinaryPublicId = "broken_hearts_3_xm79ww",
                Status = UploadStatus.Approved
            },
            new Song
            {
                Id = Guid.Parse("39111111-1111-1111-1111-111111111111"),
                Title = "soooo high",
                Length = new TimeSpan(0, 3, 51),
                ReleaseDate = new DateOnly(2025, 9, 22),
                AlbumId = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                Likes = 0,
                GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                CloudinaryPublicId = "broken_hearts_3_xm79ww",
                Status = UploadStatus.Approved
            });
        }
    }
}
