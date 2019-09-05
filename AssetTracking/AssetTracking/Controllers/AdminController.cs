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
        public async Task<IActionResult> Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                email = email ?? User.FindFirst("preferred_username")?.Value;
                ViewData["Email"] = email;

                GraphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                ViewData["Response"] = await GraphService.GetUserJson(GraphClient, email, HttpContext);
            }
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult OfficeItems()
        {
            return View();
        }
        public IActionResult OfficeBooks()  
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error(string message, string debug)
        {
            return RedirectToAction("Index");
        }
    }
}