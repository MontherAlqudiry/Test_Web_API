using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test_Web_Application.Models;

namespace Test_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _HostEnvironment;
        

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hc)
        {
            _logger = logger;
            _HostEnvironment = hc;
           
        }

        public IActionResult Index()
        {
            string wwwRootPath = _HostEnvironment.WebRootPath;

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