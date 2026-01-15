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
                new Artist
                {
                    UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("21111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("31111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("41111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("51111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("61111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("71111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("81111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("91111111-1111-1111-1111-111111111111")
                },
                new Artist
                {
                    UserId = Guid.Parse("10111111-1111-1111-1111-111111111111")
                });
        }
    }
}
