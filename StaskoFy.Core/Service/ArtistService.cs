using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Artist;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> artistRepo;

        public ArtistService(IRepository<Artist> _artistRepo)
        {
            this.artistRepo = _artistRepo;
        }

        public async Task<IEnumerable<ArtistIndexWithSongsAndAlbumsViewModel>> GetArtistsAsync()
        {
            return await artistRepo.GetAllAttached()
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(album => album.Album)
                .Include(x => x.ArtistsSongs)
                    .ThenInclude(song => song.Song)
                .Select(a => new ArtistIndexWithSongsAndAlbumsViewModel
                {
                    Id = a.Id,
                    Username = a.User.UserName,
                    ProfilePicture = a.User.ImageURL,
                    Singles = a.ArtistsSongs.Where(x => x.Song.AlbumId == null).Select(x => new SongIndexViewModel
                    {
                        Id = x.Song.Id,
                        Title = x.Song.Title,
                        Minutes = x.Song.Length.Minutes,
                        Seconds = x.Song.Length.Seconds,
                        ReleaseDate = x.Song.ReleaseDate,
                        AlbumName = "Single",
                        AlbumId = x.Song.AlbumId,
                        GenreId = x.Song.GenreId,
                        GenreName = x.Song.Genre.Name,
                        ImageURL = x.Song.ImageURL,
                        CloudinaryPublicId = x.Song.CloudinaryPublicId,
                        Likes = x.Song.Likes,
                        Artists = x.Song.ArtistsSongs.Select(s => s.Artist.User.UserName).ToList()
                    }).ToList(),
                    Albums = a.ArtistsAlbums.Select(x => new AlbumSongsIndexViewModel
                    {
                        Id = x.Album.Id,
                        Title = x.Album.Title,
                        Hours = x.Album.Length.Hours,
                        Minutes = x.Album.Length.Minutes,
                        Seconds = x.Album.Length.Seconds,
                        ReleaseDate = x.Album.ReleaseDate,
                        SongsCount = x.Album.SongsCount,
                        ImageURL = x.Album.ImageURL,
                        Artists = x.Album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                        Songs = x.Album.Songs.Select(song => new SongAlbumIndexViewModel
                        {
                            Title = song.Title,
                            Minutes = song.Length.Minutes,
                            Seconds = song.Length.Seconds,
                            Genre = song.Genre.Name,
                            Artists = song.ArtistsSongs.Select(artist => artist.Artist.User.UserName).ToList(),
                        }).ToList(),
                    }).ToList(),
                }).ToListAsync();
        }

        public async Task<ArtistViewModel?> GetArtistByIdAsync(Guid id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            if (artist == null)
            {
                return null;
            }

            return new ArtistViewModel
            {
                Id = artist.Id,
                UserId = artist.UserId
            };
        }

        public async Task AddArtistAsync(ArtistViewModel model)
        {
            var artist = new Artist
            {
                UserId = model.UserId,
            };

            await artistRepo.AddAsync(artist);
        }

        public async Task RemoveArtistAsync(Guid id)
        {
            var artist = await artistRepo.GetByIdAsync(id);

            await artistRepo.RemoveAsync(artist);
        }

        public async Task<IEnumerable<ArtistSelectViewModel>> PopulateArtistSelectListAsync(Guid userId)
        {
            var artists = await artistRepo.GetAllAttached()
                .Where(a => a.UserId != userId)
                .Select(x => new ArtistSelectViewModel
                {
                    Id = x.Id,
                    Username = x.User.UserName
                }).ToListAsync();

            return artists;
        }
    }
}
