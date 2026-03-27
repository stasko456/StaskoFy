using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Pagination;
using StaskoFy.ViewModels.Song;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly IArtistService artistService;
        private readonly IImageService imageService;
        private readonly IPlaylistService playlistService;
        private readonly IAlbumService albumService;
        private readonly ILogger<SongController> logger;

        public SongController(ISongService _songService,
                              IGenreService _genreService,
                              IArtistService _artistService,
                              IImageService _imageService,
                              IPlaylistService _playlistService,
                              IAlbumService _albumService,
                              ILogger<SongController> _logger)
        {
            this.songService = _songService;
            this.genreService = _genreService;
            this.artistService = _artistService;
            this.imageService = _imageService;
            this.playlistService = _playlistService;
            this.albumService = _albumService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> SongsIndexForAllUsers(string searchItem, List<string> filters, int pageNumber = 1)
        {
            int pageSize = 12;
            var songs = await songService.FilterSongsAsync(searchItem, filters, pageNumber, pageSize);
            int totalPages = await songService.GetTotalPagesAsync(pageSize);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = await playlistService.SelectPlaylistsFromCurrentLoggedUserAsync(Guid.Parse(userId));

            var viewModel = new SongsPaginationViewModel()
            {
                Songs = songs.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                CurrentLoggedUserPlaylists = playlists.ToList(),
            };

            if (!songs.Any())
            {
                ViewData["NoResult"] = "No songs found matching your search.";
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await genreService.GetGenresAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var viewModel = new SongCreateViewModel
            {
                Artists = new MultiSelectList(artists, "Id", "Username")
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongCreateViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var genres = await genreService.GetGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                viewModel.Artists = new MultiSelectList(artists, "Id", "Username");

                return View(viewModel);
            }
            await songService.AddSongAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
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
                var viewModel = await songService.GetSongByIdAsync(id);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var allArtists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                ViewBag.AllArtists = allArtists;

                var genres = await genreService.GetGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

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
        public async Task<IActionResult> Edit(SongEditViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var genres = await genreService.GetGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                viewModel.Artists = new MultiSelectList(artists, "Id", "Username", viewModel.SelectedArtistIds.AsEnumerable());

                return View(viewModel);
            }

            await songService.UpdateSongsAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await songService.RemoveSongAsync(id);
                return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var song = await songService.GetSongDetailsByIdAsync(id, Guid.Parse(userId));
                return View(song);
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
        public async Task<IActionResult> MakeSongSingle(Guid songId, Guid albumId)
        {
            if (songId == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await albumService.RemoveSongFromAlbumAsync(songId, albumId);
                return RedirectToAction("Details", "Album", new { id = albumId });
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
        public async Task<IActionResult> AddSongToAlbum(Guid songId, Guid albumId, string? returnUrl)
        {
            if (songId == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await albumService.AddSongToAlbumAsync(songId, albumId);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Details", "Album", new { id = albumId });
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
        public async Task<IActionResult> AcceptSongUpload(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await songService.AcceptSongUploadAsync(id);
                return RedirectToAction("ManageSongsStatus", "Admin");
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
        public async Task<IActionResult> RejectSongUpload(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await songService.RejectSongUploadAsync(id);
                return RedirectToAction("ManageSongsStatus", "Admin");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

    }
}
