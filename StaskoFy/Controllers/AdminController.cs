using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StaskoFy.Core.IService;
using System.Threading.Tasks;

namespace StaskoFy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISongService songService;
        private readonly IAlbumService albumService;

        public AdminController(ISongService _songService, IAlbumService _albumService)
        {
            this.songService = _songService;
            this.albumService = _albumService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageSongsStatus()
        {
            var songs = await songService.GetSongsWithPendingStatusAsync();

            if (!songs.Any())
            {
                ViewData["NoResult"] = "No songs waiting for acception."; 
            }

            return View(songs);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageAlbumsStatus()
        {
            var albums = await albumService.GetAlbumsWithPendingStatusAsync();

            if (!albums.Any())
            {
                ViewData["NoResult"] = "No albums waiting for acception.";
            }

            return View(albums);
        }
    }
}
