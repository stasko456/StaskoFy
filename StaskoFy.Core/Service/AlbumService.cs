using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StaskoFy.Core.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albumRepo;
        private readonly IRepository<Artist> artistRepo;
        private readonly IRepository<ArtistAlbum> artistAlbumRepo;
        private readonly IRepository<Song> songRepo;

        public AlbumService(IRepository<Album> _albumRepo, IRepository<Artist> _artistRepo, IRepository<ArtistAlbum> _artistAlbumRepo, IRepository<Song> _songRepo)
        {
            this.albumRepo = _albumRepo;
            this.artistRepo = _artistRepo;
            this.artistAlbumRepo = _artistAlbumRepo;
            this.songRepo = _songRepo;
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> GetAllAsync()
        {
            return await albumRepo.GetAllAttached()
                .Select(a => new AlbumIndexViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Hours = a.Length.Hours,
                    Minutes = a.Length.Minutes,
                    Seconds = a.Length.Seconds,
                    ReleaseDate = a.ReleaseDate,
                    SongsCount = a.SongsCount,
                    ImageURL = a.ImageURL,
                    Artists = a.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                    Songs = a.Songs.Select(x => x.Title).ToList()
                }).ToListAsync();
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId)
        {
            var specificArtistAlbums = new List<AlbumIndexViewModel>();

            var albums = await albumRepo.GetAllAttached()
                .Include(x => x.Songs)
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .Where(s => s.ArtistsAlbums.Any(a => a.Artist.UserId == userId))
                .ToListAsync();

            foreach (var album in albums)
            {
                var vm = new AlbumIndexViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Hours = album.Length.Hours,
                    Minutes = album.Length.Minutes,
                    Seconds = album.Length.Seconds,
                    ReleaseDate = album.ReleaseDate,
                    SongsCount = album.SongsCount,
                    ImageURL = album.ImageURL,
                    Songs = album.Songs.Select(x => x.Title).ToList(),
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
                };

                specificArtistAlbums.Add(vm);
            }

            return specificArtistAlbums;
        }

        public async Task<AlbumIndexViewModel?> GetByIdAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
                .Include(x => x.Songs)
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (album == null)
            {
                throw new KeyNotFoundException("Album not found.");
            }

            return new AlbumIndexViewModel
            {
                Id = id,
                Title = album.Title,
                Hours = album.Length.Hours,
                Minutes = album.Length.Minutes,
                Seconds = album.Length.Seconds,
                ReleaseDate = album.ReleaseDate,
                SongsCount = album.SongsCount,
                ImageURL = album.ImageURL,
                Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                Songs = album.Songs.Select(x => x.Title).ToList(),
            };
        }

        public async Task AddAsync(AlbumCreateViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached().
                FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached().
                Where(x => model.SelectedArtistIds.Contains(x.Id))
                .ToListAsync();

            var albumSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            // make new album entity
            var album = new Album
            {
                Title = model.Title,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                ImageURL = model.ImageURL,
                ArtistsAlbums = new List<ArtistAlbum>(),
            };


            // add main artist
            album.ArtistsAlbums.Add(new ArtistAlbum
            {
                Artist = mainArtist,
                Album = album,
            });

            // add featured artists
            foreach (var artist in featuredArtists)
            {
                album.ArtistsAlbums.Add(new ArtistAlbum
                {
                    Artist = artist,
                    Album = album,
                });
            }

            // add songs from the album
            foreach (var song in albumSongs)
            {
                album.Songs.Add(song);
            }

            // update album SongCount
            album.SongsCount = albumSongs.Count();

            // update album Length
            TimeSpan albumLength = TimeSpan.Zero;
            foreach (var song in albumSongs)
            {
                albumLength += song.Length;
            }
            album.Length = albumLength;

            await albumRepo.AddAsync(album);
        }

        public async Task UpdateAsync(AlbumEditViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached().
                FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached()
                .Where(x => model.SelectedArtistIds.Contains(x.Id))
                .ToListAsync();

            var albumSongs = await songRepo.GetAllAttached()
                .Include(x => x.Genre)
                .Include(x => x.Album)
                .Include(x => x.ArtistsSongs)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            var album = await albumRepo.GetByIdAsync(model.Id);

            album.Title = model.Title;
            album.ReleaseDate = model.ReleaseDate;
            album.ImageURL = model.ImageURL;

            // add featured artists to the album if any are selected
            if (featuredArtists.Count > 0)
            {
                // remove ArtistAlbums for this album from the DB 
                var artistsAlbum = await artistAlbumRepo.GetAllAttached()
                    .Where(x => x.AlbumId == album.Id)
                    .ToListAsync();
                await artistAlbumRepo.RemoveRangeAsync(artistsAlbum);

                // add main artist to the album
                album.ArtistsAlbums.Add(new ArtistAlbum
                {
                    ArtistId = mainArtist.Id,
                    AlbumId = album.Id,
                });

                // add featured artists to the album
                foreach (var artist in featuredArtists)
                {
                    album.ArtistsAlbums.Add(new ArtistAlbum
                    {
                        ArtistId = artist.Id,
                        AlbumId = album.Id,
                    });
                }
            }

            // add songs to the album if any are selected
            if (albumSongs.Count > 0)
            {
                // delete songs from album
                album.Songs.Clear();

                // add new songs to album
                foreach (var song in albumSongs)
                {
                    album.Songs.Add(song);
                }

                // update SongCount
                album.SongsCount = albumSongs.Count();

                // update Length
                TimeSpan albumLength = TimeSpan.Zero;
                foreach (var song in albumSongs)
                {
                    albumLength += song.Length;
                }
                album.Length = albumLength;
            }

            // update entity
            await albumRepo.UpdateAsync(album);
        }

        public async Task RemoveAsync(Guid id)
        {
            var album = await albumRepo.GetByIdAsync(id);

            await albumRepo.RemoveAsync(album);
        }

        public async Task RemoveRangeAsync(IEnumerable<Guid> ids)
        {
            List<Album> albumsToRemove = new List<Album>();

            foreach (var id in ids)
            {
                var song = await albumRepo.GetByIdAsync(id);

                albumsToRemove.Add(song);
            }

            await albumRepo.RemoveRangeAsync(albumsToRemove);
        }
    }
}