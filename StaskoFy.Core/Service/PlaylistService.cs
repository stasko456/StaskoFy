using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist> playlistRepo;
        private readonly IRepository<Song> songRepo;

        public PlaylistService(IRepository<Playlist> _playlistRepo, IRepository<Song> _songRepo)
        {
            this.playlistRepo = _playlistRepo;
            this.songRepo = _songRepo;
        }

        public async Task <IEnumerable<PlaylistIndexViewModel>> GetAllFromCurrentLoggedUser(Guid userId)
        {
            return await playlistRepo.GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(p => new PlaylistIndexViewModel
                {
                    Title = p.Title,
                    Hours = p.Length.Hours,
                    Minutes = p.Length.Minutes,
                    Seconds = p.Length.Seconds,
                    SongCount = p.SongCount,
                    ImageURL = p.ImageURL,
                    IsPublic = p.IsPublic,
                }).ToListAsync();
        }

        public async Task<PlaylistIndexViewModel?> GetByIdAsync(Guid id)
        {
            var playlist = await playlistRepo.GetAllAttached()
                .Include(x => x.PlaylistsSongs)
                    .ThenInclude(s => s.Song)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (playlist == null)
            {
                throw new KeyNotFoundException("Playlist not found.");
            }

            return new PlaylistIndexViewModel
            {
                Title = playlist.Title,
                Hours = playlist.Length.Hours,
                Minutes = playlist.Length.Minutes,
                Seconds = playlist.Length.Seconds,
                SongCount = playlist.SongCount,
                ImageURL = playlist.ImageURL,
                IsPublic = playlist.IsPublic,
            };
        }

        public async Task AddAsync(PlaylistCreateViewModel model, Guid userId)
        {
            var playlistSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            var playlist = new Playlist
            {
                Title = model.Title,
                DateCreated = DateOnly.FromDateTime(DateTime.Now),
                ImageURL = model.ImageURL,
                IsPublic = model.IsPublic,
                PlaylistsSongs = new List<PlaylistSong>(),
            };

            // add songs to playlist
            foreach (var song in playlistSongs)
            {
                playlist.PlaylistsSongs.Add(new PlaylistSong
                {
                    Playlist = playlist,
                    Song = song,
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

        public async Task UpdateAsync(PlaylistEditViewModel model)
        {
            var playlistSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            var playlist = await playlistRepo.GetByIdAsync(model.Id);

            playlist.Title = model.Title;
            playlist.Length = new TimeSpan(model.Hours, model.Minutes, model.Seconds);
            playlist.DateCreated = model.DateCreated;
            playlist.ImageURL = model.ImageURL;
            playlist.IsPublic = model.IsPublic;

            // add songs to playlist
        }

        public async Task RemoveAsync(Guid id)
        {
            var playlist = await playlistRepo.GetByIdAsync(id);

            await playlistRepo.RemoveAsync(playlist);
        }
    }
}
