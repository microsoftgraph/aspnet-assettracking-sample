using AssetTracking.Interfaces;
using AssetTracking.Models;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace AssetTracking.Repositories
{
    public class OfficeBookRepository : IOfficeBookRepository
    {
        private const string OfficeBooksDisplayName = "OfficeBooks";
        private ISiteListsCollectionPage _sharePointLists;
        private readonly Sites _sites;
        private readonly string _siteId;
        public OfficeBookRepository()
        {
            _sites = new Sites();
            _sharePointLists = new SiteListsCollectionPage();
        }
        public async Task<List<OfficeBook>> GetBooks(GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, siteId);
            List<OfficeBook> officeBooksDirectoryList = new List<OfficeBook>();
            if (_sharePointLists != null)
            {
                List officeBookList = _sharePointLists.Where(x => x.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = officeBookList.Id;
                IListItemsCollectionPage officeBookItems = await GetListItems(graphClient, siteId, listId);
                foreach (ListItem item in officeBookItems)
                {
                    IDictionary<string,object> resourceList = item.Fields.AdditionalData;
                    string jsonString = JsonConvert.SerializeObject(resourceList);

                    OfficeBook officeResource = JsonConvert.DeserializeObject<OfficeBook>(jsonString);
                    officeResource.ItemId = item.Id;
                    officeBooksDirectoryList.Add(officeResource);
                }
            }
            return officeBooksDirectoryList;
        }
        public async Task<bool> AddBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);

            if (_sharePointLists != null)
            {
                List officeBookList = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = officeBookList.Id;
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.Description }
                };
                bool addofficebooks = await _sites.AddListItem(graphClient, _siteId,
                                                      listId,
                                                      data);
                return addofficebooks;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId)        
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);
            string userItemId = officeBook.ItemId;

            if (_sharePointLists != null)
            {
                List addbook = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = addbook.Id;
                string itemId = userItemId;
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.Description }
                };
                bool updatebook =  await _sites.UpdateListItem(graphClient, _siteId,
                                                       listId, itemId,
                                                       data);
                return updatebook;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, _siteId);
            string userItemId = officeBook.ItemId;
            if (_sharePointLists != null)
            {
                List addbook = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = addbook.Id;
                string itemId = userItemId;
                bool deletebooks = await _sites.DeleteListItem(graphClient, _siteId,
                                                      listId, itemId);
                return deletebooks;
            }
            else
            {
                return false; 
            }
        }
        private async static Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphClient, string siteId, string listId)
        {
            Sites sites = new Sites();
            IListItemsCollectionPage listItems = await sites.GetListItems(graphClient, siteId, listId);
            return listItems;
        }
    }
}
