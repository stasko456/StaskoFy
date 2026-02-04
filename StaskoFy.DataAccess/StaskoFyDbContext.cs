using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning);
            });
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(StaskoFyDbContext).Assembly);

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();

            // ArtistSong join
            builder.Entity<ArtistSong>()
                .HasKey(x => new { x.ArtistId, x.SongId });

            builder.Entity<ArtistSong>()
                .HasOne(x => x.Artist)
                .WithMany(a => a.ArtistsSongs)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Restrict); // Artist survives

            builder.Entity<ArtistSong>()
                .HasOne(x => x.Song)
                .WithMany(s => s.ArtistsSongs)
                .HasForeignKey(x => x.SongId)
                .OnDelete(DeleteBehavior.Cascade); // deletes join row

            // ArtistAlbum join
            builder.Entity<ArtistAlbum>()
                .HasOne(x => x.Artist)
                .WithMany(a => a.ArtistsAlbums)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Restrict); // Artist survives

            builder.Entity<ArtistAlbum>()
                .HasOne(x => x.Album)
                .WithMany(s => s.ArtistsAlbums)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade); // deletes join row

            // Song → Album
            builder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Restrict); // Album survives
        }
    }
}
