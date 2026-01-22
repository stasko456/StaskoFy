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
                    DataCreated = new DateOnly(2024, 12, 4),
                    UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"),
                    IsPublic = true,
                    ImageURL = "/images/defaults/default-album-cover-art.png"
                },
                new Playlist
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                    Title = "00PIUMxx Filmar",
                    Length = new TimeSpan(0, 19, 9),
                    SongCount = 7,
                    DataCreated = new DateOnly(2024, 12, 10),
                    UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"),
                    IsPublic = true,
                    ImageURL = "/images/defaults/default-album-cover-art.png"
                },
                new Playlist
                {
                    Id = Guid.Parse("31111111-1111-1111-1111-111111111111"),
                    Title = "Mnooo Cherno",
                    Length = new TimeSpan(0, 22, 29),
                    SongCount = 8,
                    DataCreated = new DateOnly(2022, 6, 27),
                    UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"),
                    IsPublic = false,
                    ImageURL = "/images/defaults/default-album-cover-art.png"
                });


        }
    }
}
