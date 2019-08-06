using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    interface ISites
    {
        Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphClient, string siteId);
        Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphServiceClient, string siteId, string listId);
        Task<bool> AddListItem(GraphServiceClient graphServiceClient, string siteId, string listId, IDictionary<string, object> data);
        Task<bool> UpdateListItem(GraphServiceClient graphServiceClient, string siteId, string listId, string itemId, IDictionary<string, object> data);
        Task<bool> DeleteListItem(GraphServiceClient graphServiceClient, string siteId, string listId, string itemId);
    }
}
