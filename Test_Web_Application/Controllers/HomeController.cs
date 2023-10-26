using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

           //var userObjectJ = HttpContext.Session.GetString("UserObject");
           // if (!string.IsNullOrEmpty(userObjectJ))
           // {
               
           //     // Proceed with using the deserialized user object.
           //     var userObject = JsonConvert.DeserializeObject<User>(userObjectJ);
           //     ViewBag.UserId = userObject.Id;
           //     ViewBag.UserObject = userObject;
           // }
           // else
           // {
           //     return RedirectToAction("Index");
           //     // Handle the case where userObjectJ is null or empty.
           //     // You might log an error, throw an exception, or take appropriate action.
           // }
           
           
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