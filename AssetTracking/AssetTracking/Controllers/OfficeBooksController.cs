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
    public class OfficeBooksController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        private IOfficeBookRepository _officeBookRepository;
        private GraphServiceClient _graphClient;

        public  OfficeBooksController( IGraphSdkHelper graphSdkHelper, IOfficeBookRepository officeBookRepository) 
        { 
            _graphSdkHelper = graphSdkHelper;
            _officeBookRepository = officeBookRepository;
        }
            
        [HttpGet]
        public async Task<IActionResult> OfficeBooks()
        {
            if (User.Identity.IsAuthenticated)
            {
                _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                List<OfficeBook> officeBook = await _officeBookRepository.GetBooks(_graphClient);
                ViewBag.List = officeBook;
                return View(officeBook);
            }
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddBook(OfficeBook officeBook)
        {
            _graphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
            bool result = await _officeBookRepository.AddBook(officeBook,_graphClient);
            return RedirectToAction("~/Views/OfficeBooks/OfficeBooks.cshtml");
        }

        [HttpPut]
        public async Task<bool> UpdateBook(OfficeBook officeBook)
        {
            bool result = await _officeBookRepository.UpdateBook(officeBook, _graphClient);
            return result;
        }
        [Route("/DeleteBook")]
        [HttpPost]
        public async Task<bool> DeleteBook(OfficeBook officeBook)
        {
            bool result = await _officeBookRepository.DeleteBook(officeBook, _graphClient);
            return result;
        }
    }
}