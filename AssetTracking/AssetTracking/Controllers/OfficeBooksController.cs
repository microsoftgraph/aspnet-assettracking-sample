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
    public class OfficeBooksController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;
        private readonly string siteId = "m365b267815.sharepoint.com,6e1261a1-6d03-432a-95c0-e1c7705aef5f,f43d258c-ece0-476a-a1c0-018d359817d5";
        private readonly string listId;
        private ISiteListsCollectionPage sharePointLists;
        private readonly string itemId = null;
        private readonly string title;
        private readonly string ISBN;
        List<OfficeBooks> _officeBooksDirectoryList = new List<OfficeBooks>();

        public OfficeBooksController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper)
        {
            _configuration = configuration;
            _env = hostingEnvironment;
            _graphSdkHelper = graphSdkHelper;
        }
        public GraphServiceClient graphserviceClient { get; private set; }
        [HttpGet]
        public async Task<IActionResult> OfficeBooks()
        {
            if (User.Identity.IsAuthenticated)
            {
                graphserviceClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                var user = graphserviceClient.Me.Request().GetAsync();

                sharePointLists = await Sites.GetLists(graphserviceClient, siteId);
                if (sharePointLists != null)
                {
                    var _officeBookList = sharePointLists.Where(x => x.DisplayName.Contains("Books")).FirstOrDefault();
                    var _listId = _officeBookList.Id;
                    var _officeBookItems = await Sites.GetListItems(graphserviceClient, siteId, _listId);


                    foreach (var item in _officeBookItems)
                    {
                        var resourceList = item.Fields.AdditionalData;
                        var jsonString = JsonConvert.SerializeObject(resourceList);

                        var officeResource = JsonConvert.DeserializeObject<OfficeBooks>(jsonString);
                        _officeBooksDirectoryList.Add(officeResource);
                    }
                }
                    ViewBag.List = _officeBooksDirectoryList;
            }
            return View("~/Views/Admin/OfficeBooks.cshtml");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddBook(OfficeBooks officeBook)
        {
            graphserviceClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);

            sharePointLists = await Sites.GetLists(graphserviceClient, siteId);

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Books")).FirstOrDefault();
                string _listId = addbook.Id;
                officeBook.BookId = Guid.NewGuid();
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "BookID", officeBook.BookId },
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.BookDescription }
                };

                bool result = await Sites.AddListItem(graphserviceClient, siteId,
                                                      _listId,
                                                      data);
                if (result)
                {
                    return await OfficeBooks();
                }
            }
            return View("~/Views/Shared/Error.cshtml");
        }
        private async Task UpdateBook(string ItemId)
        {
            IDictionary<string, object> data = new Dictionary<string, object>();

            string listItemId = itemId;

            var officeBookItem = _officeBooksDirectoryList.Where(b => b.SharePointItemId.Contains(itemId)).FirstOrDefault();
            officeBookItem.Title = title;

            var jsonString = JsonConvert.SerializeObject(officeBookItem);
            data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            bool result = await Sites.UpdateListItem(graphserviceClient,siteId,listId, ItemId, data);

            if (result)
            {
                
                //GetBooks();
            }
        }

    }

    
}