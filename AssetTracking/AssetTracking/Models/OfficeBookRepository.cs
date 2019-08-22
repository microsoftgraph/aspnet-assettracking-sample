using Microsoft.Graph;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    public class OfficeBookRepository : IOfficeBookRepository
    {
        private string SiteId { get;set; }
        private ISiteListsCollectionPage sharePointLists;
        private const string officeBooksDisplayName = "OfficeBooks";
        //Gets Office Books
        public async Task<List<OfficeBook>> GetBooks(GraphServiceClient graphClient)
        {
            sharePointLists = await Sites.GetLists(graphClient, SiteId);
            List<OfficeBook> _officeBooksDirectoryList = new List<OfficeBook>();

            if (sharePointLists != null)
            {
                var _officeBookList = sharePointLists.Where(x => x.DisplayName.Contains(officeBooksDisplayName)).FirstOrDefault();
                var _listId = _officeBookList.Id;
                var _officeBookItems = await GetListItems(graphClient, SiteId, _listId);

                foreach (var item in _officeBookItems)
                {
                    var _resourceList = item.Fields.AdditionalData;
                    var _jsonString = JsonConvert.SerializeObject(_resourceList);

                    var _officeResource = JsonConvert.DeserializeObject<OfficeBook>(_jsonString);
                    _officeResource.ItemId = item.Id;
                    _officeBooksDirectoryList.Add(_officeResource);
                }
            }
            return _officeBooksDirectoryList;
        }
        //Adds Office Book
        public async Task<bool> AddBook(OfficeBook officeBook, GraphServiceClient graphClient)
        {
            sharePointLists = await Sites.GetLists(graphClient, SiteId);

            if (sharePointLists != null)
            {
                var _officeBookList = sharePointLists.Where(b => b.DisplayName.Contains(officeBooksDisplayName)).FirstOrDefault();
                string _listId = _officeBookList.Id;

                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.Description }
                };
                bool _addofficebooks = await Sites.AddListItem(graphClient, SiteId,
                                                      _listId,
                                                      data);
                return _addofficebooks;
            }
            else
            {
                return false;
            }
        }
        //Update Office Books
        public async Task<bool> UpdateBook(OfficeBook officeBook, GraphServiceClient graphClient)        
        {              
            sharePointLists = await Sites.GetLists(graphClient, SiteId);
            string userItemId = officeBook.ItemId;

            if (sharePointLists != null)
            {
                var _addbook = sharePointLists.Where(b => b.DisplayName.Contains(officeBooksDisplayName)).FirstOrDefault();
                string _listId = _addbook.Id;
                string _itemId = userItemId;

                IDictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Resource_x0020_IDLookupId", "2" },
                    { "Title", officeBook.Title },
                    { "ISBN", officeBook.ISBN },
                    { "Author0", officeBook.Author },
                    { "BookTitle", officeBook.Description }
                };
                bool _updatebook =  await Sites.UpdateListItem(graphClient, SiteId,
                                                       _listId, _itemId,
                                                       data);
                return _updatebook;
            }
            else
            {
                return false;
            }
        }
        //  Deletes Office Book
        public async Task<bool> DeleteBook(OfficeBook officeBook, GraphServiceClient graphClient)
        {
            sharePointLists = await Sites.GetLists(graphClient, SiteId);
            string _userItemId = officeBook.ItemId;

            if (sharePointLists != null)
            {
                var _addbook = sharePointLists.Where(b => b.DisplayName.Contains(officeBooksDisplayName)).FirstOrDefault();
                string _listId = _addbook.Id;

                string _itemId = _userItemId;

                bool _deletebooks = await Sites.DeleteListItem(graphClient, SiteId,
                                                      _listId, _itemId);
                return _deletebooks;
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
