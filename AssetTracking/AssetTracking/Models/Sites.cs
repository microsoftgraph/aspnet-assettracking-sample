using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using AssetTracking.Models;
using AssetTracking.Helpers;

namespace AssetTracking
{
    public class Sites : ISites
    {
        private static GraphServiceClient _graphClient = null;

        public async Task<Site> GetSites(string groupsId, string siteId, string listId)
        {
            _graphClient = GraphHelper.GetAuthenticatedClient();
            var sites = await _graphClient
                            .Groups[groupsId]
                            .Sites[siteId]
                            .Request()
                            .GetAsync();
            return sites;
        }

        public async Task<ISiteListsCollectionPage> GetLists(string groupsId, string siteId)
        {
            _graphClient = new GraphServiceClient(null);
            var result = await _graphClient
                            .Groups[groupsId]
                            .Sites[siteId]
                            .Lists.Request().GetAsync();

            return result;
        }

        public async Task<List> GetListById(string groupsId, string siteId, string listId)
        {
            _graphClient = GraphHelper.GetAuthenticatedClient();
            var list = _graphClient
                            .Groups[groupsId]
                            .Sites[siteId]
                            .Lists[listId]
                            .Request().Expand("fields")
                            .GetAsync();
            return await list;
        }

        public async Task<ListItem> AddListItem(ListItem listItem, string groupId, string siteId, string listId)
        {
            _graphClient = GraphHelper.GetAuthenticatedClient();

            ListItem result = new ListItem();
            try
            {
                result = await _graphClient.Groups[groupId]
                            .Sites[siteId]
                            .Lists[listId]
                              .Items
                              .Request()
                              .AddAsync(listItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<FieldValueSet> UpdateListItem(FieldValueSet fieldValueSet, string groupId, string siteId, string listId, string listItemId)
        {
            _graphClient = GraphHelper.GetAuthenticatedClient();

            FieldValueSet result = new FieldValueSet();
            try
            {
                result = await _graphClient.Groups[groupId]
                           .Sites[siteId]
                           .Lists[listId]
                              .Items[listItemId]
                              .Fields
                              .Request()
                              .UpdateAsync(fieldValueSet);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<ListItem> DeleteListItem(ListItem listItem, string groupId, string siteId, string listId)
        {
            throw new NotImplementedException();
        }
    }
}

