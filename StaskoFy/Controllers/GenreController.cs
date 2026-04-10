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
        private readonly ILogger<GenreController> logger;

        public GenreController(IGenreService _genreService,
                              ILogger<GenreController> _logger)
        {
            this.genreService = _genreService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var genres = await genreService.FilterGenresAsync(name, pageNumber, pageSize);
            int totalPages = await genreService.GetTotalPagesAsync(pageSize);
            int genresCount = await genreService.GetGenresCountAsync();

            var viewModel = new GenresPaginationViewModel
            {
                Genres = genres.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                GenresCount = genresCount
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
        [ValidateAntiForgeryToken]
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

            try
            {
                var genre = await genreService.GetGenreByIdAsync(id);
                return View(genre);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
