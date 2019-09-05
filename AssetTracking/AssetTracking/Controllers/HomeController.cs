using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AssetTracking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public GraphServiceClient GraphClient { get; private set; }

        [Route("")]
        public IActionResult MyBooks()
        {
            return View("~/Views/User/MyBooks.cshtml");
        }
        
        [AllowAnonymous]
        [Route("admin")]
        public IActionResult Index()
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