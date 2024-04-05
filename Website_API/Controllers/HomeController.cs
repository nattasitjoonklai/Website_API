using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Website_API.Data;
using Website_API.Models;

namespace Website_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        private readonly IConfiguration _configuration;
        
        public HomeController (ApplicationDBContext db )
        {
            _db = db; 
        }



       

        public IActionResult Index()
        {
         
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


        public ActionResult DisplayData()
        {
            // Retrieve data, status, and HTTP method type from TempData
            string responseData = TempData["ApiResponse"] as string;
            string apiStatus = TempData["ApiStatus"] as string;
            string httpMethod = TempData["HttpMethod"] as string;

            // Pass the data, status, and HTTP method type to the view
            ViewBag.ApiResponse = responseData;
            ViewBag.ApiStatus = apiStatus;
            ViewBag.HttpMethod = httpMethod;

            return View();
        }
    }

}