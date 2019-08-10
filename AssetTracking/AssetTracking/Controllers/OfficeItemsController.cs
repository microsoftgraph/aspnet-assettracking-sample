using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetTracking.Helpers;
using AssetTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace AssetTracking.Controllers
{
    [Route("[controller]")]
    public class OfficeItemsController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        public IOfficeItemRepository _officeItemRepository;

        public OfficeItemsController( IGraphSdkHelper graphSdkHelper)
        {
            _graphSdkHelper = graphSdkHelper;
        }
        public GraphServiceClient graphserviceClient { get; private set; }
        [HttpGet]
        public IActionResult OfficeItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                graphserviceClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                var user = graphserviceClient.Me.Request().GetAsync();
            }
            return View();
        }
       
        public async Task<IActionResult> GetItems()
        {
            List<OfficeItem> officeItem = await _officeItemRepository.GetItems();
            ViewBag.List = _officeItemRepository;
            return View();
        }

        [Route("/AddItems")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<bool> AddItem(OfficeItem officeItem)
        {
            bool result = await _officeItemRepository.AddItem(officeItem);
            return result;
        }

        [Route("/UpdateItems")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<bool> UpdateItem(OfficeItem officeItem)
        {
            bool result = await _officeItemRepository.UpdateItem(officeItem);
            return result;
        }
        [Route("/DeleteItems")]
        [HttpPost]
        public async Task<bool> DeleteItem(OfficeItem officeItem)
        {
            bool result = await _officeItemRepository.DeleteItem(officeItem);
            return result;
        }
    }
}