using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    interface ISites
    {
        Task<Site> GetSites(GraphServiceClient graphServiceClient, string groupsId, string siteId, string listId);
        Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphServiceClient, string groupsId, string siteId);
        Task<List> GetListById(GraphServiceClient graphServiceClient, string groupsId, string siteId, string listId);
        Task<ListItem> AddListItem(GraphServiceClient graphServiceClient, ListItem listItem, string groupId, string siteId, string listId);
        Task<FieldValueSet> UpdateListItem(GraphServiceClient graphServiceClient, FieldValueSet fieldValueSet, string groupId, string siteId, string listId, string listItemId);
        Task<ListItem> DeleteListItem(GraphServiceClient graphServiceClient,ListItem listItem, string groupId, string siteId, string listId);
    }
}
