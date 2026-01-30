using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Models;

namespace StaskoFy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService songService;

        public HomeController(ILogger<HomeController> logger, ISongService _songService)
        {
            _logger = logger;
            this.songService = _songService;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await songService.GetAllAsync();
            return View(songs);
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
