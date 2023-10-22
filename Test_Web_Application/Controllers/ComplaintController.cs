using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Test_Web_Application.Models;

namespace Test_Web_Application.Controllers
{
    public class ComplaintController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7208/api");
        private readonly HttpClient _Client;
        public ComplaintController()
        {
            _Client = new HttpClient();
            _Client.BaseAddress = baseAddress;

        }
        [HttpGet]
        public  IActionResult Index()
        {
            IList<ComplaintsApp> CompList = new List<ComplaintsApp>();
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/ComplaintsApps/GetComplaintsApp").Result;

            if(response.IsSuccessStatusCode) { 
            
                string data =response.Content.ReadAsStringAsync().Result;
                CompList = JsonConvert.DeserializeObject<List<ComplaintsApp>>(data);  

            }

            return View(CompList);
        }

       
        public IActionResult Create() {
        

            return View();
        }

        [HttpPost]
        public IActionResult Create(ComplaintsApp obj)
        {
            try
            {
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "/ComplaintsApps/PostComplaintsApp", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Complain Created successfuly!";
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

        [HttpGet]
        public IActionResult Edit(int Id) {
            try
            {
                ComplaintsApp comp = new();
                HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/ComplaintsApps/GetComplaintsApp/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    comp = JsonConvert.DeserializeObject<ComplaintsApp>(data);
                }
                TempData["Success"] = "Complaint Updated successfully!";
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
