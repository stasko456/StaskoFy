using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;

namespace StaskoFy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISongService songService;

        public AdminController(ISongService _songService)
        {
            this.songService = _songService;
        }

        public async Task<IActionResult> ManageSongsStatus()
        {
            var songs = await songService.GetSongsWithPendingStatusAsync();
            return View(songs);
        }
    }
}
