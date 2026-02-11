using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.LikedSongs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class LikedSongsService : ILikedSongsService
    {
        private readonly IRepository<LikedSongs> likedSongsRepo;

        public LikedSongsService(IRepository<LikedSongs> _likedSongsRepo)
        {
            this.likedSongsRepo = _likedSongsRepo;
        }

        public async Task<IEnumerable<LikedSongsIndexViewModel>> GetAllAsync(Guid userId)
        {
            return await likedSongsRepo.GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(x => new LikedSongsIndexViewModel
                {
                    Id = x.Id,
                    Title = x.Song.Title,
                    AlbumTitle = x.Song.Album == null ? "Single" : x.Song.Album.Title,
                    Minutes = x.Song.Length.Minutes,
                    Seconds = x.Song.Length.Seconds,
                    ImageUrl = x.Song.ImageURL,
                    DateAdded = x.DateAdded,
                    Artists = x.Song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList(),
                }).ToListAsync(); 
        }

        public async Task AddAsync(LikedSongsCreateViewModel model, Guid userId)
        {
            var likedSong = new LikedSongs
            {
                SongId = model.SongId,
                UserId = userId,
                DateAdded = DateOnly.FromDateTime(DateTime.Now),
            };

            var checkIfLikedSongExists = await likedSongsRepo.GetByIdAsync(likedSong.Id);

            if (checkIfLikedSongExists == null)
            {
                await likedSongsRepo.AddAsync(likedSong);
            }
        }

        public async Task<LikedSongsIndexViewModel?> GetByIdAsync(Guid id, Guid userId)
        {
            var likedSong = await likedSongsRepo.GetByIdAsync(id);

            if (likedSong == null)
            {
                throw new KeyNotFoundException("Liked Song not found.");
            }

            return new LikedSongsIndexViewModel
            {
                Id = id,
                Title = likedSong.Song.Title,
                AlbumTitle = likedSong.Song.Album == null ? "Single" : likedSong.Song.Album.Title,
                Minutes = likedSong.Song.Length.Minutes,
                Seconds = likedSong.Song.Length.Seconds,
                ImageUrl = likedSong.Song.ImageURL,
                DateAdded = likedSong.DateAdded,
                Artists = likedSong.Song.ArtistsSongs.Select(x => x.Artist.User.UserName).ToList(),
            };
        }

        public async Task RemoveAsync(Guid id)
        {
            var likedSongToRemove = await likedSongsRepo.GetByIdAsync(id);

            await likedSongsRepo.RemoveAsync(likedSongToRemove);
        }
    }
}
