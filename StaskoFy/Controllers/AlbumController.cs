using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Pagination;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IArtistService artistService;
        private readonly ISongService songService;
        private readonly ILogger<AlbumController> logger;

        public AlbumController(IAlbumService _albumService,
                               IArtistService _artistService,
                               ISongService _songService,
                               ILogger<AlbumController> _logger)
        {
            this.albumService = _albumService;
            this.artistService = _artistService;
            this.songService = _songService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Index(string searchItem, List<string> filters, int pageNumber = 1)
        {
            int pageSize = 4;
            var albums = await albumService.FilterAlbumsAsync(searchItem, filters, pageNumber, pageSize);
            int totalPages = await albumService.GetTotalPagesAsync(pageSize);

            var viewModel = new AlbumsPaginationViewModel
            {
                Albums = albums.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
            };

            if (!albums.Any())
            {
                ViewData["NoResult"] = "No albums found matching your search.";
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<ActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));

            var viewModel = new AlbumCreateViewModel
            {
                Artists = new MultiSelectList(artists, "Id", "Username"),
                Songs = new MultiSelectList(songs, "Id", "Title"),
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumCreateViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                viewModel.Artists = new MultiSelectList(artists, "Id", "Username");

                var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));
                viewModel.Songs = new MultiSelectList(songs, "Id", "Title");

                return View(viewModel);
            }

            await albumService.AddAlbumAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyProjectsIndex", "Library");
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await albumService.GetAlbumByIdAsync(id);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var allArtists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                ViewBag.AllArtists = allArtists;

                var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));
                viewModel.Songs = new MultiSelectList(songs, "Id", "Title");

                return View(viewModel);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlbumEditViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                viewModel.Artists = new MultiSelectList(artists, "Id", "Username", viewModel.SelectedArtistIds.AsEnumerable());
                
                var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));
                viewModel.Songs = new MultiSelectList(songs, "Id", "Title");
                
                return View(viewModel);
            }

            await albumService.UpdateAlbumAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyProjectsIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, string sourceController, string sourceAction)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await albumService.RemoveAlbumAsync(id);
                return RedirectToAction(sourceAction, sourceController);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var album = await albumService.GetAlbumByIdWithSongsAsync(id);
                return View(album);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptAlbumUpload(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await albumService.AcceptAlbumUploadAsync(id);
                return RedirectToAction("ManageAlbumsStatus", "Admin");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectAlbumUpload(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await albumService.RejectAlbumUploadAsync(id);
                return RedirectToAction("ManageAlbumsStatus", "Admin");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpGet]
        [Route("Album/GetAlbumSongsForQueue")]
        public async Task<IActionResult> GetAlbumSongsForQueue(Guid id)
        {
            var songs = await albumService.GetSongsFromAlbumByIdForMusicPlayerAsync(id);

            if (songs == null || !songs.Any())
            {
                return NoContent();
            }

            var result = songs.Select(s => new
            {
                id = s.Id,
                title = s.Title,
                artCover = s.ImageURL,
                duration = $"{s.Duration.Minutes + ":" + s.Duration.Seconds}",
                artists = s.Artists.ToArray(),
                audioUrl = s.AudioURL
            });

            return Json(result);
        }
    }
}