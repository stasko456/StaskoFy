using CloudinaryDotNet.Actions;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2019.Drawing.Model3D;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Playlist;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist> playlistRepo;
        private readonly IRepository<Song> songRepo;
        private readonly IRepository<PlaylistSong> playlistSongRepo;
        private readonly IUploadService imageService;

        public PlaylistService(IRepository<Playlist> _playlistRepo,
                               IRepository<Song> _songRepo,
                               IRepository<PlaylistSong> _playlistSongRepo,
                               IUploadService _imageService)
        {
            this.playlistRepo = _playlistRepo;
            this.songRepo = _songRepo;
            this.playlistSongRepo = _playlistSongRepo;
            this.imageService = _imageService;
        }

        public async Task <IEnumerable<PlaylistIndexViewModel>> GetPlaylistsFromCurrentLoggedUserAsync(Guid userId)
        {
            return await playlistRepo.GetAllAttached()
                .Where(x => x.UserId == userId )
                .Select(p => new PlaylistIndexViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    ImageURL = p.ImageURL,
                    IsPublic = p.IsPublic,
                    UserId = p.UserId
                }).ToListAsync();
        }

        public async Task<PlaylistIndexViewModel?> GetPlaylistByIdAsync(Guid id)
        {
            var playlist = await playlistRepo.GetAllAttached()
            .Where(p => p.Id == id)
            .Select(p => new PlaylistIndexViewModel
            {
                Id = p.Id,
                Title = p.Title,
                DateCreated = p.DateCreated,
                ImageURL = p.ImageURL,
                IsPublic = p.IsPublic,
                UserId = p.UserId
            }).FirstOrDefaultAsync();

            if (playlist is null)
            {
                throw new NullReferenceException("Unable to find this playlist!");
            }

            return playlist;
        }

        public async Task<int> GetTotalPlaylistSongsPagesAsync(Guid id, int pageSize = 5)
        {
            var totalPlaylistSongs = await playlistSongRepo
                    .GetAllAttached()
                    .Where(ps => ps.PlaylistId == id)
                    .CountAsync();

            return (int)Math.Ceiling(totalPlaylistSongs / (double)pageSize);
        }

        public async Task<IEnumerable<SongPlaylistIndexViewModel>> GetPlaylistSongsByIdAsync(Guid id, string name, int pageNumber = 1, int pageSize = 5)
        {
            var query = playlistSongRepo.GetAllAttached()
                .Where(ps => ps.PlaylistId == id && ps.Song.Status == UploadStatus.Approved);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(ps => EF.Functions.Like(ps.Song.Title, $"%{name}%"));
            }

            return await query
                .Select(ps => new SongPlaylistIndexViewModel
                {
                    Id = ps.SongId,
                    Title = ps.Song.Title,
                    AlbumTitle = ps.Song.Album == null ? "Single" : ps.Song.Album.Title,
                    Minutes = ps.Song.Length.Minutes,
                    Seconds = ps.Song.Length.Seconds,
                    ImageUrl = ps.Song.ImageURL,
                    DateAdded = ps.DateAdded,
                    Artists = ps.Song.ArtistsSongs.Select(a => a.Artist.User.UserName).ToList(),
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TimeSpan> GetLengthOfPlaylistSongsByIdAsync(Guid playlistId)
        {
            var songs = playlistSongRepo.GetAllAttached()
            .Include(ps => ps.Song)
            .Where(ps => ps.PlaylistId == playlistId && ps.Song.Status == UploadStatus.Approved);

            var totalDuration = new TimeSpan(0, 0, 0);

            foreach (var song in songs)
            {
                totalDuration = totalDuration + song.Song.Length;
            }

            return totalDuration;
        }

        public async Task<int> GetCountOfPlaylistSongsByIdAsync(Guid playlistId)
        {
            return await playlistSongRepo.GetAllAttached()
                .Where(ps => ps.PlaylistId == playlistId && ps.Song.Status == UploadStatus.Approved)
                .CountAsync();
        }

        public async Task AddPlaylistAsync(PlaylistCreateViewModel model, Guid userId)
        {
            string imageURL = "";
            string publicId = "";

            var playlist = new Playlist
            {
                Title = model.Title,
                DateCreated = DateOnly.FromDateTime(DateTime.Now),
                IsPublic = model.IsPublic,
                PlaylistsSongs = new List<PlaylistSong>(),
                UserId = userId
            };

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "playlist-covers");
                imageURL = uploadResult.Url;
                publicId = uploadResult.PublicId;
            }
            else
            {
                imageURL = "/images/defaults/default-album-cover-art.png";
                publicId = "";
            }

            playlist.ImageURL = imageURL;
            playlist.CloudinaryPublicId = publicId;

            await playlistRepo.AddAsync(playlist);
        }

        public async Task UpdatePlaylistAsync(PlaylistEditViewModel model, Guid userId)
        {
            string imageURL = "";
            string publicId = "";

            var playlist = await playlistRepo.GetByIdAsync(model.Id);

            if (playlist is null)
            {
                throw new NullReferenceException("Unable to find this playlist!");
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                await imageService.DestroyImageAsync(playlist.CloudinaryPublicId);

                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "playlist-covers");
                playlist.ImageURL = uploadResult.Url;
                playlist.CloudinaryPublicId = uploadResult.PublicId;
            }

            playlist.Title = model.Title;
            playlist.IsPublic = model.IsPublic;
            playlist.UserId = userId;

            await playlistRepo.UpdateAsync(playlist);
        }

        public async Task RemovePlaylistAsync(Guid id)
        {
            var playlist = await playlistRepo.GetByIdAsync(id);

            if (playlist is null)
            {
                throw new NullReferenceException("Unable to find this playlist!");
            }

            if (!string.IsNullOrEmpty(playlist.ImageURL) && !string.IsNullOrEmpty(playlist.CloudinaryPublicId))
            {
                await imageService.DestroyImageAsync(playlist.CloudinaryPublicId);
            }

            await playlistRepo.RemoveAsync(playlist);
        }

        public async Task AddSongToPlaylistAsync(Guid playlistId, Guid songId)
        {
            var playlistSongExists = await playlistSongRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.SongId == songId);

            if (playlistSongExists is not null)
            {
                return;
            }

            var playlistSong = new PlaylistSong
            {
                PlaylistId = playlistId,
                SongId = songId,
                DateAdded = DateOnly.FromDateTime(DateTime.Now)
            };

            var playlist = await playlistRepo.GetByIdAsync(playlistId);

            var song = await songRepo.GetByIdAsync(songId);

            if (song is null)
            {
                throw new NullReferenceException("Unable to find this song!");
            }

            playlist.Length = playlist.Length + song.Length;

            await playlistSongRepo.AddAsync(playlistSong);
        }

        public async Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId)
        {
            var playlistSong = await playlistSongRepo.GetAllAttached()
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId && p.SongId == songId);

            if (playlistSong is null)
            {
                return;
            }

            var playlist = await playlistRepo.GetByIdAsync(playlistId);

            var song = await songRepo.GetByIdAsync(songId);
            playlist.Length = playlist.Length - song.Length;

            await playlistSongRepo.RemoveAsync(playlistSong);
        }

        public async Task<IEnumerable<PlaylistSelectViewModel>> SelectPlaylistsFromCurrentLoggedUserAsync(Guid userId)
        {
            return await playlistRepo.GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(p => new PlaylistSelectViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                }).ToListAsync();
        }

        public Task<int> GetTotalPlaylistsCountByCurrentLoggedUserAsync(Guid userId)
        {
            return playlistRepo.GetAllAttached()
                .Where(p => p.UserId == userId)
                .CountAsync();
        }

        public async Task<IEnumerable<SongDetailsForMusicPlayer>> GetSongsFromPlaylistByIdForMusicPlayerAsync(Guid playlistId)
        {
            return await playlistSongRepo.GetAllAttached()
                .Where(ps => ps.PlaylistId == playlistId && ps.Song.Status == UploadStatus.Approved)
                .Select(s => new SongDetailsForMusicPlayer
                {
                    Id = s.Id,
                    Title = s.Song.Title,
                    ImageURL = s.Song.ImageURL,
                    Duration = s.Song.Length,
                    Artists = s.Song.ArtistsSongs.Select(a => a.Artist.User.UserName).ToList(),
                    AudioURL = s.Song.AudioURL
                }).ToListAsync();
        }
    }
}
