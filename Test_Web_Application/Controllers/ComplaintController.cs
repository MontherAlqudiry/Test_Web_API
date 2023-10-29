using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Test_Web_Application.Models;

namespace Test_Web_Application.Controllers
{
    public class ComplaintController : Controller
    {

        Uri baseAddress = new("https://localhost:7208/api/");
        private readonly HttpClient _Client;
        private readonly IWebHostEnvironment _HostEnvironment;
        private readonly ILogger<HomeController> _logger;
        public ComplaintController(IWebHostEnvironment hc, ILogger<HomeController> logger)
        {
            _HostEnvironment = hc;
            _Client = new HttpClient();
            _Client.BaseAddress = baseAddress;
            _logger = logger;
        }
        [HttpGet]
        public  IActionResult Index()
        {
            IList<ComplaintsApp> CompList = new List<ComplaintsApp>();

            // Get the user ID from the session
            string userjson = HttpContext.Session.GetString("UserObject");
            var userobj =JsonConvert.DeserializeObject<User>(userjson);
            int userId = userobj.Id;

            //HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/ComplaintsApps/GetComplaintsApp").Result;
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + $"ComplaintsApps/GetComplaintsApp?userId={userId}").Result;


            if (response.IsSuccessStatusCode) { 
            
                string data =response.Content.ReadAsStringAsync().Result;
                CompList = JsonConvert.DeserializeObject<List<ComplaintsApp>>(data);  

            }

            return View(CompList);
        }
        [HttpGet]
        public IActionResult GetComplaints()
        {
            IList<ComplaintsApp> CompList = new List<ComplaintsApp>();

           

            //HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/ComplaintsApps/GetComplaintsApp").Result;
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "ComplaintsApps/GetAllComplaintsApp").Result;


            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;
                CompList = JsonConvert.DeserializeObject<List<ComplaintsApp>>(data);

            }

            return View(CompList);
        }


        public IActionResult Create()
     
        {
                 var userObjectJ = HttpContext.Session.GetString("UserObject");
                 var userObject = JsonConvert.DeserializeObject<User>(userObjectJ);
                 ViewBag.UserObject = userObject;
                 ViewBag.UserId = userObject.Id;

           
           
            
           
            return View();


      
        }

        [HttpPost]
        public async  Task< IActionResult> Create( ComplaintsApp obj )
            {
            try
            { 
                //to save the upload file in wwrootpath
                string wwwRootPath = _HostEnvironment.WebRootPath;


                // var  fileName = obj.uploadFile.FileName;
                var fileName = obj.uploadFile.FileName;
                obj.File = fileName;

                string path = Path.Combine(wwwRootPath + "/UserFile/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                   await obj.uploadFile.CopyToAsync(fileStream);
                }
                string userjson = HttpContext.Session.GetString("UserObject");
                var userobj = JsonConvert.DeserializeObject<User>(userjson);
                int userId = userobj.Id;
                userobj.ConfirmPassword = userobj.Password;


                ComplaintsApp newobj = new()
                {
                   // Id = obj.Id,
                   // Name = obj.Name,
                   // Content = obj.Content,
                   // Type = obj.Type,
                   //////// File = fileName,
                   // UserId = userId,
                   // Status = obj.Status,
                   // uploadFile=obj.uploadFile,
                   // User=obj.User,


                    Id = obj.Id,
                    Name = obj.Name,
                    Content = obj.Content,
                    Type = obj.Type,
                    File = obj.File,
                    uploadFile = obj.uploadFile,
                    Status = obj.Status,
                    UserId = obj.UserId,
                    User = obj.User
                };
                newobj.File = fileName;
                newobj.User = userobj;


                //string data = JsonConvert.SerializeObject(newobj);
                //StringContent content = new(data, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/PostComplaint", content).Result;

                string json = JsonConvert.SerializeObject(newobj);

                // Send the JSON content to the Web API controller.
                StringContent content = new StringContent(json, Encoding.UTF8, "application/problem+json");
                HttpResponseMessage response =  _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/PostComplaint", content).Result;
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessComp"] = "Complain Created successfuly!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }

                return View();
        }

        public IActionResult Review(int Id) 
        {
            try
            {
                ComplaintsApp comp = new();
                HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "ComplaintsApps/GetComplaintsApp/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    comp = JsonConvert.DeserializeObject<ComplaintsApp>(data);
                }

                return View(comp);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();

            }

        }
        

            [HttpGet]
        public IActionResult Edit(int Id) {
            try
            {
                ComplaintsApp comp = new();
                HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "ComplaintsApps/GetComplaintsApp/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    comp = JsonConvert.DeserializeObject<ComplaintsApp>(data);
                }
              
                return View(comp);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
               
            }
        }


    }
}
