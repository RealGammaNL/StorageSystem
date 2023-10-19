using Domain.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Models;
using System.Diagnostics;

namespace StorageAppMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        public NavbarViewModel NavbarViewModel { get; set; }
        private readonly StorageDb _context;

        public HomeController(StorageDb context)
        {
            _context = context;
        }

        //public HomeController()
        //{
        //    this.NavbarViewModel = new NavbarViewModel();//has property PageTitle
        //    NavbarViewModel.Rooms = _context.Rooms.ToList();

        //    this.ViewData["NavbarViewModel"] = this.NavbarViewModel;
        //}
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //[AllowAnonymous] // uncomment to disable login
        public IActionResult Index()
        {
            this.NavbarViewModel = new NavbarViewModel();//has property PageTitle
            NavbarViewModel.Rooms = _context.Rooms.ToList();
            this.ViewData["NavbarViewModel"] = this.NavbarViewModel;
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