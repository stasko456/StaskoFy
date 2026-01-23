using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IServices;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Song;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly UserManager<User> userManager;

        public SongController(ISongService _songService, IGenreService _genreService, UserManager<User> _userManager)
        {
            this.songService = _songService;
            this.genreService = _genreService;
            this.userManager = _userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.GetSpecificArtistSongs(Guid.Parse(userId));
            return View(songs);
        }

        [HttpGet]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Create()
        {
            var genres = await genreService.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            var model = new SongCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Create(SongCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await genreService.GetAllAsync();

            if (ModelState.IsValid)
            {
                ViewBag.Genres = new SelectList(genres, "Id", "Name");
                return View(model);
            }

            await songService.AddAsync(model, Guid.Parse(userId));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Artist")]
        public async Task <IActionResult> Edit(Guid id)
        {
            var genres = await genreService.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var song = await songService.GetByIdAsync(id);

            var model = new SongEditViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Length = song.Length,
                ReleaseDate = song.ReleaseDate,
                GenreId = song.GenreId,
                ImageURL = song.ImageURL,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Edit(SongEditViewModel model)
        {
            var genres = await genreService.GetAllAsync();

            if (ModelState.IsValid)
            {
                ViewBag.Genres = new SelectList(genres, "Id", "Name");
                return View(model);
            }

            await songService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task <IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await songService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}