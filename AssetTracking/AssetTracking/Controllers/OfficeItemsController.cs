using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetTracking.Helpers;
using AssetTracking.Interfaces;
using AssetTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace AssetTracking.Controllers
{
    public class OfficeItemsController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        private GraphServiceClient _graphClient;
        private readonly IOfficeItemRepository _officeItemRepository;
        private readonly string siteId; 
        
        public OfficeItemsController(IGraphSdkHelper graphSdkHelper, IOfficeItemRepository officeItemRepository, IConfiguration configuration)
        {
            _graphSdkHelper = graphSdkHelper;
            _officeItemRepository = officeItemRepository;
            siteId = configuration["SiteId"];
        }

        public ActionResult OfficeItems()
        {
            return View();
        }

        public async Task<JsonResult> GetOfficeItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                List<OfficeItem> officeItemList = await _officeItemRepository.GetItems(_graphClient,siteId);
                return Json(new { data = officeItemList });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public async Task<JsonResult> GetItemsById(string Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                List<OfficeItem> officeItemList = await _officeItemRepository.GetItems(_graphClient, siteId);
                OfficeItem officeItem = officeItemList.Where(d => d.ItemId == Id).FirstOrDefault();
                return Json(officeItem);
            }
            return Json(null);
        }

        [HttpPost]
        public async Task<JsonResult> AddItem(OfficeItem officeItem)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                bool result = await _officeItemRepository.AddItem(officeItem, _graphClient, siteId);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public async Task<JsonResult> UpdateItem(OfficeItem officeItem)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                bool result = await _officeItemRepository.UpdateItem(officeItem, _graphClient, siteId);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public async Task<JsonResult> DeleteItem(OfficeItem officeItem)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                bool result = await _officeItemRepository.DeleteItem(officeItem, _graphClient, siteId);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }
    }
}