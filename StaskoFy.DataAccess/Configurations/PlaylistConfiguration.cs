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
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasData(
                new Playlist
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Hip-Hop & Trap Filmar",
                    Length = new TimeSpan(0, 16, 59),
                    SongCount = 6,
                    DateCreated = new DateOnly(2024, 12, 4),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    IsPublic = true,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698406/hip-hop-trap-filmar_n5y3kx.jpg",
                    CloudinaryPublicId = "hip-hop-trap-filmar_n5y3kx"
                },
                new Playlist
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                    Title = "00PIUM Filmar",
                    Length = new TimeSpan(0, 19, 9),
                    SongCount = 7,
                    DateCreated = new DateOnly(2024, 12, 10),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    IsPublic = true,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698406/opium_filmar_ntgsib.jpg",
                    CloudinaryPublicId = "opium_filmar_ntgsib"
                },
                new Playlist
                {
                    Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                    Title = "Mnooo Cherno",
                    Length = new TimeSpan(0, 22, 29),
                    SongCount = 8,
                    DateCreated = new DateOnly(2022, 6, 27),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    IsPublic = false,
                    ImageURL = "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698405/mnooo_cherno_zrfd5j.jpg",
                    CloudinaryPublicId = "mnooo_cherno_zrfd5j"
                });


        }
    }
}
