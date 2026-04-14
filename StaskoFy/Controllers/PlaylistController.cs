 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Playlist;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.ViewModels.Pagination;

namespace StaskoFy.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playlistService;
        private readonly ILogger<PlaylistController> logger;

        public PlaylistController(IPlaylistService _playlistService,
                                  ILogger<PlaylistController> _logger)
        {
            this.playlistService = _playlistService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new PlaylistCreateViewModel{};
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlaylistCreateViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await playlistService.AddPlaylistAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyLibraryIndex", "Library");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var playlist = await playlistService.GetPlaylistByIdAsync(id);
                var viewModel = new PlaylistEditViewModel
                {
                    Id = id,
                    Title = playlist.Title,
                    IsPublic = playlist.IsPublic,
                };
                return View(viewModel);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlaylistEditViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await playlistService.UpdatePlaylistAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyLibraryIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, string sourceController, string sourceAction)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await playlistService.RemovePlaylistAsync(id);
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
        public async Task<IActionResult> Details(Guid id, string name, int pageNumber = 1)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                int pageSize = 5;
                var playlistInfo = await playlistService.GetPlaylistByIdAsync(id);
                var playlistSongs = await playlistService.GetPlaylistSongsByIdAsync(id, name, pageNumber, pageSize);
                int totalPlaylistSongsPages = await playlistService.GetTotalPlaylistSongsPagesAsync(id, pageSize);
                TimeSpan playlistLength = await playlistService.GetLengthOfPlaylistSongsByIdAsync(id);
                int songsCount = await playlistService.GetCountOfPlaylistSongsByIdAsync(id);

                var viewModel = new PlaylistSongsPaginationViewModel
                {
                    PlaylistInfo = playlistInfo,
                    Songs = playlistSongs.ToList(),
                    PlaylistLength = playlistLength,
                    SongsCount = songsCount,
                    TotalPages = totalPlaylistSongsPages,
                    CurrentPage = pageNumber,
                };

                if (!playlistSongs.Any())
                {
                    ViewData["NoPlaylistSongs"] = "No song found matching your search.";
                }

                return View(viewModel);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSongToPlaylist(Guid playlistId, Guid songId, string? returnURL)
        {
            if (songId == Guid.Empty || playlistId == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await playlistService.AddSongToPlaylistAsync(playlistId, songId);
                if (!string.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
                {
                    return Redirect(returnURL);
                }
                return RedirectToAction("MyLibraryIndex", "Library");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSongFromPlaylist(Guid playlistId, Guid songId)
        {
            if (songId == Guid.Empty || playlistId == Guid.Empty)
            {
                return BadRequest();
            }

            await playlistService.RemoveSongFromPlaylistAsync(playlistId, songId);
            return RedirectToAction("Details", new { id = playlistId });
        }

        [HttpGet]
        [Route("Playlist/GetPlaylistSongsForQueue")]
        public async Task<IActionResult> GetPlaylistSongsForQueue([FromQuery] Guid id)
        {
            var songs = await playlistService.GetSongsFromPlaylistByIdForMusicPlayerAsync(id);

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
