using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Genre;
using StaskoFy.ViewModels.Pagination;

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
        public async Task<IActionResult> Index(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var genres = await genreService.FilterGenresAsync(name, pageNumber, pageSize);
            int totalPages = await genreService.GetTotalPagesAsync(pageSize);

            var viewModel = new GenresPaginationViewModel
            {
                Genres = genres.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };

            if (!genres.Any())
            {
                ViewData["NoResult"] = "No genres found matching your search.";
            }

            return View(viewModel);
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
