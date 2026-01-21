using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IServices;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Genre;
using MODELS = StaskoFy.Core.DTOs;

namespace StaskoFy.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService _genreService)
        {
            this.genreService = _genreService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var genres = genreService.GetAll();

            var model = genres.Select(x => new GenreIndexViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Songs = x.Songs,
            });

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new GenreCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(GenreCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var genre = new MODELS.Genre
            {
                Name = model.Name
            };

            await genreService.AddAsync(genre);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var genre = await genreService.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            var model = new GenreEditViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(GenreEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var genre = new MODELS.Genre
            {
                Id = model.Id,
                Name = model.Name,
            };

            await genreService.UpdateAsync(genre); // get serviceModel as params

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var genre = await genreService.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            await genreService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}