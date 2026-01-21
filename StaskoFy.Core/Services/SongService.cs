using StaskoFy.Core.IServices;
using StaskoFy.Core.Models;
using StaskoFy.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MODELS = StaskoFy.Core.Models;
using ENTITIES = StaskoFy.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;

namespace StaskoFy.Core.Services
{
    public class SongService : ISongService
    {
        private readonly IRepository<ENTITIES.Song> songRepo;

        public SongService(IRepository<ENTITIES.Song> _songRepo)
        {
            this.songRepo = _songRepo;
        }

        public async Task<IEnumerable<MODELS.Song>> GetAllAttached()
        {
            return await songRepo.GetAllAttached()
                .Select(s => new MODELS.Song
                {
                    Id = s.Id,
                    Title = s.Title,
                    Length = s.Length,
                    ReleaseDate = s.ReleaseDate,
                    AlbumId = s.AlbumId,
                    Genre = s.Genre,
                    Likes = s.Likes,
                    ImageURL = s.ImageURL,
                }).ToListAsync();
        }

        public async Task<MODELS.Song> GetByIdAsync(Guid? id)
        {
            var song = await songRepo.GetByIdAsync(id);

            return new MODELS.Song
            {
                Id = song.Id,
                Title = song.Title,
                Length = song.Length,
                ReleaseDate = song.ReleaseDate,
                AlbumId = song.AlbumId,
                Genre = song.Genre,
                Likes = song.Likes,
                ImageURL = song.ImageURL,
            };
        }

        public async Task AddAsync(MODELS.Song model)
        {
            var song = new ENTITIES.Song
            {
                Id = model.Id,
                Title = model.Title,
                Length = model.Length,
                ReleaseDate = model.ReleaseDate,
                AlbumId = model.AlbumId,
                Genre = model.Genre,
                Likes = model.Likes,
                ImageURL = model.ImageURL,
            };

            await songRepo.AddAsync(song);
        }

        public async Task AddRangeAsync(IEnumerable<MODELS.Song> models)
        {
            List<ENTITIES.Song> songsToAdd = new List<ENTITIES.Song>();

            foreach (var model in models)
            {
                var song = new ENTITIES.Song
                {
                    Id = model.Id,
                    Title = model.Title,
                    Length = model.Length,
                    ReleaseDate = model.ReleaseDate,
                    AlbumId = model.AlbumId,
                    Genre = model.Genre,
                    Likes = model.Likes,
                    ImageURL = model.ImageURL,
                };

                songsToAdd.Add(song);
            }

            await songRepo.AddRangeAsync(songsToAdd);
        }

        public async Task UpdateAsync(MODELS.Song model)
        {
            var song = await songRepo.GetByIdAsync(model.Id);

            song.Title = model.Title;
            song.Length = model.Length;
            song.ReleaseDate = model.ReleaseDate;
            song.AlbumId = model.AlbumId;
            song.Genre = model.Genre;
            song.Likes = model.Likes;
            song.ImageURL = model.ImageURL;

            await songRepo.UpdateAsync(song);
        }

        public async Task RemoveAsync(Guid? id)
        {
            var song = await songRepo.GetByIdAsync(id);

            await songRepo.RemoveAsync(song);
        }

        public async Task RemoveRangeAsync(IEnumerable<Guid?> ids)
        {
            List<Guid> idsToAdd = new List<Guid>();
            List<ENTITIES.Song> songsToRemove = new List<ENTITIES.Song>();

            foreach (var id in ids)
            {
                var song = await songRepo.GetByIdAsync(id);

                songsToRemove.Add(song);
            }

            await songRepo.RemoveRangeAsync(songsToRemove);
        }
    }
}