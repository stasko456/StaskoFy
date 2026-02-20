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
        private readonly ISongService songService;

        public LikedSongsController(ILikedSongsService _likedSongsService, ISongService _songService)
        {
            this.likedSongsService = _likedSongsService;
            this.songService = _songService;
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
        public async Task<IActionResult> Create(Guid songId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new LikedSongsCreateViewModel
            {
                SongId = songId,
            };

            await likedSongsService.AddLikedSongAsync(model, Guid.Parse(userId));
            return RedirectToAction("LikedSongsIndexForCurrentLoggedUser");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Delete(Guid songId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await likedSongsService.RemoveLikedSongAsync(Guid.Parse(userId), songId);
            return RedirectToAction("LikedSongsIndexForCurrentLoggedUser");
        }
    }
}
