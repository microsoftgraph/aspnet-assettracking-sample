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
    public class OfficeItemsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;
        private readonly string siteId = "m365b267815.sharepoint.com,6e1261a1-6d03-432a-95c0-e1c7705aef5f,f43d258c-ece0-476a-a1c0-018d359817d5";
        private readonly string listId;
        private ISiteListsCollectionPage sharePointLists;
        private readonly string itemId = null;
        List<OfficeItems> _officeItemDirectoryList = new List<OfficeItems>();

        public OfficeItemsController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper)
        {
            _configuration = configuration;
            _env = hostingEnvironment;
            _graphSdkHelper = graphSdkHelper;
        }
        public GraphServiceClient graphserviceClient { get; private set; }
        [HttpGet]
        public async Task<IActionResult> OfficeItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                graphserviceClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);
                var user = graphserviceClient.Me.Request().GetAsync();

                sharePointLists = await Sites.GetLists(graphserviceClient, siteId);
                if (sharePointLists != null)
                {
                    var _officeItemList = sharePointLists.Where(x => x.DisplayName.Contains("Items")).FirstOrDefault();
                    var _listId = _officeItemList.Id;
                    var _officeItems = await Sites.GetListItems(graphserviceClient, siteId, _listId);


                    foreach (var item in _officeItems)
                    {
                        var resourceList = item.Fields.AdditionalData;
                        var jsonString = JsonConvert.SerializeObject(resourceList);

                        var officeResource = JsonConvert.DeserializeObject<OfficeItems>(jsonString);
                        _officeItemDirectoryList.Add(officeResource);
                    }
                }
                ViewBag.List = _officeItemDirectoryList;
            }
            return View("~/Views/Admin/OfficeItems.cshtml");
        }

        [HttpPost]
        [AutoValidateAntiforgeryTokenAttribute]
        public async Task<IActionResult> AddItem(OfficeItems officeItem)
        {
            graphserviceClient = _graphSdkHelper.GetAuthenticatedClient((ClaimsIdentity)User.Identity);

            sharePointLists = await Sites.GetLists(graphserviceClient, siteId);

            if (sharePointLists != null)
            {
                var additem = sharePointLists.Where(b => b.DisplayName.Contains("Items")).FirstOrDefault();
                string _listId = additem.Id;
                officeItem.ItemId = Guid.NewGuid();
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "BookID", officeItem.ItemId },
                    { "Resource_x0020_IDLookupId", "1" },
                    { "Title", officeItem.Title },
                    { "ISBN", officeItem.ResourceId },
                    { "Author0", officeItem.SerialNumber },
                    { "BookTitle", officeItem.ItemDescription }
                };

                bool result = await Sites.AddListItem(graphserviceClient, siteId,
                                                      _listId,
                                                      data);
                if (result)
                {
                    return await OfficeItems();
                }
            }
            return View("~/Views/Shared/Error.cshtml");
        }
       
        
    }


}