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
        public IActionResult Index()
        {
            IList<ComplaintsApp> CompList = new List<ComplaintsApp>();

            // Get the user ID from the session
            string userjson = HttpContext.Session.GetString("UserObject");
            var userobj = JsonConvert.DeserializeObject<User>(userjson);
            int userId = userobj.Id;

            //HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/ComplaintsApps/GetComplaintsApp").Result;
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + $"ComplaintsApps/GetComplaintsApp?userId={userId}").Result;


            if (response.IsSuccessStatusCode) {

                string data = response.Content.ReadAsStringAsync().Result;
                CompList = JsonConvert.DeserializeObject<List<ComplaintsApp>>(data);

            }

            return View(CompList);
        }



        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            IList<ComplaintsApp> CompList = new List<ComplaintsApp>();



            //HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/ComplaintsApps/GetComplaintsApp").Result;
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "ComplaintsApps/GetAllComplaintsApp").Result;

            

                if (response.IsSuccessStatusCode)
                {   
                    if (response == null)
                    {

                    return View();
                    }

                    string data = response.Content.ReadAsStringAsync().Result;
                    CompList = JsonConvert.DeserializeObject<List<ComplaintsApp>>(data);


                }

            return View(CompList);
        }


        public IActionResult Create()

        {
            var userObjectJ = HttpContext.Session.GetString("UserObject");
            var userObject = JsonConvert.DeserializeObject<User>(userObjectJ);
            
            
            ViewBag.UserId = userObject.Id;
            ViewBag.UserObject = userObject;
            




            return View();



        }

        [HttpPost]
        public async Task<IActionResult> Create(ComplaintsApp obj)
        {
            try
            {
                //to save the upload file in wwrootpath
                string wwwRootPath = _HostEnvironment.WebRootPath;

                if (obj.uploadFile == null)
                {
                    obj.uploadFile = new FormFile(Stream.Null, 0, 0, "", "");
                }
               
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
                        Id = obj.Id,
                        Name = obj.Name,
                        Content = obj.Content,
                        Type = obj.Type,
                        File = obj.File,
                        uploadFile = obj.uploadFile,
                        Status = obj.Status,
                        UserGmail = userobj.Email,
                        UserId = obj.UserId,
                        User = obj.User,
                        demandOneText = obj.demandOneText,
                        demandTwoText = obj.demandTwoText


                    };
                    newobj.File = fileName;
                    newobj.User = userobj;

               
                string ObjJson = JsonConvert.SerializeObject(newobj);

                // Send the JSON content to the Web API controller.
                StringContent content = new StringContent(ObjJson, Encoding.UTF8, "application/problem+json");
                HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/PostComplaint", content).Result;
                string responseContent = await response.Content.ReadAsStringAsync();

                ComplaintsApp compresp = JsonConvert.DeserializeObject<ComplaintsApp>(responseContent);



                if (compresp.demandOneText != null)
                {
                    //create demand_one 
                    demandOne dOne = new();
                    dOne.demandOneText = compresp.demandOneText;
                    dOne.ComplaintId = compresp.Id;
                    dOne.ComplaintsApp = compresp;
                    dOne.UserId = compresp.UserId;
                    string dOneJson = JsonConvert.SerializeObject(dOne);
                    // Send the JSON content to the Web API controller.
                    StringContent D1content = new StringContent(dOneJson, Encoding.UTF8, "application/problem+json");
                    HttpResponseMessage D1response = _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/CreateDemandOne", D1content).Result;
                    string D1responseContent = await D1response.Content.ReadAsStringAsync();
                }

                if (compresp.demandTwoText != null)
                {
                    //create demand_Two 
                    demandTwo dTwo = new();
                    dTwo.demandTwoText = compresp.demandTwoText;
                    dTwo.ComplaintId = compresp.Id;
                    dTwo.ComplaintsApp = compresp;
                    dTwo.UserId = compresp.UserId;
                    string dTwoJson = JsonConvert.SerializeObject(dTwo);
                    // Send the JSON content to the Web API controller.
                    StringContent D2content = new StringContent(dTwoJson, Encoding.UTF8, "application/problem+json");
                    HttpResponseMessage D2response = _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/CreateDemandTwo", D2content).Result;
                    string D2responseContent = await D2response.Content.ReadAsStringAsync();
                }



                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessComp"] = "Complain Created successfully!";
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
                HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "ComplaintsApps/GetComplaintById/" + Id).Result;
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
                HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "ComplaintsApps/GetComplaintById/" + Id).Result;
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

        
        public async Task<IActionResult> EditStatusApprove(int Id)
        {
            try
            {

                var data = new { id = Id }; // Wrap the Id in a JSON object with a key
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _Client.PostAsync(_Client.BaseAddress + $"ComplaintsApps/ChangeComplaintsAppStutusApprove/{Id}", content);


                //string data = JsonConvert.SerializeObject(Id.ToString());
                //Console.WriteLine("Data: " + data);
                //StringContent content = new(data, Encoding.UTF8, "application/problem+json");
                //HttpResponseMessage response =  _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/ChangeComplaintsAppStutusApprove/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("GetAllComplaints", "Complaint");
                    

                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();

            }

        }

       
        public async Task< IActionResult> EditStatusDeny(int Id)
        {
            try
            {

                var data = new { id = Id }; // Wrap the Id in a JSON object with a key
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _Client.PostAsync(_Client.BaseAddress + $"ComplaintsApps/ChangeComplaintsAppStutusDeny/{Id}", content);


                //string data = JsonConvert.SerializeObject(Id.ToString());
                //Console.WriteLine("Data: " + data);
                //StringContent content = new(data, Encoding.UTF8, "application/problem+json");
                //HttpResponseMessage response =  _Client.PostAsync(_Client.BaseAddress + "ComplaintsApps/ChangeComplaintsAppStutusApprove/", content).Result;
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetAllComplaints", "Complaint");


                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();

            }

        }


        



    }
}
