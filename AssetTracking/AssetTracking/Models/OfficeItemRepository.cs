using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace AssetTracking.Models
{
    public class OfficeItemRepository : IOfficeItemRepository
    {
        private string SiteId { get; set; }
        private ISiteListsCollectionPage officeItemsLists;
        private const string OfficeItemsDisplayName = "OfficeItems";
        //Gets Office Items
        public async Task<List<OfficeItem>> GetItems(GraphServiceClient graphClient)
        {
            officeItemsLists = await Sites.GetLists(graphClient, SiteId);
            List<OfficeItem> _officeItemDirectoryList = new List<OfficeItem>();
            if (officeItemsLists != null)
            {
                var _officeItemList = officeItemsLists.Where(x => x.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                var listId = _officeItemList.Id;
                var _officeItems = await Sites.GetListItems(graphClient, SiteId, listId);

                foreach (var item in _officeItems)
                {
                    var resourceList = item.Fields.AdditionalData;
                    var jsonString = JsonConvert.SerializeObject(resourceList);

                    var officeResource = JsonConvert.DeserializeObject<OfficeItem>(jsonString);
                    officeResource.ItemId = item.Id;
                    _officeItemDirectoryList.Add(officeResource);
                }
            }
            return _officeItemDirectoryList;
        }
        //Adds Office Items
        public async Task<bool> AddItem(OfficeItem officeItem, GraphServiceClient graphClient)
        {
            officeItemsLists = await Sites.GetLists(graphClient, SiteId);
            if (officeItemsLists != null)
            {
                var additem = officeItemsLists.Where(b => b.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string listId = additem.Id;
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    {"OfficeItemID", officeItem.ItemId},
                    {"Title", officeItem.Title },
                    {"Resource_x0020_IDLookupId", officeItem.ResourceId },
                    {"SerialNo", officeItem.SerialNo },
                    {"Description", officeItem.ItemDescription }
                };
                bool addofficeitem = await Sites.AddListItem(graphClient, SiteId,
                                                      listId,
                                                      data);
                return addofficeitem;
            }
            else
            {
                return false;
            }
        }
        //Updates Office Items
        public async Task<bool> UpdateItem(OfficeItem officeItem, GraphServiceClient graphClient)
        {
            officeItemsLists = await Sites.GetLists(graphClient, SiteId);
            string userItemId = officeItem.ItemId;
            if (officeItemsLists != null)
            {
                var addItem = officeItemsLists.Where(b => b.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string _listId = addItem.Id;

                string itemId = userItemId;

                IDictionary<string, object> data = new Dictionary<string, object>
                {

                    {"OfficeItemID", officeItem.ItemId},
                    {"Title", officeItem.Title },
                    {"Resource_x0020_IDLookupId", officeItem.ResourceId },
                    {"SerialNo", officeItem.SerialNo },
                    {"Description", officeItem.ItemDescription }
                };
                bool updatebook = await Sites.UpdateListItem(graphClient, SiteId,
                                                      _listId, itemId,
                                                      data);
                return updatebook;
            }
            else
            {
                return false;
            }
        }
        //Deletes Office Items
        public async Task<bool> DeleteItem(OfficeItem officeItem, GraphServiceClient graphClient)
        {
            officeItemsLists = await Sites.GetLists(graphClient, SiteId);
            string userItemId = officeItem.ItemId;
            if (officeItemsLists != null)
            {
                var addItem = officeItemsLists.Where(b => b.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string _listId = addItem.Id;
                string itemId = userItemId;

                bool deletebook = await Sites.DeleteListItem(graphClient, SiteId,
                                                      _listId, itemId);
                return deletebook;
            }
            else
            {
                return false;
            }
        }
    }


}