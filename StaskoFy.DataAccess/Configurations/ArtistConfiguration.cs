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
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasData(
                new Artist { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("01111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("21111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("02111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("31111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("03111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("41111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("04111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("51111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("05111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("61111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("06111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("71111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("07111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("81111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("08111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("91111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("09111111-1111-1111-1111-111111111111"), },
                new Artist { Id = Guid.Parse("10111111-1111-1111-1111-111111111111"), UserId = Guid.Parse("10111111-1111-1111-1111-111111111111"), });
        }
    }
}
