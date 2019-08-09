using Microsoft.Graph;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    public class OfficeBookRepository : IOfficeBookRepository
    {
        private readonly string siteId = "m365b267815.sharepoint.com,6e1261a1-6d03-432a-95c0-e1c7705aef5f,f43d258c-ece0-476a-a1c0-018d359817d5";
        private ISiteListsCollectionPage sharePointLists;
        private readonly string itemId = null;
        public GraphServiceClient GraphClient { get; private set; }
        public async Task<List<OfficeBook>> GetBooks()
        {
            sharePointLists = await Sites.GetLists(GraphClient, siteId);
            List<OfficeBook> _officeBooksDirectoryList = new List<OfficeBook>();

            if (sharePointLists != null)
            {
                var _officeBookList = sharePointLists.Where(x => x.DisplayName.Contains("Office Books")).FirstOrDefault();
                var _listId = _officeBookList.Id;
                var _officeBookItems = await GetListItems(GraphClient, siteId, _listId);

                foreach (var item in _officeBookItems)
                {
                    var resourceList = item.Fields.AdditionalData;
                    var jsonString = JsonConvert.SerializeObject(resourceList);

                    var officeResource = JsonConvert.DeserializeObject<OfficeBook>(jsonString);
                    officeResource.ItemId = item.Id;
                    _officeBooksDirectoryList.Add(officeResource);
                }
            }
            return _officeBooksDirectoryList;
        }
        public async Task<bool> AddBook(OfficeBook officeBook)
        {
            sharePointLists = await Sites.GetLists(GraphClient, siteId);

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Books")).FirstOrDefault();
                string _listId = addbook.Id;

                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.BookDescription }
                };
                bool addofficebooks = await Sites.AddListItem(GraphClient, siteId,
                                                      _listId,
                                                      data);
                return addofficebooks;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateBook(OfficeBook officeBook)        
        {              
            sharePointLists = await Sites.GetLists(GraphClient, siteId);

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Books")).FirstOrDefault();
                string _listId = addbook.Id;

                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.BookDescription }
                };
                bool updatebook =  await Sites.UpdateListItem(GraphClient, siteId,
                                                       _listId, itemId,
                                                       data);
                return updatebook;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteBook(OfficeBook officeBook)
        {
            sharePointLists = await Sites.GetLists(GraphClient, siteId);
            string userItemId = officeBook. ItemId;

            if (sharePointLists != null)
            {
                var addbook = sharePointLists.Where(b => b.DisplayName.Contains("Office Books")).FirstOrDefault();
                string _listId = addbook.Id;

                string itemId = userItemId;

                bool deletebooks = await Sites.DeleteListItem(GraphClient, siteId,
                                                      _listId, itemId);
                return deletebooks;
            }
            else
            {
                return false; 
            }
        }
        private async static Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphClient, string siteId, string listId)
        {
            IListItemsCollectionPage listItems = await Sites.GetListItems(graphClient, siteId, listId);
            return listItems;
        }
    }
}
