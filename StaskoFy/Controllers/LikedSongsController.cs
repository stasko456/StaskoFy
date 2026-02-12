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
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserLikedSongs = await likedSongsService.GetAllAsync(Guid.Parse(userId));

            return View(currentUserLikedSongs);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Create(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new LikedSongsCreateViewModel
            {
                SongId = id,
            };

            await likedSongsService.AddAsync(model, Guid.Parse(userId));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var song = await songService.GetByIdAsync(id);

            await likedSongsService.RemoveAsync(Guid.Parse(userId), song.Id);
            return RedirectToAction("Index");
        }
    }
}
