using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class SongService : ISongService
    {
        private readonly IRepository<Song> songRepo;
        private readonly IRepository<ArtistSong> artistSongRepo;
        private readonly IRepository<Artist> artistRepo;
        private readonly IRepository<Album> albumRepo;
        private readonly IImageService imageService;
        private readonly IRepository<PlaylistSong> playlistSongRepo;
        private readonly IRepository<LikedSongs> likedSongsRepo;
        private readonly IRepository<Playlist> playlistRepo;

        public SongService(IRepository<Song> _songRepo,
                           IRepository<ArtistSong> _artistSongRepo,
                           IRepository<Artist> _artistRepo,
                           IRepository<Album> _albumRepo,
                           IImageService _imageService,
                           IRepository<PlaylistSong> _playlistSongRepo,
                           IRepository<LikedSongs> _likedSongsRepo,
                           IRepository<Playlist> _playlistRepo)
        {
            this.songRepo = _songRepo;
            this.artistSongRepo = _artistSongRepo;
            this.artistRepo = _artistRepo;
            this.albumRepo = _albumRepo;
            this.imageService = _imageService;
            this.playlistSongRepo = _playlistSongRepo;
            this.likedSongsRepo = _likedSongsRepo;
            this.playlistRepo = _playlistRepo;
        }

        public async Task<IEnumerable<SongApprovalViewModel>> GetSongsWithPendingStatusAsync()
        {
            return await songRepo.GetAllAttached()
                .Where(s => s.Status != UploadStatus.Approved)
                .Select(song => new SongApprovalViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    Minutes = song.Length.Minutes,
                    Seconds = song.Length.Seconds,
                    ReleaseDate = song.ReleaseDate,
                    AlbumName = song.Album != null ? song.Album.Title : "Single",
                    GenreName = song.Genre.Name,
                    Genre = song.Genre.Name,
                    ImageURL = song.ImageURL,
                    Likes = song.Likes,
                    Artists = song.ArtistsSongs.Select(a => a.Artist.User.UserName).ToList(),
                    Status = song.Status,
                })
                .ToListAsync();
        }

        public async Task<SongIndexViewModel?> GetSongByIdAsync(Guid id)
        {
            return await songRepo.GetAllAttached()
                .Where(s => s.Id == id)
                .Select(s => new SongIndexViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    ImageURL = s.ImageURL,
                }).FirstOrDefaultAsync();
        }

        public async Task AddSongAsync(SongCreateViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached().
                FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached().
                Where(x => model.SelectedArtistIds.Contains(x.Id))
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
                imageURL = "/images/defaults/default-song-cover-art.png";
                publicId = ""; // No publicId because we didn’t upload
            }

            var song = new Song
            {
                Title = model.Title,
                Length = new TimeSpan(0, model.Minutes, model.Seconds),
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                GenreId = model.GenreId,
                ImageURL = imageURL,
                CloudinaryPublicId = publicId,
                Status = UploadStatus.Pending,
                ArtistsSongs = new List<ArtistSong>()
            };

            // add main artist
            song.ArtistsSongs.Add(new ArtistSong
            {
                Artist = mainArtist,
                Song = song,
            });

            // add featured artists if any are seletced
            if (featuredArtists.Count > 0)
            {
                foreach (var artist in featuredArtists)
                {
                    song.ArtistsSongs.Add(new ArtistSong
                    {
                        Artist = artist,
                        Song = song,
                    });
                }
            }

            await songRepo.AddAsync(song);
        }

        public async Task UpdateSongsAsync(SongEditViewModel model, Guid userId)
        {
            var mainArtist = await artistRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var featuredArtists = await artistRepo.GetAllAttached()
                .Where(x => model.SelectedArtistIds.Contains(x.Id))
                .ToListAsync();

            var song = await songRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {model.Id} is not found!");
            }

            // if song is part of album do not remove the image
            if (model.ImageFile != null && model.ImageFile.Length > 0 && song.AlbumId == null)
            {
                // delete image from Cloudinary
                if (!string.IsNullOrEmpty(song.CloudinaryPublicId))
                {
                    await imageService.DestroyImageAsync(song.CloudinaryPublicId);
                }

                // Artist uploaded a cover → use Cloudinary
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "art-covers");
                song.ImageURL = uploadResult.Url;
                song.CloudinaryPublicId = uploadResult.PublicId;
            }

            song.Title = model.Title;
            song.Length = new TimeSpan(0, model.Minutes, model.Seconds);
            song.ReleaseDate = model.ReleaseDate;
            song.GenreId = model.GenreId;

            // make status of song pending
            song.Status = UploadStatus.Pending;

            // remove ArtistSong for this song from the DB 
            var artistsSong = await artistSongRepo.GetAllAttached()
                .Where(x => x.SongId == song.Id)
                .ToListAsync();
            await artistSongRepo.RemoveRangeAsync(artistsSong);

            // add main artist to the song
            song.ArtistsSongs.Add(new ArtistSong
            {
                ArtistId = mainArtist.Id,
                SongId = song.Id,
            });

            // add featured artists if any are seletced
            if (featuredArtists.Count > 0)
            {
                foreach (var artist in featuredArtists)
                {
                    song.ArtistsSongs.Add(new ArtistSong
                    {
                        ArtistId = artist.Id,
                        SongId = song.Id,
                    });
                }
            }

            // add featured artists to the song if any are selected
            //if (featuredArtists.Count == 0)
            //{
            //    // add main artist to the song
            //    song.ArtistsSongs.Add(new ArtistSong
            //    {
            //        ArtistId = mainArtist.Id,
            //        SongId = song.Id,
            //    });
            //}
            //else
            //{
            //    // add main artist to the song
            //    song.ArtistsSongs.Add(new ArtistSong
            //    {
            //        ArtistId = mainArtist.Id,
            //        SongId = song.Id,
            //    });

            //    // add featured artists to the song
            //    foreach (var artist in featuredArtists)
            //    {
            //        song.ArtistsSongs.Add(new ArtistSong
            //        {
            //            ArtistId = artist.Id,
            //            SongId = song.Id,
            //        });
            //    }
            //}

            // update entity
            await songRepo.UpdateAsync(song);
        }

        public async Task RemoveSongAsync(Guid id)
        {
            var song = await songRepo.GetAllAttached()
            .Include(x => x.Album)
            .Include(x => x.PlaylistSongs)
                .ThenInclude(ps => ps.Playlist)
            .Include(x => x.LikedSongs)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {id} is not found!");
            }

            // if song is part of album do not remove the image
            if (!string.IsNullOrEmpty(song.ImageURL) && !string.IsNullOrEmpty(song.CloudinaryPublicId) && song.AlbumId == null)
            {
                // delete image from Cloudinary
                await imageService.DestroyImageAsync(song.CloudinaryPublicId);
                song.ImageURL = "/images/defaults/default-song-cover-art.png";
                song.CloudinaryPublicId = "";
            }

            if (song.AlbumId != null)
            {
                song.Album.SongsCount--;
                song.Album.Length = song.Album.Length - song.Length;
                song.AlbumId = null;
            }

            // remove from playlists
            if (song.PlaylistSongs.Any())
            {
                foreach (var playlistSong in song.PlaylistSongs)
                {
                    var playlist = playlistSong.Playlist;

                    playlist.SongCount--;
                    playlist.Length = playlist.Length - song.Length;
                }

                await playlistSongRepo.RemoveRangeAsync(song.PlaylistSongs);
            }

            // remove from liked songs
            if (song.LikedSongs.Any())
            {
                await likedSongsRepo.RemoveRangeAsync(song.LikedSongs);
                song.Likes = 0;
            }

            // soft delete
            song.Status = UploadStatus.Deleted;

            await songRepo.UpdateAsync(song);
        }

        public async Task<IEnumerable<SongIndexViewModel>> FilterSongsAsync(string searchItem, List<string> filters)
        {
            var query = songRepo.GetAllAttached()
                .Where(x => x.Status == UploadStatus.Approved);

            if (!string.IsNullOrEmpty(searchItem) && filters != null && filters.Any())
            {
                query = query.Where(s =>
                    (filters.Contains("Title") && EF.Functions.Like(s.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Album") && s.Album != null && EF.Functions.Like(s.Album.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Genre") && EF.Functions.Like(s.Genre.Name, $"%{searchItem}%")) ||
                    (filters.Contains("Artist") && s.ArtistsSongs.Any(a => EF.Functions.Like(a.Artist.User.UserName, $"%{searchItem}%")))
                );
            }

            return await query
                .Select(song => new SongIndexViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    ImageURL = song.ImageURL,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongIndexViewModel>> FilterSongsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters)
        {
            var query = songRepo.GetAllAttached()
                .Where(s => s.ArtistsSongs.Any(x => x.Artist.UserId == userId) && s.Status == UploadStatus.Approved);

            if (!string.IsNullOrEmpty(searchItem) && filters != null && filters.Any())
            {
                query = query.Where(s =>
                    (filters.Contains("Title") && EF.Functions.Like(s.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Album") && s.Album != null && EF.Functions.Like(s.Album.Title, $"%{searchItem}%")) ||
                    (filters.Contains("Genre") && EF.Functions.Like(s.Genre.Name, $"%{searchItem}%"))
                );
            }

            return await query
                .Select(song => new SongIndexViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    ImageURL = song.ImageURL,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongSelectViewModel>> SelectSongsAsync()
        {
            return await songRepo.GetAllAttached()
                .Select(song => new SongSelectViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongSelectViewModel>> SelectSinglesByCurrentLoggedArtistAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                 .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId) && s.Album == null)
                .Select(song => new SongSelectViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SongIndexViewModel>> GetSinglesForCurrentLoggedArtistAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                .Where(song => song.ArtistsSongs.Any(a => a.Artist.UserId == userId) && song.AlbumId == null && song.Status == UploadStatus.Approved)
                .Select(song => new SongIndexViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    ImageURL = song.ImageURL,
                }).ToListAsync();
        }

        public async Task AcceptSongUploadAsync(Guid id)
        {
            var song = await songRepo.GetByIdAsync(id);

            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {id} is not found!");
            }

            song.Status = UploadStatus.Approved;

            await songRepo.UpdateAsync(song);
        }

        public async Task RejectSongUploadAsync(Guid id)
        {
            var song = await songRepo.GetByIdAsync(id);

            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {id} is not found!");
            }

            song.Status = UploadStatus.Rejected;

            await songRepo.UpdateAsync(song);
        }

        public async Task<int> GetTotalSongsCountAsync()
        {
            return await songRepo.GetAllAttached()
                .CountAsync(s => s.Status != UploadStatus.Deleted);
        }

        public async Task<int> GetTotalPendingSongsCountAsync()
        {
            return await songRepo.GetAllAttached()
                .CountAsync(s => s.Status != UploadStatus.Approved);
        }

        public async Task<int> GetTotalSongsCountByCurrentLoggedArtistAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId) && s.Status != UploadStatus.Deleted)
                .CountAsync();
        }

        public async Task<int> GetTotalPendingSongsCountByCurrentLoggedArtistAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId) && s.Status != UploadStatus.Approved)
                .CountAsync();
        }

        public async Task<int> GetTotalSongsLikesByCurrentLoggedArtistAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId))
                .SumAsync(s => s.Likes);
        }

        public async Task<MostLikedSongViewModel?> GetMostLikedSongAsync(Guid userId)
        {
            return await songRepo.GetAllAttached()
                .Where(s => s.ArtistsSongs.Any(a => a.Artist.UserId == userId))
                .OrderByDescending(s => s.Likes)
                .Select(s => new MostLikedSongViewModel
                {
                    MostLikedSongTitle = s.Title,
                    MostLikedSongCount = s.Likes
                }).FirstOrDefaultAsync();
        }
    }
}
