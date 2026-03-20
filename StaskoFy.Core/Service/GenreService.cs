using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> genreRepo;
        private readonly IRepository<Song> songRepo;
        private readonly IRepository<Album> albumRepo;

        public GenreService(IRepository<Genre> _genreRepo,
                            IRepository<Song> _songRepo,
                            IRepository<Album> _albumRepo)
        {
            this.genreRepo = _genreRepo;
            this.songRepo = _songRepo;
            this.albumRepo = _albumRepo;
        }

        public async Task<IEnumerable<GenreIndexViewModel>> GetGenresAsync()
        {
            return await genreRepo.GetAllAttached()
                .Where(g => g.Status == UploadStatus.Approved)
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToListAsync();
        }

        public async Task<GenreIndexViewModel?> GetGenreByIdAsync(Guid id)
        {
            return await genreRepo.GetAllAttached()
                .Where(g => g.Id == id)
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).FirstOrDefaultAsync();
        }

        public async Task AddGenreAsync(GenreCreateViewModel model)
        {
            var genre = new Genre
            {
                Name = model.Name,
                Status = UploadStatus.Approved
            };

            await genreRepo.AddAsync(genre);
        }

        public async Task UpdateGenreAsync(GenreEditViewModel model)
        {
            var genre = await genreRepo.GetByIdAsync(model.Id);

            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {model.Id} is not found!");
            }

            genre.Name = model.Name;

            await genreRepo.UpdateAsync(genre);
        }

        public async Task RemoveGenreAsync(Guid id)
        {
            var genre = await genreRepo.GetAllAttached()
                .Include(g => g.Songs)
                    .ThenInclude(a => a.Album)
                .FirstOrDefaultAsync(g => g.Id == id);
            
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} is not found!");
            }

            genre.Status = UploadStatus.Deleted;

            foreach (var song in genre.Songs)
            {
                song.Status = UploadStatus.Deleted;
                song.LikedSongs.Clear();
                song.PlaylistSongs.Clear();
                song.Likes = 0;

                if (song.Album != null)
                {
                    song.Album.Length = song.Album.Length - song.Length;
                    song.AlbumId = null;
                }

                song.ImageURL = "/images/defaults/default-song-cover-art.png";
                song.CloudinaryPublicId = "";
            }

            await genreRepo.UpdateAsync(genre);
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 5)
        {
            int totalGenres = await genreRepo.GetAllAttached()
                .Where(g => g.Status == UploadStatus.Approved)
                .CountAsync();

            return (int)Math.Ceiling(totalGenres / (double)pageSize);
        }

        public async Task<IEnumerable<GenreIndexViewModel>> FilterGenresAsync(string name, int pageNumber = 1, int pageSize = 5)
        {
            var query = genreRepo.GetAllAttached()
                .Where(g => g.Status == UploadStatus.Approved);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(g => EF.Functions.Like(g.Name, $"%{name}%"));
            }

            return await query
                .Select(g => new GenreIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<GenreApprovalViewModel>> FilterDeletedGenresAsync(string name, int pageNumber = 1, int pageSize = 5)
        {
            var query = genreRepo.GetAllAttached()
                .Where(g => g.Status == UploadStatus.Deleted);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(g => EF.Functions.Like(g.Name, $"%{name}%"));
            }

            return await query
                .Select(g => new GenreApprovalViewModel
                {
                    Id = g.Id,
                    Name= g.Name,
                    Status = g.Status
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AcceptGenreUploadAsync(Guid id)
        {
            var genre = await genreRepo.GetAllAttached()
                .Include(g => g.Songs)
                    .ThenInclude(a => a.Album)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} is not found!");
            }

            genre.Status = UploadStatus.Deleted;

            foreach (var song in genre.Songs)
            {
                song.Status = UploadStatus.Approved;
            }

            await genreRepo.UpdateAsync(genre);
        }
    }
}
