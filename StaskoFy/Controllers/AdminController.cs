using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StaskoFy.Core.IService;
using System.Threading.Tasks;
using StaskoFy.ViewModels.Pagination;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace StaskoFy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISongService songService;
        private readonly IAlbumService albumService;
        private readonly IGenreService genreService;

        public AdminController(ISongService _songService,
                               IAlbumService _albumService,
                               IGenreService _genreService)
        {
            this.songService = _songService;
            this.albumService = _albumService;
            this.genreService = _genreService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageSongsStatus(string title, int pageNumber = 1)
        {
            int pageSize = 5;
            var songs = await songService.FilterSongsWithPendingStatusAsync(title, pageNumber, pageSize);
            int totalPendingSongs = await songService.GetTotalPendingPagesAsync(pageSize);

            var viewModel = new PendingSongsViewModel
            {
                Songs = songs.ToList(),
                TotalPages = totalPendingSongs,
                CurrentPage = pageNumber
            };

            if (!songs.Any())
            {
                ViewData["NoResult"] = "No songs waiting for acception."; 
            }

            return View(viewModel);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageAlbumsStatus(string title, int pageNumber = 1)
        {
            int pageSize = 5;
            var albums = await albumService.FilterAlbumsWithPendingStatusAsync(title, pageNumber, pageSize);
            int totalPendingSongs = await songService.GetTotalPendingPagesAsync(pageSize);

            var viewModel = new PendingAlbumsViewModel
            {
                Albums = albums.ToList(),
                TotalPages = totalPendingSongs,
                CurrentPage = pageNumber
            };

            if (!albums.Any())
            {
                ViewData["NoResult"] = "No albums waiting for acception.";
            }

            return View(viewModel);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageGenresStatus(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var genres = await genreService.FilterDeletedGenresAsync(name, pageNumber, pageSize);
            int totalPages = await genreService.GetTotalPagesAsync(pageSize);

            var viewModel = new DeletedGenresPaginationViewModel
            {
                Genres = genres.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
            };

            if (!genres.Any())
            {
                ViewData["NoResult"] = "No genres waiting for acception.";
            }

            return View(viewModel);
        }
    }
}
