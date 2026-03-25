using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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

        public AlbumService(IRepository<Album> _albumRepo,
                            IRepository<Artist> _artistRepo,
                            IRepository<ArtistAlbum> _artistAlbumRepo,
                            IRepository<Song> _songRepo,
                            IImageService _imageService)
        {
            this.albumRepo = _albumRepo;
            this.artistRepo = _artistRepo;
            this.artistAlbumRepo = _artistAlbumRepo;
            this.songRepo = _songRepo;
            this.imageService = _imageService;
        }

        public async Task<int> GetTotalPendingPagesAsync(int pageSize = 4)
        {
            var totalPendingAlbums = await songRepo
                .GetAllAttached()
                .Where(s => s.Status != UploadStatus.Approved)
                .CountAsync();

            return (int)Math.Ceiling(totalPendingAlbums / (double)pageSize);
        }

        public async Task<IEnumerable<AlbumApprovalViewModel>> FilterAlbumsWithPendingStatusAsync(string title, int pageNumber = 1, int pageSize = 4)
        {
            var query = albumRepo.GetAllAttached()
                .Where(a => a.Status != UploadStatus.Approved);

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{title}%"));
            }

            return await query
                .Select(a => new AlbumApprovalViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Hours = a.Length.Hours,
                    Minutes = a.Length.Minutes,
                    Seconds = a.Length.Seconds,
                    ReleaseDate = a.ReleaseDate,
                    SongsCount = a.Songs.Count,
                    ImageURL = a.ImageURL,
                    Status = a.Status,
                    Artists = a.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> GetSpecificArtistAlbumsAsync(Guid userId)
        {
            return await albumRepo.GetAllAttached()
                .Where(s => s.ArtistsAlbums.Any(a => a.Artist.UserId == userId) && s.Status == UploadStatus.Approved)
                .Select(album => new AlbumIndexViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Hours = album.Length.Hours,
                    Minutes = album.Length.Minutes,
                    Seconds = album.Length.Seconds,
                    ReleaseDate = album.ReleaseDate,
                    SongsCount = album.Songs.Count,
                    ImageURL = album.ImageURL,
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                })
                .ToListAsync();
        }

        public async Task<AlbumEditViewModel?> GetAlbumByIdAsync(Guid id)
        {
            return await albumRepo.GetAllAttached()
                .Where(a => a.Id == id)
                .Select(a => new AlbumEditViewModel
                {
                    Id = id,
                    Title = a.Title,
                    ReleaseDate = a.ReleaseDate,
                    SelectedArtistIds = a.ArtistsAlbums.Select(sa => sa.ArtistId).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<AlbumSongsIndexViewModel?> GetAlbumByIdWithSongsAsync(Guid id)
        {
            return await albumRepo.GetAllAttached()
                .Where(a => a.Id == id)
                .Select(a => new AlbumSongsIndexViewModel
                {
                    Id = id,
                    UserIds = a.ArtistsAlbums.Select(aa => aa.Artist.UserId).ToList(),
                    Title = a.Title,
                    Hours = a.Length.Hours,
                    Minutes = a.Length.Minutes,
                    Seconds = a.Length.Seconds,
                    ReleaseDate = a.ReleaseDate,
                    SongsCount = a.Songs.Count,
                    ImageURL = a.ImageURL,
                    Artists = a.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList(),
                    Songs = a.Songs.Select(s => new SongAlbumIndexViewModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Minutes = s.Length.Minutes,
                        Seconds = s.Length.Seconds,
                        Genre = s.Genre.Name,
                        Artists = s.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList()
                    }).ToList(),
                }).FirstOrDefaultAsync();
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
                ArtistsAlbums = new List<ArtistAlbum>(),
                Songs = new List<Song>()
            };


            // add main artist
            album.ArtistsAlbums.Add(new ArtistAlbum
            {
                Artist = mainArtist,
                Album = album,
            });

            // add featured artists
            if (featuredArtists.Count > 0)
            {
                foreach (var artist in featuredArtists)
                {
                    album.ArtistsAlbums.Add(new ArtistAlbum
                    {
                        Artist = artist,
                        Album = album,
                    });
                }
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

            var selectedArtistsIds = model.SelectedArtistIds ?? new List<Guid>();

            var featuredArtists = await artistRepo.GetAllAttached()
                .Where(x => selectedArtistsIds.Contains(x.Id))
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

            if (albumSongs.Count > 0)
            {
                // add new songs to the album
                albumSongs.ForEach(s => album.Songs.Add(s));

                // increase length of album
                TimeSpan albumLength = TimeSpan.Zero;
                foreach (var song in albumSongs)
                {
                    albumLength += song.Length;
                }
                album.Length = album.Length + albumLength;
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(album.CloudinaryPublicId))
                {
                    // delete image from Cloudinary
                    await imageService.DestroyImageAsync(album.CloudinaryPublicId);
                }

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
            else if (album.ImageURL == "/images/defaults/default-album-cover-art.png")
            {
                foreach (var song in album.Songs)
                {
                    song.ImageURL = "/images/defaults/default-song-cover-art.png";
                    song.CloudinaryPublicId = "";
                }
            }
            else
            {
                foreach (var song in album.Songs)
                {
                    song.ImageURL = album.ImageURL;
                    song.CloudinaryPublicId = album.CloudinaryPublicId;
                }
            }

            album.Title = model.Title;
            album.ReleaseDate = model.ReleaseDate;

            // make status of album pending
            album.Status = UploadStatus.Pending;

            // remove ArtistAlbums for this album from the DB 
            album.ArtistsAlbums.Clear();

            // add main artist to the album
            album.ArtistsAlbums.Add(new ArtistAlbum
            {
                ArtistId = mainArtist.Id,
                AlbumId = album.Id,
            });

            // add featured artists to the album if any are selected
            if (featuredArtists.Count > 0)
            { 
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

            // update entity
            await albumRepo.UpdateAsync(album);
        }

        public async Task RemoveAlbumAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.Id == id);

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
            if (album.Songs.Count > 0)
            {
                foreach (var song in album.Songs)
                {
                    song.ImageURL = "/images/defaults/default-song-cover-art.png";
                    song.CloudinaryPublicId = "";
                    // album's songs soft delete
                    song.Status = UploadStatus.Deleted;
                }
            }

            await albumRepo.UpdateAsync(album);
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 4)
        {
            var totalAlbums = await albumRepo
                .GetAllAttached()
                .Where(s => s.Status == UploadStatus.Approved)
                .CountAsync();

            return (int)Math.Ceiling(totalAlbums / (double)pageSize);
        }

        public async Task<IEnumerable<AlbumIndexViewModel>> FilterAlbumsAsync(string searchItem, List<string> filters, int pageNumber = 1, int pageSize = 4)
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
                    SongsCount = album.Songs.Count,
                    ImageURL = album.ImageURL,
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
                    SongsCount = album.Songs.Count,
                    ImageURL = album.ImageURL,
                    Artists = album.ArtistsAlbums.Select(x => x.Artist.User.UserName).ToList()
                }).ToListAsync();
        }

        public async Task RemoveSongFromAlbumAsync(Guid songId, Guid albumId)
        {
            var song = await songRepo.GetAllAttached()
                .Include(s => s.Album)
                .FirstOrDefaultAsync(s => s.Id == songId);

            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {songId} is not found!");
            }

            song.AlbumId = null;
            song.ImageURL = "/images/defaults/default-song-cover-art.png";
            song.CloudinaryPublicId = "";
            song.Album.Length = song.Album.Length - song.Length;

            await songRepo.UpdateAsync(song);
        }

        public async Task AddSongToAlbumAsync(Guid songId, Guid albumId)
        {
            var song = await songRepo.GetByIdAsync(songId);

            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {songId} is not found!");
            }

            var album = await albumRepo.GetAllAttached()
            .Include(a => a.Songs)
            .FirstOrDefaultAsync(a => a.Id == albumId);

            if (album == null)
            {
                throw new KeyNotFoundException($"Album with ID {albumId} is not found!");
            }

            // delete song from cloudinary for the single if one exists in the cloud
            if (!string.IsNullOrEmpty(song.CloudinaryPublicId) && !string.IsNullOrEmpty(song.ImageURL))
            {
                await imageService.DestroyImageAsync(song.CloudinaryPublicId);
            }

            // update song
            song.AlbumId = album.Id;
            song.ImageURL = album.ImageURL;
            song.CloudinaryPublicId = album.CloudinaryPublicId;

            // update album stats
            album.Length += song.Length;

            await songRepo.UpdateAsync(song);
            await albumRepo.UpdateAsync(album);
        }

        public async Task AcceptAlbumUploadAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
            {
                throw new KeyNotFoundException($"Album with ID {id} is not found!");
            }

            album.Status = UploadStatus.Approved;

            if (album.Songs.Count > 0)
            {
                foreach (var song in album.Songs)
                {
                    song.Status = UploadStatus.Approved;
                }
            }

            await albumRepo.UpdateAsync(album);
        }

        public async Task RejectAlbumUploadAsync(Guid id)
        {
            var album = await albumRepo.GetAllAttached()
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.Id == id);

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

        public async Task<int> GetTotalAlbumsCountAsync()
        {
            return await albumRepo.GetAllAttached()
                .CountAsync(a => a.Status != UploadStatus.Deleted);
        }

        public async Task<int> GetTotalPendingAlbumsCountAsync()
        {
            return await albumRepo.GetAllAttached()
                .CountAsync(a => a.Status != UploadStatus.Approved);
        }

        public async Task<int> GetTotalAlbumsCountByCurrentLoggedArtistAsync(Guid userId)
        {
            return await albumRepo.GetAllAttached()
                .Where(s => s.ArtistsAlbums.Any(a => a.Artist.UserId == userId) && s.Status != UploadStatus.Deleted)
                .CountAsync();
        }

        public async Task<int> GetTotalPendingAlbumsCountByCurrentLoggedArtistAsync(Guid userId)
        {
            return await albumRepo.GetAllAttached()
                .Where(s => s.ArtistsAlbums.Any(a => a.Artist.UserId == userId) && s.Status != UploadStatus.Approved)
                .CountAsync();
        }
    }
}