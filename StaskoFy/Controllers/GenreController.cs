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
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            var genres = await genreService.GetGenresAsync();

            return View(genres);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            var model = new GenreCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(GenreCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await genreService.AddGenreAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var genre = await genreService.GetGenreByIdAsync(id);

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
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(GenreEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await genreService.UpdateGenreAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var genre = await genreService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            await genreService.RemoveGenreAsync(id);
            return RedirectToAction("Index");
        }
    }
}
