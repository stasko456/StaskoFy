using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.DataAccess.Configurations
{
    public class PlaylistSongConfiguration : IEntityTypeConfiguration<PlaylistSong>
    {
        public void Configure(EntityTypeBuilder<PlaylistSong> builder)
        {
            builder.HasData(
                new PlaylistSong { PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("06111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("07111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("10111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("20111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("15111111-1111-1111-1111-111111111111") },

                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("02111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("04111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("05111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("23111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("26111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("27111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("28111111-1111-1111-1111-111111111111") },

                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("15111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("20111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("16111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("02111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("03111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("14111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("22111111-1111-1111-1111-111111111111") },
                new PlaylistSong { PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("24111111-1111-1111-1111-111111111111") });
        }
    }
}
