using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Library;
using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Playlist;
using System.Security.Claims;
namespace StaskoFy.Controllers
{
    public class LibraryController : Controller
    {

        private readonly IPlaylistService playlistService;
        private readonly ILikedSongsService likedSongsService;

        public LibraryController(IPlaylistService _playlistService, ILikedSongsService _likedSongsService)
        {
            this.playlistService = _playlistService;
            this.likedSongsService = _likedSongsService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> MyLibraryIndex()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var playlists = await playlistService.GetPlaylistsFromCurrentLoggedUserAsync(Guid.Parse(userId));
            var likedSongs = await likedSongsService.GetLikedSongsFromCurrentLoggedUserAsync(Guid.Parse(userId));

            var viewModel = new LibraryViewModel
            {
                Playlists = new List<PlaylistIndexViewModel>(playlists),
                LikedSongs = new List<LikedSongsIndexViewModel>(likedSongs)
            };

            return View(viewModel);
        }
    }
}
