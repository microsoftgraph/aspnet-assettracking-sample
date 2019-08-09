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
        public IOfficeBookRepository _officeBookRepository;

        public OfficeBooksController(IGraphSdkHelper graphSdkHelper)
        {
            _graphSdkHelper = graphSdkHelper;
        }
        public GraphServiceClient GraphClient { get; private set; }

        [Route("")]
        public IActionResult OfficeBooks()
        {
            if (User.Identity.IsAuthenticated)
            {
                GraphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                var user = GraphClient.Me.Request().GetAsync();
            }
            return View("~/Views/Admin/OfficeBooks.cshtml");
        }

        [Route("/GetBooks")]
        [HttpPost]
        public async Task<IActionResult> GetBooks()
        {
            List<OfficeBook> officeBook = await _officeBookRepository.GetBooks();
            ViewBag.List = officeBook;
            return View();
        }

        [Route("/AddBooks")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<bool> AddBook(OfficeBook officeBook)
        {
            bool result = await _officeBookRepository.AddBook(officeBook);
            return result;
        }

        [HttpPut]
        public async Task<bool> UpdateBook(OfficeBook officeBook)
        {
            bool result = await _officeBookRepository.UpdateBook(officeBook);
            return result;
        }
        [Route("/DeleteBook")]
        [HttpPost]
        public async Task<bool> DeleteBook(OfficeBook officeBook)
        {
            bool result = await _officeBookRepository.DeleteBook(officeBook);
            return result;
        }
    }
}