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
        public async Task<IActionResult> Index(string username)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.GetFilteredArtistsAsync(Guid.Parse(userId), username);

            if (!artists.Any())
            {
                ViewData["NoResult"] = "No artist found matching your search.";
            }

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

            var artists = await artistService.GetArtistByIdWithProjectsAsync(id);

            if (artists == null)
            {
                return NotFound();
            }

            return View(artists);
        }
    }
}
