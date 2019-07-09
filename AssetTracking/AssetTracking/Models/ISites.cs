using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    interface ISites
    {
        Task<Site> GetSites(string groupsId, string siteId, string listId);
        Task<ISiteListsCollectionPage> GetLists(string groupsId, string siteId);
        Task<List> GetListById(string groupsId, string siteId, string listId);
        Task<ListItem> AddListItem(ListItem listItem, string groupId, string siteId, string listId);
        Task<FieldValueSet> UpdateListItem(FieldValueSet fieldValueSet, string groupId, string siteId, string listId, string listItemId);
        Task<ListItem> DeleteListItem(ListItem listItem, string groupId, string siteId, string listId);
    }
}
