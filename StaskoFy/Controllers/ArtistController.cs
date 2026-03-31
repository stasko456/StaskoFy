using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
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
    }
}
