using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaskoFy.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private StaskoFyDbContext context;
        private Repository<Genre> repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StaskoFyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new StaskoFyDbContext(options);
            repository = new Repository<Genre>(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Test]
        public async Task AddAsync_AddsEntityToDatabase()
        {
            var genre = new Genre { Id = Guid.NewGuid(), Name = "Pop" };

            await repository.AddAsync(genre);

            var dbGenre = await context.Genres.FindAsync(genre.Id);
            Assert.That(dbGenre, Is.Not.Null);
            Assert.That(dbGenre!.Name, Is.EqualTo("Pop"));
        }

        [Test]
        public async Task AddRangeAsync_AddsMultipleEntitiesToDatabase()
        {
            var genres = new List<Genre>
            {
                new Genre { Id = Guid.NewGuid(), Name = "Pop" },
                new Genre { Id = Guid.NewGuid(), Name = "Rock" }
            };

            await repository.AddRangeAsync(genres);

            var count = await context.Genres.CountAsync();
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllAttached_ReturnsQueryableOfEntities()
        {
            await context.Genres.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Jazz" });
            await context.Genres.AddAsync(new Genre { Id = Guid.NewGuid(), Name = "Blues" });
            await context.SaveChangesAsync();

            var result = repository.GetAllAttached();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Is.InstanceOf<IQueryable<Genre>>());
        }

        [Test]
        public async Task GetByIdAsync_ReturnsEntityWhenFound()
        {
            var id = Guid.NewGuid();
            var genre = new Genre { Id = id, Name = "Classical" };
            await context.Genres.AddAsync(genre);
            await context.SaveChangesAsync();

            var result = await repository.GetByIdAsync(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNullWhenNotFound()
        {
            var result = await repository.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RemoveAsync_RemovesEntityFromDatabase()
        {
            var genre = new Genre { Id = Guid.NewGuid(), Name = "Metal" };
            await context.Genres.AddAsync(genre);
            await context.SaveChangesAsync();

            await repository.RemoveAsync(genre);

            var result = await context.Genres.FindAsync(genre.Id);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RemoveRangeAsync_RemovesMultipleEntitiesFromDatabase()
        {
            var genres = new List<Genre>
            {
                new Genre { Id = Guid.NewGuid(), Name = "Hip Hop" },
                new Genre { Id = Guid.NewGuid(), Name = "Folk" }
            };
            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();

            await repository.RemoveRangeAsync(genres);

            var count = await context.Genres.CountAsync();
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateAsync_UpdatesEntityInDatabase()
        {
            var genre = new Genre { Id = Guid.NewGuid(), Name = "LoFi" };
            await context.Genres.AddAsync(genre);
            await context.SaveChangesAsync();

            genre.Name = "Updated Name";
            await repository.UpdateAsync(genre);

            var updatedGenre = await context.Genres.FindAsync(genre.Id);
            Assert.That(updatedGenre!.Name, Is.EqualTo("Updated Name"));
        }
    }
}
