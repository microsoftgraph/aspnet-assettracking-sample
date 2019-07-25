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

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
       

    }
}