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
    public class ArtistAlbumConfiguration : IEntityTypeConfiguration<ArtistAlbum>
    {
        public void Configure(EntityTypeBuilder<ArtistAlbum> builder)
        {
            builder.HasKey(x => new { x.ArtistId, x.AlbumId });

            builder.HasData(
                new ArtistAlbum { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), AlbumId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
                new ArtistAlbum { ArtistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111") },
                new ArtistAlbum { ArtistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), AlbumId = Guid.Parse("21111111-1111-1111-1111-111111111111") },
                new ArtistAlbum { ArtistId = Guid.Parse("41111111-1111-1111-1111-111111111111"), AlbumId = Guid.Parse("31111111-1111-1111-1111-111111111111") },
                new ArtistAlbum { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), AlbumId = Guid.Parse("41111111-1111-1111-1111-111111111111") },
                new ArtistAlbum { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), AlbumId = Guid.Parse("51111111-1111-1111-1111-111111111111") }
                );
        }
    }
}
