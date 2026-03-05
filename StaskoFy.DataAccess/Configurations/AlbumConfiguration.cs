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
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasData(
                new Album
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "A Great Chaos (Deluxe)",
                    Length = new TimeSpan(1, 5, 0),
                    ReleaseDate = new DateOnly(2024, 7, 5),
                    SongsCount = 5,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg",
                    CloudinaryPublicId = "a_great_chaos_deluxe_d2vxhf"
                },
                new Album
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                    Title = "Super Slimey",
                    Length = new TimeSpan(0, 40, 49),
                    ReleaseDate = new DateOnly(2017, 10, 20),
                    SongsCount = 6,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg",
                    CloudinaryPublicId = "super_slimey_v5r2c1"
                },
                new Album
                {
                    Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                    Title = "Pray For Paris",
                    Length = new TimeSpan(0, 36, 25),
                    ReleaseDate = new DateOnly(2020, 4, 17),
                    SongsCount = 4,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg",
                    CloudinaryPublicId = "pray_for_paris_rx4tq8"
                },
                new Album
                {
                    Id = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                    Title = "CALL ME IF YOU GET LOST: The Estate Sale",
                    Length = new TimeSpan(1, 17, 0),
                    ReleaseDate = new DateOnly(2023, 3, 31),
                    SongsCount = 6,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg",
                    CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_xiqapi"
                },
                new Album
                {
                    Id = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                    Title = "XTENDED",
                    Length = new TimeSpan(1, 1, 0),
                    ReleaseDate = new DateOnly(2022, 10, 31),
                    SongsCount = 4,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg",
                    CloudinaryPublicId = "xtended_wemgwk"
                },
                new Album
                {
                    Id = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                    Title = "ᐸ/3³",
                    Length = new TimeSpan(0, 54, 32),
                    ReleaseDate = new DateOnly(2025, 9, 22),
                    SongsCount = 6, // risk, no presure, stfu, jumanji, not the mayor, soooo high // TO BE ADDED!!!
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                    CloudinaryPublicId = "broken_hearts_3_xm79ww"
                });
        }
    }
}
