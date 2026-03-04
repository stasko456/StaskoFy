using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Song;
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
        private readonly IImageService imageService;

        public AlbumService(IRepository<Album> _albumRepo, IRepository<Artist> _artistRepo, IRepository<ArtistAlbum> _artistAlbumRepo, IRepository<Song> _songRepo, IImageService _imageService)
        {
            this.albumRepo = _albumRepo;
            this.artistRepo = _artistRepo;
            this.artistAlbumRepo = _artistAlbumRepo;
            this.songRepo = _songRepo;
            this.imageService = _imageService;
        }

        //public async Task<IEnumerable<AlbumIndexViewModel>> GetAlbumsAsync()
        //{
        //    return await albumRepo.GetAllAttached()
        //        .Select(a => new AlbumIndexViewModel
        //        {
        //            Id = a.Id,
        //            Title = a.Title,
        //            Hours = a.Length.Hours,
        //            Minutes = a.Length.Minutes,
        //            Seconds = a.Length.Seconds,
        //            ReleaseDate = a.ReleaseDate,
        //            SongsCount = a.SongsCount,
        //            ImageURL = a.ImageURL,
        //            Artists = a.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
        //        }).ToListAsync();
        //}

        public async Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId)
        {
            return await albumRepo.GetAllAttached()
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .Where(s => s.ArtistsAlbums.Any(a => a.Artist.UserId == userId))
                .Select(album => new AlbumIndexViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Hours = album.Length.Hours,
                    Minutes = album.Length.Minutes,
                    Seconds = album.Length.Seconds,
                    ReleaseDate = album.ReleaseDate,
                    SongsCount = album.SongsCount,
                    ImageURL = album.ImageURL,
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                })
                .ToListAsync();
        }

        public async Task<AlbumIndexViewModel?> GetAlbumByIdAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (album == null)
            {
                return null;
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
            };
        }

        public async Task<AlbumSongsIndexViewModel?> GetAlbumByIdWithSongsAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
            .Include(a => a.Songs)
                .ThenInclude(s => s.Genre)
            .Include(a => a.Songs)
                .ThenInclude(s => s.ArtistsSongs)
            .ThenInclude(sa => sa.Artist)
                .ThenInclude(ar => ar.User)
            .Include(a => a.ArtistsAlbums)
                .ThenInclude(aa => aa.Artist)
                    .ThenInclude(ar => ar.User)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (album == null)
            {
                return null;
            }

            List<Guid> areAuthorized = await artistRepo.GetAllAttached()
                .Where(x => x.ArtistsAlbums.Any(aa => aa.AlbumId == album.Id))
                .Select(x => x.UserId)
                .ToListAsync();

            return new AlbumSongsIndexViewModel
            {
                Id = id,
                UserIds = areAuthorized,
                Title = album.Title,
                Hours = album.Length.Hours,
                Minutes = album.Length.Minutes,
                Seconds = album.Length.Seconds,
                ReleaseDate = album.ReleaseDate,
                SongsCount = album.SongsCount,
                ImageURL = album.ImageURL,
                Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                Songs = album.Songs.Select(s => new SongAlbumIndexViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Minutes = s.Length.Minutes,
                    Seconds = s.Length.Seconds,
                    Genre = s.Genre.Name,
                    Artists = s.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                }).ToList(),
            };
        }

        public async Task AddAlbumAsync(AlbumCreateViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached().
                FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached().
                Where(x => model.SelectedArtistIds.Contains(x.Id))
                .ToListAsync();

            var albumSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            string imageURL = "";
            string publicId = "";

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // Artist uploaded a cover → use Cloudinary
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.Title, "albums");
                imageURL = uploadResult.Url;
                publicId = uploadResult.PublicId;
            }
            else
            {
                // No upload → use default cover
                imageURL = "/images/defaults/default-album-cover-art.png";
                publicId = ""; // No publicId because we didn’t upload
            }

            // make new album entity
            var album = new Album
            {
                Title = model.Title,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                ImageURL = imageURL,
                CloudinaryPublicId = publicId,
                ArtistsAlbums = new List<ArtistAlbum>()
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
                song.ImageURL = album.ImageURL;
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

            // add album to DB
            await albumRepo.AddAsync(album);
        }

        public async Task UpdateAlbumAsync(AlbumEditViewModel model, Guid userId)
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

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // delete image from Cloudinary
                await imageService.DestroyImageAsync(album.CloudinaryPublicId);

                // Artist uploaded a cover → use Cloudinary
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.Title, "albums");
                album.ImageURL = uploadResult.Url;
                album.CloudinaryPublicId = uploadResult.PublicId;
            }

            album.Title = model.Title;
            album.ReleaseDate = model.ReleaseDate;

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
                    song.ImageURL = album.ImageURL;
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

        public async Task RemoveAlbumAsync(Guid id)
        {
            var album = await albumRepo.GetByIdAsync(id);

            await albumRepo.RemoveAsync(album);
        }

        public async Task RemoveAlbumsRangeAsync(IEnumerable<Guid> ids)
        {
            List<Album> albumsToRemove = new List<Album>();

            foreach (var id in ids)
            {
                var song = await albumRepo.GetByIdAsync(id);

                albumsToRemove.Add(song);
            }

            await albumRepo.RemoveRangeAsync(albumsToRemove);
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsAsync(string searchItem, List<string> filters)
        {
            var query = albumRepo.GetAllAttached();

            if (!string.IsNullOrEmpty(searchItem) && filters != null && filters.Any())
            {
                query = query.Where(s =>
                    (filters.Contains("Title") && EF.Functions.Like(s.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Artist") && s.ArtistsAlbums.Any(a => EF.Functions.Like(a.Artist.User.UserName, $"%{searchItem}%")))
                );
            }

            return await query
                .Select(album => new AlbumIndexViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Hours = album.Length.Hours,
                    Minutes = album.Length.Minutes,
                    Seconds = album.Length.Seconds,
                    ReleaseDate = album.ReleaseDate,
                    SongsCount = album.SongsCount,
                    ImageURL = album.ImageURL,
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters)
        {
            var query = albumRepo.GetAllAttached()
            .Where(s => s.ArtistsAlbums.Any(x => x.Artist.UserId == userId));

            if (!string.IsNullOrEmpty(searchItem) && filters != null && filters.Any())
            {
                query = query.Where(s =>
                    (filters.Contains("Title") && EF.Functions.Like(s.Title, $"%{searchItem}%"))
                );
            }

            return await query
                .Select(album => new AlbumIndexViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Hours = album.Length.Hours,
                    Minutes = album.Length.Minutes,
                    Seconds = album.Length.Seconds,
                    ReleaseDate = album.ReleaseDate,
                    SongsCount = album.SongsCount,
                    ImageURL = album.ImageURL,
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }
    }
}