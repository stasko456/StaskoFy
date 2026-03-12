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
            var viewModel = new GenreCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(GenreCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await genreService.AddGenreAsync(viewModel);
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

            var viewModel = new GenreEditViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(GenreEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await genreService.UpdateGenreAsync(viewModel);

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
