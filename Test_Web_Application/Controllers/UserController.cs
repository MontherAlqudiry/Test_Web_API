

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using Test_Web_Application.Models;

namespace Test_Web_Application.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new("https://localhost:7208/api/");
        private readonly HttpClient _Client;
        private readonly IWebHostEnvironment _HostEnvironment;
        private readonly ILogger<HomeController> _logger;
        public UserController(IWebHostEnvironment hc, ILogger<HomeController> logger)
        {
            _HostEnvironment = hc;
            _Client = new HttpClient();
            _Client.BaseAddress = baseAddress;
            _logger = logger;
        }


        public IActionResult Rigester()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Rigester(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new(data, Encoding.UTF8, "application/problem+json");
            HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "Users/RegisterUser", content).Result;


            //here you have to go to login page after rigesteration


            if (response.IsSuccessStatusCode)
            {
                var userjson = response.Content.ReadAsStringAsync().Result;
                var userobject = JsonConvert.DeserializeObject<User>(userjson);
                var userid = userobject.Id;


                TempData["SuccessRigestration"] = "Registration successful!";
                // Redirect to the login page
                return RedirectToAction("Login", "User");
            }
            // Handle registration failure
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userlog)
        {

            string data = JsonConvert.SerializeObject(userlog);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "Users/Login", content).Result;
            var userData = await response.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("UserObject", userData);

            var userObject = JsonConvert.DeserializeObject<User>(userData);
            HttpContext.Session.SetString("UserId", userObject.Id.ToString());
            HttpContext.Session.SetString("UserRole", userObject.Role.ToString());

            // string  serObj = JsonConvert.SerializeObject(userObject);





            // var userid = userObject.Id;
            if (response.IsSuccessStatusCode)
            {
                
                ViewBag.UserObject=userObject;
                // TempData["userId"] = userid;
                // ViewBag.UserId = userid ;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                return View();
            }
           
        }


        //to view the user info by calling the api controller
        public IActionResult Info(int Id)
        {
            User userInfo = new();
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "Users/Info/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                 userInfo =  JsonConvert.DeserializeObject<User>(data);
            }

          var userJson=  HttpContext.Session.GetString("UserObject");
            User userObject = JsonConvert.DeserializeObject<User>(userJson);
            


            return View(userInfo);
        }

        //to get user data from the form and call the api controller to update the user
       
        public async Task<IActionResult> InfoPut(User user)
        {
            var data = JsonConvert.SerializeObject(user); // Wrap the Id in a JSON object with a key
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response =  _Client.PutAsync(_Client.BaseAddress + $"User/UpdateUser/{user.Id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                 return RedirectToAction("Index", "Home");
    
            }

            return View();
           
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
