using Microsoft.EntityFrameworkCore;
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

        public LikedSongsService(IRepository<LikedSongs> _likedSongsRepo, IRepository<Song> _songRepo)
        {
            this.likedSongsRepo = _likedSongsRepo;
            this.songRepo = _songRepo;
        }

        public async Task<IEnumerable<LikedSongsIndexViewModel>> GetAllFromCurrentLoggedUserAsync(Guid userId)
        {
            return await likedSongsRepo.GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(x => new LikedSongsIndexViewModel
                {
                    Id = x.Id,
                    SongId = x.SongId,
                    Title = x.Song.Title,
                    AlbumTitle = x.Song.Album == null ? "Single" : x.Song.Album.Title,
                    Minutes = x.Song.Length.Minutes,
                    Seconds = x.Song.Length.Seconds,
                    ImageUrl = x.Song.ImageURL,
                    DateAdded = x.DateAdded,
                    Artists = x.Song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList(),
                }).ToListAsync(); 
        }

        public async Task<LikedSongs?> GetByUserAndSongAsync(Guid userId, Guid songId)
        {
            return await likedSongsRepo.GetAllAttached()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.SongId == songId);
        }

        public async Task AddAsync(LikedSongsCreateViewModel model, Guid userId)
        {
            var isLiked = await likedSongsRepo.GetAllAttached()
                .AnyAsync(x => x.UserId == userId && x.SongId == model.SongId);

            if (isLiked)
            {
                return;
            }

            var song = await songRepo.GetByIdAsync(model.SongId);
            if (song == null)
            {
                return;
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

        public async Task<LikedSongsIndexViewModel?> GetByIdAsync(Guid id)
        {
            var likedSong = await likedSongsRepo.GetByIdAsync(id);

            if (likedSong == null)
            {
                throw new KeyNotFoundException("Liked Song not found.");
            }

            return new LikedSongsIndexViewModel
            {
                Id = id,
                SongId = likedSong.SongId,
                Title = likedSong.Song.Title,
                AlbumTitle = likedSong.Song.Album == null ? "Single" : likedSong.Song.Album.Title,
                Minutes = likedSong.Song.Length.Minutes,
                Seconds = likedSong.Song.Length.Seconds,
                ImageUrl = likedSong.Song.ImageURL,
                DateAdded = likedSong.DateAdded,
                Artists = likedSong.Song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList(),
            };
        }

        public async Task RemoveAsync(Guid userId, Guid songId)
        {
            var likedSong = await this.GetByUserAndSongAsync(userId, songId);

            if (likedSong == null)
            {
                return;
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
