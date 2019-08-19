using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetTracking.Helpers;
using AssetTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace AssetTracking.Controllers
{
    public class OfficeBooksController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        private IOfficeBookRepository _officeBookRepository;
        private GraphServiceClient _graphClient;

        public OfficeBooksController(IGraphSdkHelper graphSdkHelper, IOfficeBookRepository officeBookRepository)
        {
            _graphSdkHelper = graphSdkHelper;
            _officeBookRepository = officeBookRepository;
        }

        public ActionResult OfficeBooks()
        {
            
            return View();
        }

        public async Task<JsonResult> OfficeBooksGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                List<OfficeBook> officeBook = await _officeBookRepository.GetBooks(_graphClient);

                return Json(new { data = officeBook });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public async Task<ActionResult> OfficeBooksGetbyId(string Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                List<OfficeBook> officeBookList = await _officeBookRepository.GetBooks(_graphClient);
                OfficeBook officeBook = officeBookList.Where(d => d.ItemId == Id).FirstOrDefault();
                return Json(officeBook);
            }
            return Json(null);
        }

        [HttpPost]
        public async Task<JsonResult> AddBook(OfficeBook officeBook)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                bool result = await _officeBookRepository.AddBook(officeBook, _graphClient);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public async Task<JsonResult> UpdateBook(OfficeBook officeBook)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                bool result = await _officeBookRepository.UpdateBook(officeBook, _graphClient);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public async Task<JsonResult> DeleteBook(OfficeBook officeBook)
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                bool result = await _officeBookRepository.DeleteBook(officeBook, _graphClient);
                return Json(new { IsSuccess = result });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }
    }
}