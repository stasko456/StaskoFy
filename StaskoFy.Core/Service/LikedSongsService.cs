using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.LikedSongs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class LikedSongsService : ILikedSongsService
    {
        private readonly IRepository<LikedSongs> likedSongsRepo;
        private readonly IRepository<Song> songRepo;

        public LikedSongsService(IRepository<LikedSongs> _likedSongsRepo, 
                                 IRepository<Song> _songRepo)
        {
            this.likedSongsRepo = _likedSongsRepo;
            this.songRepo = _songRepo;
        }

        public async Task<LikedSongsPageViewModel?> GetLikedSongsFromCurrentLoggedUserAsync(Guid userId)
        {
            var likedSongs = await likedSongsRepo.GetAllAttached()
            .Where(x => x.UserId == userId)
            .Include(x => x.Song)
                .ThenInclude(s => s.Album)
            .Include(x => x.Song)
                .ThenInclude(s => s.ArtistsSongs)
                    .ThenInclude(a => a.Artist)
                        .ThenInclude(ar => ar.User)
            .ToListAsync();

            var totalLength = TimeSpan.FromTicks(
                likedSongs.Sum(x => x.Song.Length.Ticks));

            return new LikedSongsPageViewModel
            {
                SongsCount = likedSongs.Count,
                Length = totalLength,
                LikedSongs = likedSongs.Select(x => new LikedSongsIndexViewModel
                {
                    Id = x.Id,
                    SongId = x.SongId,
                    Title = x.Song.Title,
                    AlbumTitle = x.Song.Album == null ? "Single" : x.Song.Album.Title,
                    Minutes = x.Song.Length.Minutes,
                    Seconds = x.Song.Length.Seconds,
                    ImageUrl = x.Song.ImageURL,
                    DateAdded = x.DateAdded,
                    Artists = x.Song.ArtistsSongs.Select(a => a.Artist.User.UserName).ToList()
                }).ToList()
            };
        }

        public async Task<LikedSongsIndexViewModel?> GetLikedSongByIdAsync(Guid id)
        {
            return await likedSongsRepo.GetAllAttached()
                .Where(ls => ls.Id == id)
                .Select(ls => new LikedSongsIndexViewModel
                {
                    Id = id,
                    SongId = ls.SongId,
                    Title = ls.Song.Title,
                    AlbumTitle = ls.Song.Album == null ? "Single" : ls.Song.Album.Title,
                    Minutes = ls.Song.Length.Minutes,
                    Seconds = ls.Song.Length.Seconds,
                    ImageUrl = ls.Song.ImageURL,
                    DateAdded = ls.DateAdded,
                    Artists = ls.Song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList(),
                }).FirstOrDefaultAsync();
        }

        public async Task<LikedSongs?> GetLikedSongByUserAndSongAsync(Guid userId, Guid songId)
        {
            return await likedSongsRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.SongId == songId);
        }

        public async Task AddLikedSongAsync(LikedSongsCreateViewModel model, Guid userId)
        {
            var isLiked = await GetLikedSongByUserAndSongAsync(userId, model.SongId);

            //likedSongsRepo.GetAllAttached()
            //    .AnyAsync(x => x.UserId == userId && x.SongId == model.SongId);

            if (isLiked == null)
            {
                throw new KeyNotFoundException($"Liked song with Id of song {model.SongId} does not exists!");
            }

            var song = await songRepo.GetByIdAsync(model.SongId);
            if (song == null)
            {
                throw new KeyNotFoundException($"Song with ID {model.SongId} is not found!");
            }

            var likedSong = new LikedSongs
            {
                SongId = model.SongId,
                UserId = userId,
                DateAdded = DateOnly.FromDateTime(DateTime.Now),
            };

            await likedSongsRepo.AddAsync(likedSong);

            song.Likes++;
            await songRepo.UpdateAsync(song);
        }

        public async Task RemoveLikedSongAsync(Guid userId, Guid songId)
        {
            var likedSong = await this.GetLikedSongByUserAndSongAsync(userId, songId);

            if (likedSong == null)
            {
                throw new KeyNotFoundException($"Song with ID {songId} is not found!");
            }

            var song = await songRepo.GetByIdAsync(songId);

            if (song == null)
            {
                return;
            }

            if (song.Likes > 0)
            {
                song.Likes--;
                await songRepo.UpdateAsync(song);
            }

            await likedSongsRepo.RemoveAsync(likedSong);
        }
    }
}
