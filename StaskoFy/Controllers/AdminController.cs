using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Pagination;

namespace StaskoFy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISongService songService;
        private readonly IAlbumService albumService;
        private readonly IGenreService genreService;
        private readonly IArtistService artistService;

        public AdminController(ISongService _songService,
                               IAlbumService _albumService,
                               IGenreService _genreService,
                               IArtistService _artistService)
        {
            this.songService = _songService;
            this.albumService = _albumService;
            this.genreService = _genreService;
            this.artistService = _artistService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageSongsStatus(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var songs = await songService.FilterSongsWithPendingStatusAsync(name, pageNumber, pageSize);
            int totalPendingSongs = await songService.GetTotalPendingPagesAsync(pageSize);

            var viewModel = new PendingSongsViewModel
            {
                Songs = songs.ToList(),
                TotalPages = totalPendingSongs,
                CurrentPage = pageNumber
            };

            if (!songs.Any())
            {
                ViewData["NoResult"] = "No songs found matching your search."; 
            }

            return View(viewModel);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageAlbumsStatus(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var albums = await albumService.FilterAlbumsWithPendingStatusAsync(name, pageNumber, pageSize);
            int totalPendingSongs = await songService.GetTotalPendingPagesAsync(pageSize);

            var viewModel = new PendingAlbumsViewModel
            {
                Albums = albums.ToList(),
                TotalPages = totalPendingSongs,
                CurrentPage = pageNumber
            };

            if (!albums.Any())
            {
                ViewData["NoResult"] = "No albums found matching your search.";
            }

            return View(viewModel);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageGenresStatus(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var genres = await genreService.FilterDeletedGenresAsync(name, pageNumber, pageSize);
            int totalPages = await genreService.GetTotalDeletedPagesAsync(pageSize);

            var viewModel = new DeletedGenresPaginationViewModel
            {
                Genres = genres.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
            };

            if (!genres.Any())
            {
                ViewData["NoResult"] = "No genres found matching your search.";
            }

            return View(viewModel);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageArtistsStatus(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var artists = await artistService.FilterArtistsWithPendingStatusAsync(name, pageNumber, pageSize);
            int totalPendingArtists = await artistService.GetTotalPendingPagesAsync(pageSize);

            var viewModel = new PendingArtistsViewModel
            {
                Artists = artists.ToList(),
                TotalPages = totalPendingArtists,
                CurrentPage = pageNumber
            };

            if (!artists.Any())
            {
                ViewData["NoResult"] = "No artists found matching your search.";
            }

            return View(viewModel);
        }
    }
}
