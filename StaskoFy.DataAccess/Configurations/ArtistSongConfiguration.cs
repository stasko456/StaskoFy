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
    public class ArtistSongConfiguration : IEntityTypeConfiguration<ArtistSong>
    {
        public void Configure(EntityTypeBuilder<ArtistSong> builder)
        {
            builder.HasKey(x => new { x.ArtistId, x.SongId });

            builder.HasData(
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("01111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("02111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("03111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("04111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("05111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("06111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("07111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("08111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("10111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("06111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("07111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("08111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("09111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("11111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("41111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("12111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("41111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("13111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("41111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("14111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("41111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("15111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("13111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("16111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("17111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("18111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("19111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("20111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("51111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("21111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("25111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("26111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("27111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("28111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("61111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("04111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("61111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("22111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("61111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("23111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("61111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("25111111-1111-1111-1111-111111111111") },

                new ArtistSong { ArtistId = Guid.Parse("91111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("16111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("91111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("24111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("71111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("13111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("81111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("13111111-1111-1111-1111-111111111111") },
                new ArtistSong { ArtistId = Guid.Parse("10111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("27111111-1111-1111-1111-111111111111") });
        }
    }
}
