using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
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

        public PlaylistService(IRepository<Playlist> _playlistRepo, IRepository<Song> _songRepo, IRepository<PlaylistSong> _playlistSongRepo)
        {
            this.playlistRepo = _playlistRepo;
            this.songRepo = _songRepo;
            this.playlistSongRepo = _playlistSongRepo;
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
            var playlist = await playlistRepo.GetAllAttached()
                .Include(x => x.PlaylistsSongs)
                    .ThenInclude(s => s.Song)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (playlist == null)
            {
                return null;
            }

            return new PlaylistIndexViewModel
            {
                Id = playlist.Id,
                Title = playlist.Title,
                Hours = playlist.Length.Hours,
                Minutes = playlist.Length.Minutes,
                Seconds = playlist.Length.Seconds,
                SongCount = playlist.SongCount,
                DateCreated = playlist.DateCreated,
                ImageURL = playlist.ImageURL,
                IsPublic = playlist.IsPublic,
            };
        }

        public async Task<PlaylistSongsIndexViewModel?> GetPlaylistByIdWithSongsAsync(Guid id)
        {
            var playlist = await playlistRepo.GetAllAttached()
                .Include(x => x.PlaylistsSongs)
                    .ThenInclude(s => s.Song)
                        .ThenInclude(g => g.Genre)
                .Include(x => x.PlaylistsSongs)
                    .ThenInclude(ps => ps.Song)
                        .ThenInclude(s => s.ArtistsSongs)
                            .ThenInclude(sa => sa.Artist)
                                .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (playlist == null)
            {
                return null;
            }

            return new PlaylistSongsIndexViewModel
            {
                Id = playlist.Id,
                Title = playlist.Title,
                Hours = playlist.Length.Hours,
                Minutes = playlist.Length.Minutes,
                Seconds = playlist.Length.Seconds,
                SongsCount = playlist.SongCount,
                DateCreated = playlist.DateCreated,
                ImageURL = playlist.ImageURL,
                Songs = playlist.PlaylistsSongs.Select(x => new SongAlbumIndexViewModel
                {
                    Title = x.Song.Title,
                    Minutes= x.Song.Length.Minutes,
                    Seconds = x.Song.Length.Seconds,
                    Genre = x.Song.Genre.Name,
                    Artists = x.Song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList(),
                }).ToList(),
            };
        }

        public async Task AddPlaylistAsync(PlaylistCreateViewModel model, Guid userId)
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
                UserId = userId
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

        public async Task UpdatePlaylistAsync(PlaylistEditViewModel model, Guid userId)
        {
            var playlistSongs = await songRepo.GetAllAttached()
                .Where(x => model.SelectedSongIds.Contains(x.Id))
                .ToListAsync();

            var playlist = await playlistRepo.GetByIdAsync(model.Id);

            playlist.Title = model.Title;
            playlist.DateCreated = model.DateCreated;
            playlist.ImageURL = model.ImageURL;
            playlist.IsPublic = model.IsPublic;
            playlist.UserId = userId;

            // add songs to the playlist if any are selected
            if (playlistSongs.Count > 0)
            {
                // remove old playlistSong entities
                var playlistSongsToRemove = await playlistSongRepo.GetAllAttached()
                    .Where(x => x.PlaylistId == playlist.Id)
                    .ToListAsync();
                await playlistSongRepo.RemoveRangeAsync(playlistSongsToRemove);

                // add new songs to playlist
                foreach (var song in playlistSongs)
                {
                    playlist.PlaylistsSongs.Add(new PlaylistSong
                    {
                        Playlist = playlist,
                        Song = song,
                    });
                }

                // update playlist length
                TimeSpan playlistLength = TimeSpan.Zero;
                foreach (var song in playlistSongs)
                {
                    playlistLength += song.Length;
                }
                playlist.Length = playlistLength;

                // update playlist sogn count
                playlist.SongCount = playlistSongs.Count();

                // update entity
                await playlistRepo.UpdateAsync(playlist);
            }
        }

        public async Task RemovePlaylistAsync(Guid id)
        {
            var playlist = await playlistRepo.GetByIdAsync(id);

            await playlistRepo.RemoveAsync(playlist);
        }
    }
}
