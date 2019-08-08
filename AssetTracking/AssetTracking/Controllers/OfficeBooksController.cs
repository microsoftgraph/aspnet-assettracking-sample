using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetTracking.Helpers;
using AssetTracking.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace AssetTracking.Controllers
{
    [Route("[controller]")]
    public class OfficeBooksController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;

        private readonly string siteId = "m365b267815.sharepoint.com,6e1261a1-6d03-432a-95c0-e1c7705aef5f,f43d258c-ece0-476a-a1c0-018d359817d5";
        private readonly string listId;
        private ISiteListsCollectionPage sharePointLists;
        private string itemId = null;
        private readonly string title;
        private readonly string ISBN;
        List<OfficeBook> _officeBooksDirectoryList = new List<OfficeBook>();

        public OfficeBooksController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper)
        {
            _configuration = configuration;
            _env = hostingEnvironment;
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
        private async Task<IActionResult> GetBooks()
        {
            sharePointLists = await Sites.GetLists(GraphClient, siteId);
            if (sharePointLists != null)
            {
                var _officeBookList = sharePointLists.Where(x => x.DisplayName.Contains("Office Books")).FirstOrDefault();
                var _listId = _officeBookList.Id;
                var _officeBookItems = await GetListItems(GraphClient, siteId, _listId);

                List<OfficeBook> _officeBooksDirectoryList = new List<OfficeBook>();

                foreach (var item in _officeBookItems)
                {
                    var resourceList = item.Fields.AdditionalData;
                    var jsonString = JsonConvert.SerializeObject(resourceList);

                    var officeResource = JsonConvert.DeserializeObject<OfficeBook>(jsonString);
                    officeResource.SharePointItemId = item.Id;
                    _officeBooksDirectoryList.Add(officeResource);
                }

                ViewBag.List = _officeBooksDirectoryList;
            }
            return View("~/Views/Shared/Error.cshtml");
        }
        private async static Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphClient, string siteId, string listId)
        {
            IListItemsCollectionPage listItems = await Sites.GetListItems(graphClient, siteId, listId);
            return listItems;
        }

        [Route("/AddBooks")]
        [HttpPost]
        [AutoValidateAntiforgeryTokenAttribute]
        public async Task<IActionResult> AddBooks(OfficeBook officeBook)
        {
            GraphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);

            sharePointLists = await Sites.GetLists(GraphClient, siteId);

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Books")).FirstOrDefault();
                string _listId = addbook.Id;
                officeBook.BookID = Guid.NewGuid();

                IDictionary<string, object> data = new Dictionary<string, object>
                {

                    { "BookID", officeBook.BookID },
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.BookDescription }
                };

                bool result = await Sites.AddListItem(GraphClient, siteId,
                                                      _listId,
                                                      data);
                if (result)
                {
                    return RedirectToAction("GetBooks");
                }
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPut]
        public RedirectToActionResult Update(OfficeBook officeBook)
        {
            GraphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);

            sharePointLists = Sites.GetLists(GraphClient, siteId).GetAwaiter().GetResult();

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Books")).FirstOrDefault();
                string _listId = addbook.Id;

                IDictionary<string, object> data = new Dictionary<string, object>
                {

                    { "BookID", officeBook.BookID },
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.BookDescription }
                };

                bool result = Sites.UpdateListItem(GraphClient, siteId,
                                                      _listId, itemId,
                                                      data).GetAwaiter().GetResult();
                if (result)
                {
                    return RedirectToAction("GetBooks");
                }
            }
            return RedirectToAction("OfficeBooks");
        }

        [Route("/UpdateBookDetails")]
        [HttpPost]
        [AutoValidateAntiforgeryTokenAttribute]
        public async Task<IActionResult> UpdateBookDetails([FromForm]OfficeBook officeBook)
        {
            GraphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);

            sharePointLists = await Sites.GetLists(GraphClient, siteId);
            string userItemId = officeBook.SharePointItemId;

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Office Books")).FirstOrDefault();
                string _listId = addbook.Id;

                string itemId = userItemId;

                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.BookDescription }
                };

                bool result = await Sites.UpdateListItem(GraphClient, siteId,
                                                      _listId, itemId,
                                                      data);
                if (result)
                {
                    return RedirectToAction("GetBooks");
                }
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        [Route("/DeleteBook")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook(OfficeBook officeBook)
        {
            GraphClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);

            sharePointLists = await Sites.GetLists(GraphClient, siteId);
            string userItemId = officeBook.SharePointItemId;

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Office Books")).FirstOrDefault();
                string _listId = addbook.Id;

                string itemId = userItemId;

                bool result = await Sites.DeleteListItem(GraphClient, siteId,
                                                      _listId, itemId);
                if (result)
                {
                    return RedirectToAction("GetBooks");
                }
            }
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}