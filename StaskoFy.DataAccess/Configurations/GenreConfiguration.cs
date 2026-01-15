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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Hip-Hop" },
                new Genre { Id = Guid.Parse("21111111-1111-1111-1111-111111111111"), Name = "Mumble Rap" },
                new Genre { Id = Guid.Parse("31111111-1111-1111-1111-111111111111"), Name = "Trap" },
                new Genre { Id = Guid.Parse("41111111-1111-1111-1111-111111111111"), Name = "Hypertrap" },
                new Genre { Id = Guid.Parse("51111111-1111-1111-1111-111111111111"), Name = "Boom Bap" });
        }
    }
}
