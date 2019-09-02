using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Interfaces
{
    public interface ISites
    {
        Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphClient, string siteId);
        Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphClient, string siteId, string listId);
        Task<bool> AddListItem(GraphServiceClient graphClient, string siteId, string listId, IDictionary<string, object> data);
        Task<bool> UpdateListItem(GraphServiceClient graphClient, string siteId, string listId, string itemId, IDictionary<string, object> data);
        Task<bool> DeleteListItem(GraphServiceClient graphClient, string siteId, string listId, string itemId);
    }
}
