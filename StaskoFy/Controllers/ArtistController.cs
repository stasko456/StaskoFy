using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;

namespace StaskoFy.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;

        public ArtistController(IArtistService _artistService)
        {
            this.artistService = _artistService;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await artistService.GetArtistsAsync();
            return View(artists);
        }
    }
}
