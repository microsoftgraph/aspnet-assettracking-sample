using System.Diagnostics;
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
   public class AdminController : Controller
   {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;
        public AdminController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper)
        {
            _configuration = configuration;
            _env = hostingEnvironment;
            _graphSdkHelper = graphSdkHelper;
        }
        public GraphServiceClient GraphClient { get; private set; }
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