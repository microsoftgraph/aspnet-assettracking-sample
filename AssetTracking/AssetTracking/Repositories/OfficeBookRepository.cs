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
        private ISiteListsCollectionPage _sharePointList;
        private readonly Sites _sites;

        public OfficeBookRepository()
        {
            _sites = new Sites();
            _sharePointList = new SiteListsCollectionPage();
        }

        public async Task<List<OfficeBook>> GetBooks(GraphServiceClient graphClient, string siteId)
        {
            _sharePointList = await _sites.GetLists(graphClient, siteId);
            List<OfficeBook> officeBooksDirectoryList = new List<OfficeBook>();

            if (_sharePointList != null)
            {
                List officeBookList = _sharePointList.Where(x => x.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = officeBookList.Id;
                IListItemsCollectionPage officeBookItem = await _sites.GetListItems(graphClient, siteId, listId);

                foreach (ListItem item in officeBookItem)
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
            _sharePointList = await _sites.GetLists(graphClient, siteId);

            if (_sharePointList != null)
            {
                List officeBookList = _sharePointList.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = officeBookList.Id;
                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.Description }
                };
                bool addOfficeBook = await _sites.AddListItem(graphClient, siteId,
                                                      listId,
                                                      data);
                return addOfficeBook;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId)        
        {
            _sharePointList = await _sites.GetLists(graphClient, siteId);
            string userItemId = officeBook.ItemId;

            if (_sharePointList != null)
            {
                List addBook = _sharePointList.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
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
                bool updateBook =  await _sites.UpdateListItem(graphClient, siteId,
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
            _sharePointList = await _sites.GetLists(graphClient, siteId);
            string userItemId = officeBook.ItemId;

            if (_sharePointList != null)
            {
                List addBook = _sharePointList.Where(b => b.DisplayName.Contains(OfficeBooksDisplayName)).FirstOrDefault();
                string listId = addBook.Id;
                string itemId = userItemId;
                bool deleteBook = await _sites.DeleteListItem(graphClient, siteId,
                                                      listId, itemId);
                return deleteBook;
            }
            else
            {
                return false; 
            }
        }
    }
}
