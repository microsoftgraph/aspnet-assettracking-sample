using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetTracking.Helpers;
using AssetTracking.Models;
using AssetTracking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace AssetTracking.Controllers
{
    public class OfficeItemsController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        private GraphServiceClient _graphClient;
        public IOfficeItemRepository _officeItemRepository;
        public string _siteId;
        public OfficeItemsController(IGraphSdkHelper graphSdkHelper, IOfficeItemRepository officeItemRepository, IConfiguration configuration)
        {
            _graphSdkHelper = graphSdkHelper;
            _officeItemRepository = officeItemRepository;
            _siteId = configuration["SiteId"];
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
                List<OfficeItem> officeItemList = await _officeItemRepository.GetItems(_graphClient);
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
                List<OfficeItem> officeItemList = await _officeItemRepository.GetItems(_graphClient);
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
                bool result = await _officeItemRepository.AddItem(officeItem, _graphClient);
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
                bool result = await _officeItemRepository.UpdateItem(officeItem, _graphClient);
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
                bool result = await _officeItemRepository.DeleteItem(officeItem, _graphClient);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }
    }
}