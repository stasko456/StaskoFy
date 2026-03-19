using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using StaskoFy.ViewModels.Pagination;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaskoFy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IArtistService artistService;
        private readonly ISongService songService;

        public AlbumController(IAlbumService _albumService,
                               IArtistService _artistService,
                               ISongService _songService)
        {
            this.albumService = _albumService;
            this.artistService = _artistService;
            this.songService = _songService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> AlbumsIndexForAllUsers(string searchItem, List<string> filters, int pageNumber = 1)
        {
            int pageSize = 4;
            var albums = await albumService.FilterAlbumsAsync(searchItem, filters, pageNumber, pageSize);
            int totalPages = await albumService.GetTotalPagesAsync(pageSize);

            var viewModel = new AlbumsPaginationViewModel
            {
                Albums = albums.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
            };

            if (!albums.Any())
            {
                ViewData["NoResult"] = "No albums found matching your search.";
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<ActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));

            var viewModel = new AlbumCreateViewModel
            {
                Artists = new MultiSelectList(artists, "Id", "Username"),
                Songs = new MultiSelectList(songs, "Id", "Title"),
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Create(AlbumCreateViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                viewModel.Artists = new MultiSelectList(artists, "Id", "Username");

                var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));
                viewModel.Songs = new MultiSelectList(songs, "Id", "Title");

                return View(viewModel);
            }

            await albumService.AddAlbumAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var album = await albumService.GetAlbumByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));

            var viewModel = new AlbumEditViewModel
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                Artists = new MultiSelectList(artists, "Id", "Username"),
                Songs = new MultiSelectList(songs, "Id", "Title"),
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(AlbumEditViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                viewModel.Artists = new MultiSelectList(artists, "Id", "Username");
                
                var songs = await songService.SelectSinglesByCurrentLoggedArtistAsync(Guid.Parse(userId));
                viewModel.Songs = new MultiSelectList(songs, "Id", "Title");
                
                return View(viewModel);
            }

            await albumService.UpdateAlbumAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await albumService.RemoveAlbumAsync(id);
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var album = await albumService.GetAlbumByIdWithSongsAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AcceptAlbumUpload(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await albumService.AcceptAlbumUploadAsync(id);

            return RedirectToAction("ManageAlbumsStatus", "Admin");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> RejectAlbumUpload(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await albumService.AcceptAlbumUploadAsync(id);

            return RedirectToAction("ManageAlbumsStatus", "Admin");
        }
    }
}