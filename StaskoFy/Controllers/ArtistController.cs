using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Playlist;
using System.Security.Claims;
namespace StaskoFy.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IPlaylistService playlistService;

        public ArtistController(IArtistService _artistService,
                                IPlaylistService _playlistService)
        {
            this.artistService = _artistService;
            this.playlistService = _playlistService;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var artist = await artistService.GetArtistByIdWithProjectsAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            var playlists = await playlistService.SelectPlaylistsFromCurrentLoggedUserAsync(Guid.Parse(userId));

            artist.LoggedUserPlaylists = playlists.ToList();

            return View(artist);
        }
    }
}
