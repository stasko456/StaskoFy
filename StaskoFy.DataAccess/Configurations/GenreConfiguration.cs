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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                // Approved genres
                new Genre { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Hip-Hop", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("21111111-1111-1111-1111-111111111111"), Name = "Mumble Rap", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("31111111-1111-1111-1111-111111111111"), Name = "Trap", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("41111111-1111-1111-1111-111111111111"), Name = "Hypertrap", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("51111111-1111-1111-1111-111111111111"), Name = "Boom Bap", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("61111111-1111-1111-1111-111111111111"), Name = "House", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("14111111-1111-1111-1111-111111111111"), Name = "Soul", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("15111111-1111-1111-1111-111111111111"), Name = "Opera", Status = UploadStatus.Approved },
                new Genre { Id = Guid.Parse("16111111-1111-1111-1111-111111111111"), Name = "Lo-fi", Status = UploadStatus.Approved },

                // Deleted genres
                new Genre { Id = Guid.Parse("71111111-1111-1111-1111-111111111111"), Name = "Jazz", Status = UploadStatus.Deleted },
                new Genre { Id = Guid.Parse("81111111-1111-1111-1111-111111111111"), Name = "Pop", Status = UploadStatus.Deleted },
                new Genre { Id = Guid.Parse("91111111-1111-1111-1111-111111111111"), Name = "Pop-Folk", Status = UploadStatus.Deleted },
                new Genre { Id = Guid.Parse("10111111-1111-1111-1111-111111111111"), Name = "Rock", Status = UploadStatus.Deleted },
                new Genre { Id = Guid.Parse("11011111-1111-1111-1111-111111111111"), Name = "Metal", Status = UploadStatus.Deleted },
                new Genre { Id = Guid.Parse("12111111-1111-1111-1111-111111111111"), Name = "Indie Pop", Status = UploadStatus.Deleted },
                new Genre { Id = Guid.Parse("13111111-1111-1111-1111-111111111111"), Name = "Country", Status = UploadStatus.Deleted });
        }
    }
}
