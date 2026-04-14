using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Pagination;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class LikedSongsController : Controller
    {
        private readonly ILikedSongsService likedSongsService;
        private readonly ILogger<LikedSongsController> logger;

        public LikedSongsController(ILikedSongsService _likedSongsService
                                    ,ILogger<LikedSongsController> _logger)
        {
            this.likedSongsService = _likedSongsService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Index(string name, int pageNumber = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int pageSize = 5;
            var currentUserLikedSongs = await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(Guid.Parse(userId), name, pageNumber, pageSize);
            int totalPages = await likedSongsService.GetTotalPagesAsync(Guid.Parse(userId), pageSize);
            int likedSongsCount = await likedSongsService.GetTotalLikedSongsByCurrentLoggedUserAsync(Guid.Parse(userId));
            TimeSpan likedSongsLength = await likedSongsService.GetLengthOfLikedSongsByCurrentLoggedUserAsync(Guid.Parse(userId));

            var viewModel = new LikedSongsPaginationViewModel
            {
                LikedSongs = currentUserLikedSongs.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                LikedSongsCount = likedSongsCount,
                LikedSongsHours = likedSongsLength.Hours,
                LikedSongsMinutes = likedSongsLength.Minutes,
                LikedSongsSeconds = likedSongsLength.Seconds,
            };

            if (!currentUserLikedSongs.Any())
            {
                ViewData["NoResult"] = "No songs found matching your search.";
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLikedSong(Guid songId, string? returnURL)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new LikedSongsCreateViewModel
            {
                SongId = songId,
            };

            try
            {
                await likedSongsService.AddLikedSongAsync(viewModel, Guid.Parse(userId));

                if (!string.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
                {
                    return Redirect(returnURL);
                }
                return RedirectToAction("Index", "Song");
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
        public async Task<IActionResult> RemoveLikedSong(Guid songId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await likedSongsService.RemoveLikedSongAsync(Guid.Parse(userId), songId);
                return RedirectToAction("Index");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("LikedSongs/GetLikedSongsForQueue")]
        public async Task<IActionResult> GetLikedSongsForQueue()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await likedSongsService.GetLikedSongsByIdForMusicPlayerAsync(Guid.Parse(userId));

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
