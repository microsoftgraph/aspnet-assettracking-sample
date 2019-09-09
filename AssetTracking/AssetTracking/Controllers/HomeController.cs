using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AssetTracking.Models;
using Microsoft.Graph;
using AssetTracking.Helpers;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        public HomeController( IGraphSdkHelper graphSdkHelper)
        {
            _graphSdkHelper = graphSdkHelper;
        }
        public GraphServiceClient GraphClient { get; private set; }
        public async Task<IActionResult> Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                email = email ?? User.FindFirst("preferred_username")?.Value;
                ViewData["Email"] = email;

        public IActionResult MyBooks()
        {
            return View("~/Views/User/MyBooks.cshtml");
        }
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