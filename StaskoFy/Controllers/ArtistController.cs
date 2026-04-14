using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using System.Security.Claims;
namespace StaskoFy.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IPlaylistService playlistService;
        private readonly ILogger<ArtistController> logger;

        public ArtistController(IArtistService _artistService,
                                IPlaylistService _playlistService,
                                ILogger<ArtistController> _logger)
        {
            this.artistService = _artistService;
            this.playlistService = _playlistService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> ArtistDetails(Guid artistUserId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (artistUserId == Guid.Empty)
            {
                return BadRequest();
            }

            var artist = await artistService.GetArtistByIdWithProjectsAsync(artistUserId);

            if (artist == null)
            {
                return NotFound();
            }

            if (!artist.Singles.Any())
            {
                ViewData["NoSingles"] = "This artist has not uploaded any singles.";
            }
            if (!artist.Albums.Any())
            {
                ViewData["NoAlbums"] = "This artist has not uploaded any albums.";
            }
            if (!artist.Singles.Any())
            {
                ViewData["NoPlaylists"] = "This artist has not uploaded any public playlists.";
            }
            if (!artist.LoggedUserPlaylists.Any())
            {
                ViewData["NoCurrentLoggedUserPlaylists"] = "No playlists found.";
            }

            var playlists = await playlistService.SelectPlaylistsFromCurrentLoggedUserAsync(Guid.Parse(userId));

            artist.LoggedUserPlaylists = playlists.ToList();

            return View(artist);
        }

        [HttpGet]
        public IActionResult ArtistNotAcceptedPage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptArtist(Guid id)
        {
            try
            {
                await artistService.AcceptArtistAsync(id);
                return RedirectToAction("ManageArtistsStatus", "Admin");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectArtist(Guid id)
        {
            try
            {
                await artistService.RejectArtistAsync(id);
                return RedirectToAction("ManageArtistsStatus", "Admin");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }
    }
}
