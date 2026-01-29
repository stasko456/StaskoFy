using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Genre;

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
        public async Task<IActionResult> Index()
        {
            var genres = await genreService.GetAllAsync();

            return View(genres);
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

            await genreService.AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var genre = await genreService.GetByIdAsync(id);

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

            await genreService.UpdateAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var genre = await genreService.GetByIdAsync(id);

            await genreService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
