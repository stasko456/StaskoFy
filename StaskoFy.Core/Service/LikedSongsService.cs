using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
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

        public async Task<int> GetTotalPagesAsync(Guid userId, int pageSize = 5)
        {
            var totalLikedSongs = await likedSongsRepo
                .GetAllAttached()
                .Where(ls => ls.Song.Status == UploadStatus.Approved && ls.UserId == userId)
                .CountAsync();

            return (int)Math.Ceiling(totalLikedSongs / (double)pageSize);
        }

        public async Task<IEnumerable<LikedSongsIndexViewModel>> GetLikedSongsFromCurrentLoggedUserAsync(Guid userId, string name, int pageNumber = 1, int pageSize = 5)
        {
            var query = likedSongsRepo.GetAllAttached()
                .Where(ls => ls.UserId == userId && ls.Song.Status == UploadStatus.Approved);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => EF.Functions.Like(s.Song.Title, $"%{name}%"));
            }

            return await query
                .Select(ls => new LikedSongsIndexViewModel
                {
                    Id = ls.Id,
                    SongId = ls.SongId,
                    Title = ls.Song.Title,
                    AlbumTitle = ls.Song.Album == null ? "Single" : ls.Song.Album.Title,
                    Minutes = ls.Song.Length.Minutes,
                    Seconds = ls.Song.Length.Seconds,
                    ImageUrl = ls.Song.ImageURL,
                    DateAdded = ls.DateAdded,
                    Artists = ls.Song.ArtistsSongs.Select(a => a.Artist.User.UserName).ToList()
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<LikedSongs?> GetLikedSongByUserAndSongAsync(Guid userId, Guid songId)
        {
            return await likedSongsRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.SongId == songId);
        }

        public async Task AddLikedSongAsync(LikedSongsCreateViewModel model, Guid userId)
        {
            var isLiked = await GetLikedSongByUserAndSongAsync(userId, model.SongId);

            var song = await songRepo.GetByIdAsync(model.SongId);
            if (song is null)
            {
                throw new NullReferenceException("Unable to find this song!");
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

            if (likedSong is null)
            {
                throw new NullReferenceException("Unable to find this liked song!");
            }

            var song = await songRepo.GetByIdAsync(songId);

            if (song is null)
            {
                throw new NullReferenceException("Unable to find this song!");
            }

            if (song.Likes > 0)
            {
                song.Likes--;
                await songRepo.UpdateAsync(song);
            }

            await likedSongsRepo.RemoveAsync(likedSong);
        }

        public Task<int> GetTotalLikedSongsByCurrentLoggedUserAsync(Guid userId)
        {
            return likedSongsRepo.GetAllAttached()
                .Where(ls => ls.UserId == userId)
                .CountAsync();
        }

        public async Task<TimeSpan> GetLengthOfLikedSongsByCurrentLoggedUserAsync(Guid userId)
        {
            var songs = likedSongsRepo.GetAllAttached()
            .Include(ls => ls.Song)
            .Where(ls => ls.UserId == userId);

            var totalDuration = new TimeSpan(0,0,0);

            foreach (var song in songs)
            {
                totalDuration = totalDuration + song.Song.Length;
            }

            return totalDuration;
        }
    }
}
