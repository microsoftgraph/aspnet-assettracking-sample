using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssetTracking.Models;
using Microsoft.AspNetCore.Authorization;
using AssetTracking.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public GraphServiceClient GraphClient { get; private set; }

        [AllowAnonymous]
        public IActionResult Index(string email)
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error(string message, string debug)
        {
            return RedirectToAction("Index");
        }
    }
}