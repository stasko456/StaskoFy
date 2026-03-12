using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Models;
using StaskoFy.ViewModels.Home;

namespace StaskoFy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService songService;
        private readonly IAlbumService albumService;
        private readonly IPlaylistService playlistService;
        private readonly ILikedSongsService likedSongsService;

        public HomeController(ILogger<HomeController> logger,
                              ISongService _songService,
                              IAlbumService _albumService,
                              IPlaylistService _playlistService,
                              ILikedSongsService _likedSongsService)
        {
            _logger = logger;
            this.songService = _songService;
            this.albumService = _albumService;
            this.playlistService = _playlistService;
            this.likedSongsService = _likedSongsService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                int totalSongs = await songService.GetTotalSongsCountAsync();
                int totalPendingSongs = await songService.GetTotalPendingSongsCountAsync();
                int totalAlbums = await albumService.GetTotalAlbumsCountAsync();
                int totalPendingAlbums = await albumService.GetTotalPendingAlbumsCountAsync();

                var viewModel = new HomeAdminIndexViewModel
                {
                    TotalSongs = totalSongs,
                    TotalPendingSongs = totalPendingSongs,
                    TotalAlbums = totalAlbums,
                    TotalPendingAlbums = totalPendingAlbums
                };

                return View("AdminIndex", viewModel);
            }
            else if (User.IsInRole("Artist"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int totalSongsByArtist = await songService.GetTotalSongsCountByCurrentLoggedArtistAsync(Guid.Parse(userId));
                int totalAlbumsByArtist = await albumService.GetTotalAlbumsCountByCurrentLoggedArtistAsync(Guid.Parse(userId));
                int totalPendingSongsByArtist = await songService.GetTotalPendingSongsCountByCurrentLoggedArtistAsync(Guid.Parse(userId));
                int totalPendingAlbumsByArtist = await albumService.GetTotalPendingAlbumsCountByCurrentLoggedArtistAsync(Guid.Parse(userId));
                int TotalLikesOfSongs = await songService.GetTotalSongsLikesByCurrentLoggedArtistAsync(Guid.Parse(userId));
                var mostLikedSongByArtist = await songService.GetMostLikedSongAsync(Guid.Parse(userId));

                var viewModel = new HomeArtistIndexViewModel
                {
                    TotalSongsByArtist = totalSongsByArtist,
                    TotalAlbumsByArtist = totalAlbumsByArtist,
                    TotalPendingSongsByArtist = totalPendingSongsByArtist,
                    TotalPendingAlbumsByArtist = totalPendingAlbumsByArtist,
                    TotalLikesOfSongsByArtist = TotalLikesOfSongs,
                    MostLikedSongTitleByArtist = mostLikedSongByArtist.MostLikedSongTitle,
                    MostLikedSongCountByArtist = mostLikedSongByArtist.MostLikedSongCount
                };

                return View("ArtistIndex", viewModel);
            }
            else if (User.IsInRole("User"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int totalPlaylistsByUser = await playlistService.GetTotalPlaylistsCountByCurrentLoggedUserAsync(Guid.Parse(userId));
                int totalLikedSongsByUser = await likedSongsService.GetTotalLikedSongsByCurrentLoggedUserAsync(Guid.Parse(userId));

                var viewModel = new HomeUserIndexViewModel
                {
                    TotalPlaylistByUser = totalPlaylistsByUser,
                    TotalLikedSongsByUser = totalLikedSongsByUser
                };

                return View("UserIndex", viewModel);
            }
            return View();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AdminIndex()
        {
            return View();
        }

        [Authorize(Policy = "Artist")]
        [HttpGet]
        public async Task<IActionResult> ArtistIndex()
        {
            return View();
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public async Task<IActionResult> UserIndex()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
