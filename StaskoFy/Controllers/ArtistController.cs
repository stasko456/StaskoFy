using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using System.Security.Claims;
namespace StaskoFy.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;

        public ArtistController(IArtistService _artistService)
        {
            this.artistService = _artistService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.GetArtistsAsync(Guid.Parse(userId));
            return View(artists);
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var artists = await artistService.GetArtistByIdAsync(id);

            if (artists == null)
            {
                return NotFound();
            }

            return View(artists);
        }
    }
}
