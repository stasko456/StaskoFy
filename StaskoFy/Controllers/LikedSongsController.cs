using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.LikedSongs;
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
        public async Task<IActionResult> LikedSongsIndexForCurrentLoggedUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserLikedSongs = await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(Guid.Parse(userId));

            return View(currentUserLikedSongs);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> AddLikedSong(Guid songId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new LikedSongsCreateViewModel
            {
                SongId = songId,
            };

            await likedSongsService.AddLikedSongAsync(viewModel, Guid.Parse(userId));
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
