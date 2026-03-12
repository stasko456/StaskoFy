using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2019.Drawing.Model3D;
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
        private readonly IImageService imageService;

        public PlaylistService(IRepository<Playlist> _playlistRepo,
                               IRepository<Song> _songRepo,
                               IRepository<PlaylistSong> _playlistSongRepo,
                               IImageService _imageService)
        {
            this.playlistRepo = _playlistRepo;
            this.songRepo = _songRepo;
            this.playlistSongRepo = _playlistSongRepo;
            this.imageService = _imageService;
        }

        public async Task <IEnumerable<PlaylistIndexViewModel>> GetPlaylistsFromCurrentLoggedUserAsync(Guid userId)
        {
            return await playlistRepo.GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(p => new PlaylistIndexViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Hours = p.Length.Hours,
                    Minutes = p.Length.Minutes,
                    Seconds = p.Length.Seconds,
                    SongCount = p.SongCount,
                    DateCreated = p.DateCreated,
                    ImageURL = p.ImageURL,
                    IsPublic = p.IsPublic,
                }).ToListAsync();
        }

        public async Task<PlaylistIndexViewModel?> GetPlaylistByIdAsync(Guid id)
        {
            return await playlistRepo.GetAllAttached()
            .Where(p => p.Id == id)
            .Select(p => new PlaylistIndexViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Hours = p.Length.Hours,
                Minutes = p.Length.Minutes,
                Seconds = p.Length.Seconds,
                SongCount = p.SongCount,
                DateCreated = p.DateCreated,
                ImageURL = p.ImageURL,
                IsPublic = p.IsPublic,
            }).FirstOrDefaultAsync();
        }

        public async Task<PlaylistSongsIndexViewModel?> GetPlaylistByIdWithSongsAsync(Guid id)
        {
            return await playlistRepo.GetAllAttached()
                .Where(p => p.Id == id)
                .Select(p => new PlaylistSongsIndexViewModel
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Title = p.Title,
                    Hours = p.Length.Hours,
                    Minutes = p.Length.Minutes,
                    Seconds = p.Length.Seconds,
                    SongCount = p.SongCount,
                    DateCreated = p.DateCreated,
                    ImageURL = p.ImageURL,
                    Songs = p.PlaylistsSongs.Select(s => new SongPlaylistIndexViewModel
                    {
                        Id = s.SongId,
                        Title = s.Song.Title,
                        AlbumTitle = s.Song.Album == null ? "Single" : s.Song.Album.Title,
                        Minutes = s.Song.Length.Minutes,
                        Seconds = s.Song.Length.Seconds,
                        ImageUrl = s.Song.ImageURL,
                        DateAdded = s.DateAdded,
                        Artists = s.Song.ArtistsSongs.Select(a => a.Artist.User.UserName).ToList(),
                    }).ToList(),
                }).FirstOrDefaultAsync();
        }

        public async Task AddPlaylistAsync(PlaylistCreateViewModel model, Guid userId)
        {
            var playlistSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

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
                // No upload → use default cover
                imageURL = "/images/defaults/default-album-cover-art.png";
                publicId = ""; // No publicId because we didn’t upload
            }

            playlist.ImageURL = imageURL;
            playlist.CloudinaryPublicId = publicId;

            // add songs to playlist
            foreach (var song in playlistSongs)
            {
                playlist.PlaylistsSongs.Add(new PlaylistSong
                {
                    Playlist = playlist,
                    Song = song,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now)
                });
            }

            // add songCount
            playlist.SongCount = playlistSongs.Count();

            // add length
            TimeSpan playlistLength = TimeSpan.Zero;
            foreach (var song in playlistSongs)
            {
                playlistLength += song.Length;
            }
            playlist.Length = playlistLength;

            // add playlist to DB
            await playlistRepo.AddAsync(playlist);
        }

        public async Task UpdatePlaylistAsync(PlaylistEditViewModel model, Guid userId)
        {
            var playlistSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            string imageURL = "";
            string publicId = "";

            var playlist = await playlistRepo.GetByIdAsync(model.Id);

            if (playlist == null)
            {
                throw new KeyNotFoundException($"Playlist with ID {model.Id} is not found!");
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // delete image from Cloudinary
                await imageService.DestroyImageAsync(playlist.CloudinaryPublicId);

                // add image to Cloudinary
                var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "playlist-covers");
                playlist.ImageURL = uploadResult.Url;
                playlist.CloudinaryPublicId = uploadResult.PublicId;
            }

            playlist.Title = model.Title;
            playlist.DateCreated = model.DateCreated;
            playlist.IsPublic = model.IsPublic;
            playlist.UserId = userId;

            //add songs to the playlist if any are selected
            if (playlistSongs.Count > 0)
            {
                // add new songs to playlist
                foreach (var song in playlistSongs)
                {
                    playlist.PlaylistsSongs.Add(new PlaylistSong
                    {
                        Playlist = playlist,
                        Song = song,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now)
                    });
                    playlist.SongCount++;
                }

                // update playlist length
                TimeSpan playlistLength = TimeSpan.Zero;
                foreach (var song in playlistSongs)
                {
                    playlistLength += song.Length;
                }
                playlist.Length = playlist.Length + playlistLength;
            }

            // update entity
            await playlistRepo.UpdateAsync(playlist);
        }

        public async Task RemovePlaylistAsync(Guid id)
        {
            var playlist = await playlistRepo.GetByIdAsync(id);

            if (playlist == null)
            {
                throw new KeyNotFoundException($"Playlist with ID {id} is not found!");
            }

            if (!string.IsNullOrEmpty(playlist.ImageURL) && !string.IsNullOrEmpty(playlist.CloudinaryPublicId))
            {
                // delete image from Cloudinary
                await imageService.DestroyImageAsync(playlist.CloudinaryPublicId);
            }

            await playlistRepo.RemoveAsync(playlist);
        }

        public async Task AddSongToPlaylistAsync(Guid playlistId, Guid songId)
        {
            var playlistSongExists = await playlistSongRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.SongId == songId);

            if (playlistSongExists != null)
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
            playlist.SongCount++;

            var song = await songRepo.GetByIdAsync(songId);
            playlist.Length = playlist.Length + song.Length;

            await playlistSongRepo.AddAsync(playlistSong);
        }

        public async Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId)
        {
            var playlistSong = await playlistSongRepo.GetAllAttached()
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId && p.SongId == songId);

            if (playlistSong == null)
            {
                return;
            }

            var playlist = await playlistRepo.GetByIdAsync(playlistId);
            playlist.SongCount--;

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
    }
}
