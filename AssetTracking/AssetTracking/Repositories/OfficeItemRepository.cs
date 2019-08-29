using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetTracking.Interfaces;
using AssetTracking.Models;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace AssetTracking.Repositories
{
    public class OfficeItemRepository : IOfficeItemRepository
    {
        private const string OfficeItemsDisplayName = "OfficeItems";
        private ISiteListsCollectionPage _sharePointLists;
        private readonly Sites _sites;
        private readonly string _siteId;
        public OfficeItemRepository()
        {
            _sites = new Sites();
            _sharePointLists = new SiteListsCollectionPage();
        }
        public async Task<List<OfficeItem>> GetItems(GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);
            List<OfficeItem> officeItemDirectoryList = new List<OfficeItem>();
            if (_sharePointLists != null)
            {
                List officeItemList = _sharePointLists.Where(x => x.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string listId = officeItemList.Id;
                IListItemsCollectionPage _officeItems = await _sites.GetListItems(graphClient, _siteId, listId);
                foreach (ListItem item in _officeItems)
                {
                    IDictionary<string, object> resourceList = item.Fields.AdditionalData;
                    string jsonString = JsonConvert.SerializeObject(resourceList);

                    OfficeItem officeResource = JsonConvert.DeserializeObject<OfficeItem>(jsonString);
                    officeResource.ItemId = item.Id;
                    officeItemDirectoryList.Add(officeResource);
                }
            }
            return officeItemDirectoryList;
        }
        public async Task<bool> AddItem(OfficeItem officeItem, GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);
            if (_sharePointLists != null)
            {
                List additem = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string listId = additem.Id;
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    {"OfficeItemID", officeItem.ItemId},
                    {"Title", officeItem.Title },
                    {"Resource_x0020_IDLookupId", officeItem.ResourceId },
                    {"SerialNo", officeItem.SerialNo },
                    {"Description", officeItem.ItemDescription }
                };
                bool addofficeitem = await _sites.AddListItem(graphClient, _siteId,
                                                      listId,
                                                      data);
                return addofficeitem;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateItem(OfficeItem officeItem, GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);
            string userItemId = officeItem.ItemId;
            if (_sharePointLists != null)
            {
                List addItem = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string listId = addItem.Id;

                string itemId = userItemId;

                IDictionary<string, object> data = new Dictionary<string, object>
                {

                    {"OfficeItemID", officeItem.ItemId},
                    {"Title", officeItem.Title },
                    {"Resource_x0020_IDLookupId", officeItem.ResourceId },
                    {"SerialNo", officeItem.SerialNo },
                    {"Description", officeItem.ItemDescription }
                };
                bool updatebook = await _sites.UpdateListItem(graphClient, _siteId,
                                                      listId, itemId,
                                                      data);
                return updatebook;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteItem(OfficeItem officeItem, GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);
            string userItemId = officeItem.ItemId;
            if (_sharePointLists != null)
            {
                List addItem = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeItemsDisplayName)).FirstOrDefault();
                string listId = addItem.Id;
                string itemId = userItemId;

                bool deletebook = await _sites.DeleteListItem(graphClient, _siteId,
                                                      listId, itemId);
                return deletebook;
            }
            else
            {
                return false;
            }
        }
    }
}