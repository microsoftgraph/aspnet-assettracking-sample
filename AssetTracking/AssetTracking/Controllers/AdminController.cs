using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AssetTracking.Models;
using Microsoft.AspNetCore.Authorization;


namespace AssetTracking.Controllers
{ 
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index() { 
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