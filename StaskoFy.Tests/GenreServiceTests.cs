using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class GenreServiceTests
    {
        private StaskoFyDbContext context;
        private Repository<Genre> genreRepo;
        private GenreService genreService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);
            genreRepo = new Repository<Genre>(context);
            genreService = new GenreService(genreRepo);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        // ── GetGenresAsync ──

        [Test]
        public async Task GetGenresAsync_ReturnsAllGenres()
        {
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Rock" });
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Jazz" });

            var result = (await genreService.GetGenresAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        // ── GetGenreByIdAsync ──

        [Test]
        public async Task GetGenreByIdAsync_ReturnsGenreWhenFound()
        {
            var id = Guid.NewGuid();
            await genreRepo.AddAsync(new Genre { Id = id, Name = "Pop" });

            var result = await genreService.GetGenreByIdAsync(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Pop"));
        }

        [Test]
        public void GetGenreByIdAsync_ThrowsWhenNotFound()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await genreService.GetGenreByIdAsync(Guid.NewGuid()));
        }

        // ── AddGenreAsync ──

        [Test]
        public async Task AddGenreAsync_AddsGenreToDatabase()
        {
            var model = new GenreCreateViewModel { Name = "Classical" };

            await genreService.AddGenreAsync(model);

            var genres = await genreRepo.GetAllAttached().ToListAsync();
            Assert.That(genres.Count, Is.EqualTo(1));
            Assert.That(genres.First().Name, Is.EqualTo("Classical"));
        }

        // ── UpdateGenreAsync ──

        [Test]
        public async Task UpdateGenreAsync_UpdatesGenre()
        {
            var id = Guid.NewGuid();
            await genreRepo.AddAsync(new Genre { Id = id, Name = "Old Name" });

            var model = new GenreEditViewModel { Id = id, Name = "New Name" };
            await genreService.UpdateGenreAsync(model);

            var updated = await genreRepo.GetByIdAsync(id);
            Assert.That(updated!.Name, Is.EqualTo("New Name"));
        }

        [Test]
        public void UpdateGenreAsync_ThrowsWhenNotFound()
        {
            var model = new GenreEditViewModel { Id = Guid.NewGuid(), Name = "Doesn't exist" };

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await genreService.UpdateGenreAsync(model));
        }

        // ── GetTotalPagesAsync ──

        [Test]
        public async Task GetTotalPagesAsync_ReturnsCorrectPageCount()
        {
            for (int i = 0; i < 7; i++)
                await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = $"Genre {i}" });

            var result = await genreService.GetTotalPagesAsync(5);

            Assert.That(result, Is.EqualTo(2)); // 7 / 5 = 1.4 -> ceil = 2
        }

        // ── FilterGenresAsync ──

        [Test]
        public async Task FilterGenresAsync_FiltersByName()
        {
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Rock" });
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Rockabilly" });
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Jazz" });

            var result = (await genreService.FilterGenresAsync("Rock", 1, 10)).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task FilterGenresAsync_RespectsPagination()
        {
            for (int i = 0; i < 10; i++)
                await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = $"Genre {i}" });

            var result = (await genreService.FilterGenresAsync("", 2, 4)).ToList();

            Assert.That(result.Count, Is.EqualTo(4));
        }

        // ── GetGenresCountAsync ──

        [Test]
        public async Task GetGenresCountAsync_ReturnsTotalGenres()
        {
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "A" });
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "B" });
            await genreRepo.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "C" });

            var result = await genreService.GetGenresCountAsync();

            Assert.That(result, Is.EqualTo(3));
        }
    }
}
