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

namespace StaskoFy.Core.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albumRepo;
        private readonly IRepository<Artist> artistRepo;
        private readonly IRepository<ArtistAlbum> artistAlbumRepo;

        public AlbumService(IRepository<Album> _albumRepo, IRepository<Artist> _artistRepo, IRepository<ArtistAlbum> _artistAlbumRepo)
        {
            this.albumRepo = _albumRepo;
            this.artistRepo = _artistRepo;
            this.artistAlbumRepo = _artistAlbumRepo;
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> GetAllAsync()
        {
            return await albumRepo.GetAllAttached()
                .Select(a => new AlbumIndexViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Length = a.Length,
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
                    Length = album.Length,
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
                Length = album.Length,
                ReleaseDate = album.ReleaseDate,
                SongsCount = album.SongsCount,
                ImageURL = album.ImageURL,
                Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
            };
        }

        public async Task AddAsync(AlbumCreateViewModel model, Guid userId)
        {
            var artist = await artistRepo.GetAllAttached().FirstOrDefaultAsync(x => x.UserId == userId);

            var album = new Album
            {
                Title = model.Title,
                Length = model.Length,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                // add songs after creating album
                ImageURL = model.ImageURL,
            };

            var artistAlbum = new ArtistAlbum
            {
                Artist = artist,
                Album = album,
            };

            await artistAlbumRepo.AddAsync(artistAlbum);
        }

        public async Task UpdateAsync(AlbumEditViewModel model)
        {
            var album = await albumRepo.GetByIdAsync(model.Id);

            album.Title = model.Title;
            album.Length = model.Length;
            album.ReleaseDate = model.ReleaseDate;
            album.ImageURL = model.ImageURL;
            // change artist to the album,
            // change songs to the album
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
