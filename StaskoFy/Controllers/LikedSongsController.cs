using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Pagination;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaskoFy.Controllers
{
    public class LikedSongsController : Controller
    {
        private readonly ILikedSongsService likedSongsService;

        public LikedSongsController(ILikedSongsService _likedSongsService)
        {
            this.likedSongsService = _likedSongsService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> LikedSongsIndexForCurrentLoggedUser(string name, int pageNumber = 1)
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

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> AddLikedSong(Guid songId, string? returnURL)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new LikedSongsCreateViewModel
            {
                SongId = songId,
            };

            await likedSongsService.AddLikedSongAsync(viewModel, Guid.Parse(userId));

            if (!string.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
            {
                return Redirect(returnURL);
            }
            return RedirectToAction("SongsIndexForAllUsers", "Song");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> RemoveLikedSong(Guid songId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await likedSongsService.RemoveLikedSongAsync(Guid.Parse(userId), songId);
            return RedirectToAction("LikedSongsIndexForCurrentLoggedUser");
        }
    }
}
