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
    public class PlaylistSongConfiguration : IEntityTypeConfiguration<PlaylistSong>
    {
        public void Configure(EntityTypeBuilder<PlaylistSong> builder)
        {
            builder.HasData(
                new PlaylistSong { Id = Guid.Parse("01111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("06111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 12, 3) },
                new PlaylistSong { Id = Guid.Parse("02111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("07111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 11, 3) },
                new PlaylistSong { Id = Guid.Parse("03111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("10111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 10, 3) },
                new PlaylistSong { Id = Guid.Parse("04111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("11111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 9, 3) },
                new PlaylistSong { Id = Guid.Parse("05111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("20111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 8, 3) },
                new PlaylistSong { Id = Guid.Parse("06111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("11111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("15111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 7, 3) },

                new PlaylistSong { Id = Guid.Parse("07111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("02111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 6, 3) },
                new PlaylistSong { Id = Guid.Parse("08111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("04111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 5, 3) },
                new PlaylistSong { Id = Guid.Parse("09111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("05111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 4, 3) },
                new PlaylistSong { Id = Guid.Parse("10111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("23111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 3, 3) },
                new PlaylistSong { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("26111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 2, 3) },
                new PlaylistSong { Id = Guid.Parse("12111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("27111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2007, 1, 3) },
                new PlaylistSong { Id = Guid.Parse("13111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("21111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("28111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 12, 3) },

                new PlaylistSong { Id = Guid.Parse("14111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("15111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 11, 3) },
                new PlaylistSong { Id = Guid.Parse("15111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("20111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 10, 3) },
                new PlaylistSong { Id = Guid.Parse("16111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("16111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 9, 3) },
                new PlaylistSong { Id = Guid.Parse("17111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("02111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 8, 3) },
                new PlaylistSong { Id = Guid.Parse("18111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("03111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 7, 3) },
                new PlaylistSong { Id = Guid.Parse("19111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("14111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 6, 3) },
                new PlaylistSong { Id = Guid.Parse("20111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("22111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 5, 3) },
                new PlaylistSong { Id = Guid.Parse("21111111-1111-1111-1111-111111111111"), PlaylistId = Guid.Parse("31111111-1111-1111-1111-111111111111"), SongId = Guid.Parse("24111111-1111-1111-1111-111111111111"), DateAdded = new DateOnly(2006, 4, 3) });
        }
    }
}
