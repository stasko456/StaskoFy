using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Library;
using StaskoFy.ViewModels.LikedSongs;
using StaskoFy.ViewModels.Playlist;
using StaskoFy.ViewModels.Song;
using System.Security.Claims;
namespace StaskoFy.Controllers
{
    public class LibraryController : Controller
    {

        private readonly IPlaylistService playlistService;
        private readonly ILikedSongsService likedSongsService;
        private readonly ISongService songService;
        private readonly IAlbumService albumService;

        public LibraryController(IPlaylistService _playlistService, ILikedSongsService _likedSongsService, ISongService _songService, IAlbumService _albumService)
        {
            this.playlistService = _playlistService;
            this.likedSongsService = _likedSongsService;
            this.songService = _songService;
            this.albumService = _albumService;
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
                LikedSongs = likedSongs
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> MyProjectsForCurrentLoggedArtistIndex()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var singles = await songService.GetSinglesForCurrentLoggedArtistAsync(Guid.Parse(userId));

            var albums = await albumService.GetSpecificArtistAlbumsAsync(Guid.Parse(userId));
            
            if (!singles.Any())
            {
                ViewData["NoResultSongs"] = "You have not released any singles yet.";
            }

            if (!albums.Any())
            {
                ViewData["NoResultAlbums"] = "You have not released any albums yet.";
            }

            var viewModel = new MyProjectsViewModel
            {
                Singles = new List<SongIndexViewModel>(singles),
                Albums = new List<AlbumIndexViewModel>(albums)
            };

            return View(viewModel);
        }
    }

}
