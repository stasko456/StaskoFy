using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaskoFy.Models.Entities;

namespace StaskoFy.DataAccess
{
    public class StaskoFyDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public StaskoFyDbContext(DbContextOptions<StaskoFyDbContext> options) : base(options)
        {

        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<ArtistAlbum> ArtistsAlbums { get; set; }

        public DbSet<ArtistSong> ArtistsSongs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<LikedSongs> LikedSongs { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistSong> PlaylistsSongs { get; set; }

        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
