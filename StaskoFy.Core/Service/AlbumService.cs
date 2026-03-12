using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
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

        public async Task<IEnumerable<AlbumApprovalViewModel>> GetAlbumsWithPendingStatusAsync()
        {
            return await albumRepo.GetAllAttached()
                .Where(a => a.Status != UploadStatus.Approved)
                .Select(a => new AlbumApprovalViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Hours = a.Length.Hours,
                    Minutes = a.Length.Minutes,
                    Seconds = a.Length.Seconds,
                    ReleaseDate = a.ReleaseDate,
                    SongsCount = a.SongsCount,
                    ImageURL = a.ImageURL,
                    Status = a.Status,
                    Artists = a.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                }).ToListAsync();
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId)
        {
            return await albumRepo.GetAllAttached()
                .Include(x => x.ArtistsAlbums)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(u => u.User)
                .Where(s => s.ArtistsAlbums.Any(a => a.Artist.UserId == userId) && s.Status == UploadStatus.Approved)
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
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "art-covers");
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
                Status = UploadStatus.Pending,
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
            if (album.ImageURL == "/images/defaults/default-album-cover-art.png")
            {
                foreach (var song in albumSongs)
                {
                    song.ImageURL = "/images/defaults/default-song-cover-art.png";
                    song.CloudinaryPublicId = "";
                    album.Songs.Add(song);
                }
            }
            else
            {
                foreach (var song in albumSongs)
                {
                    song.ImageURL = album.ImageURL;
                    song.CloudinaryPublicId = album.CloudinaryPublicId;
                    album.Songs.Add(song);
                }
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
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            var album = await albumRepo.GetAllAttached()
            .Include(a => a.Songs)
            .Include(a => a.ArtistsAlbums)
            .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (album == null)
            {
                throw new KeyNotFoundException($"Album with ID {model.Id} is not found!");
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // delete image from Cloudinary
                await imageService.DestroyImageAsync(album.CloudinaryPublicId);

                // Artist uploaded a cover → use Cloudinary
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "art-covers");
                album.ImageURL = uploadResult.Url;
                album.CloudinaryPublicId = uploadResult.PublicId;

                // change the covers of the songs that are already in the album to the album cover
                foreach (var song in album.Songs)
                {
                    song.ImageURL = album.ImageURL;
                    song.CloudinaryPublicId = album.CloudinaryPublicId;
                }
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

            // add more songs to the album if any are selected
            if (albumSongs.Count > 0)
            {
                if (album.ImageURL == "/images/defaults/default-album-cover-art.png")
                {
                    foreach (var song in albumSongs)
                    {
                        song.ImageURL = "/images/defaults/default-song-cover-art.png";
                        song.CloudinaryPublicId = "";
                        album.Songs.Add(song);
                        album.SongsCount++;
                    }
                }
                else
                {
                    foreach (var song in albumSongs)
                    {
                        song.ImageURL = album.ImageURL;
                        song.CloudinaryPublicId = album.CloudinaryPublicId;
                        album.Songs.Add(song);
                        album.SongsCount++;
                    }
                }

                // update Length
                TimeSpan albumLength = TimeSpan.Zero;
                foreach (var song in albumSongs)
                {
                    albumLength += song.Length;
                }
                album.Length = album.Length + albumLength;
            }

            // update entity
            await albumRepo.UpdateAsync(album);
        }

        public async Task RemoveAlbumAsync(Guid id)
        {
            var album = await albumRepo.GetByIdAsync(id);

            var albumSongs = await songRepo.GetAllAttached()
                .Where(s => s.AlbumId == id)
                .ToListAsync();

            if (album == null)
            {
                throw new KeyNotFoundException($"Album with ID {id} is not found!");
            }

            // album soft delete
            album.Status = UploadStatus.Deleted;

            // destroy image from cloudinary
            await imageService.DestroyImageAsync(album.CloudinaryPublicId);

            // change image of album to default
            album.ImageURL = "/images/defaults/default-album-cover-art.png";
            album.CloudinaryPublicId = "";

            // chnage images of songs to default
            foreach (var song in albumSongs)
            {
                song.ImageURL = "/images/defaults/default-song-cover-art.png";
                song.CloudinaryPublicId = "";
                // album's songs soft delete
                song.Status = UploadStatus.Deleted;
            }

            await albumRepo.UpdateAsync(album);
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsAsync(string searchItem, List<string> filters)
        {
            var query = albumRepo.GetAllAttached()
                .Where(x => x.Status == UploadStatus.Approved);

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
            .Where(s => s.ArtistsAlbums.Any(x => x.Artist.UserId == userId) && s.Status == UploadStatus.Approved);

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

        public async Task AcceptAlbumUploadAsync(Guid id)
        {
            var album = await albumRepo.GetByIdAsync(id);

            var songs = await songRepo.GetAllAttached()
                .Where(s => s.AlbumId == id)
                .ToListAsync();

            if (album == null)
            {
                throw new KeyNotFoundException($"Album with ID {id} is not found!");
            }

            album.Status = UploadStatus.Approved;

            foreach (var song in songs)
            {
                // album's songs soft delete
                song.Status = UploadStatus.Approved;
            }

            await albumRepo.UpdateAsync(album);
        }

        public async Task RejectAlbumUploadAsync(Guid id)
        {
            var album = await albumRepo.GetByIdAsync(id);

            if (album == null)
            {
                throw new KeyNotFoundException($"Album with ID {id} is not found!");
            }

            if (album.Songs.Count > 0)
            {
                foreach (var song in album.Songs)
                {
                    song.Status = UploadStatus.Rejected;
                }
            }

            album.Status = UploadStatus.Rejected;

            await albumRepo.UpdateAsync(album);
        }
    }
}