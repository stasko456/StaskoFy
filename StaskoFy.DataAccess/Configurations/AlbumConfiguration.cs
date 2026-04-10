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
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasData(
                new Album
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "A Great Chaos (Deluxe)",
                    Length = new TimeSpan(0, 14, 33),
                    ReleaseDate = new DateOnly(2024, 7, 5),
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg",
                    CloudinaryPublicId = "a_great_chaos_deluxe_nhsydj",
                    Status = UploadStatus.Approved
                },
                new Album
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                    Title = "Super Slimey",
                    Length = new TimeSpan(0, 20, 12),
                    ReleaseDate = new DateOnly(2017, 10, 20),
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg",
                    CloudinaryPublicId = "super_slimey_bb4lno",
                    Status = UploadStatus.Approved
                },
                new Album
                {
                    Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                    Title = "Pray For Paris",
                    Length = new TimeSpan(0, 11, 17),
                    ReleaseDate = new DateOnly(2020, 4, 17),
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg",
                    CloudinaryPublicId = "pray_for_paris_rx4tq8",
                    Status = UploadStatus.Approved
                },
                new Album
                {
                    Id = Guid.Parse("41111111-1111-1111-1111-111111111111"),
                    Title = "CALL ME IF YOU GET LOST: The Estate Sale",
                    Length = new TimeSpan(0, 24, 14),
                    ReleaseDate = new DateOnly(2023, 3, 31),
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg",
                    CloudinaryPublicId = "call_me_if_you_get_lost_the_estate_sale_sfd1rw",
                    Status = UploadStatus.Approved
                },
                new Album
                {
                    Id = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                    Title = "XTENDED",
                    Length = new TimeSpan(0, 12, 12),
                    ReleaseDate = new DateOnly(2022, 10, 31),
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg",
                    CloudinaryPublicId = "xtended_wemgwk",
                    Status = UploadStatus.Approved
                },
                new Album
                {
                    Id = Guid.Parse("61111111-1111-1111-1111-111111111111"),
                    Title = "ᐸ/3³",
                    Length = new TimeSpan(0, 17, 12),
                    ReleaseDate = new DateOnly(2025, 9, 22),
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg",
                    CloudinaryPublicId = "broken_hearts_3_xm79ww",
                    Status = UploadStatus.Approved
                });
        }
    }
}
