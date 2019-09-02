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
        private readonly string siteId;
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
            _sharePointLists = await _sites.GetLists(graphClient, siteId);

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
                bool addOfficeBooks = await _sites.AddListItem(graphClient, siteId,
                                                      listId,
                                                      data);
                return addOfficeBooks;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId)        
        {
            _sharePointLists = await _sites.GetLists(graphClient, this.siteId);
            string userItemId = officeBook.ItemId;

            if (_sharePointLists != null)
            {
                List addBook = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = addBook.Id;
                string itemId = userItemId;
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.Description }
                };
                bool updateBook =  await _sites.UpdateListItem(graphClient, this.siteId,
                                                       listId, itemId,
                                                       data);
                return updateBook;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId)
        {
            _sharePointLists = await _sites.GetLists(graphClient, this.siteId);
            string userItemId = officeBook.ItemId;
            if (_sharePointLists != null)
            {
                List addBook = _sharePointLists.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = addBook.Id;
                string itemId = userItemId;
                bool deleteBooks = await _sites.DeleteListItem(graphClient, this.siteId,
                                                      listId, itemId);
                return deleteBooks;
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
