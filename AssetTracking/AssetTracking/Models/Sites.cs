using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using AssetTracking.Models;
using AssetTracking.Helpers;

namespace AssetTracking
{
    public static class Sites
    {
        public static async Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphClient, string siteId)
        {
            try
            {
                var result = await graphClient
                                .Sites[siteId]
                                .Lists.Request().GetAsync();

                return result;
            }
            catch (ServiceException e)
            {
                return null;
            }
        }
        public async static Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphServiceClient, string siteId, string listId)
        {
            IListItemsCollectionPage listItems = await graphServiceClient
                            .Sites[siteId]
                            .Lists[listId]
                            .Items
                            .Request().Expand("fields")
                            .GetAsync();
            return listItems;
        }
        public static async Task<bool> AddListItem(GraphServiceClient graphServiceClient, string siteId, string listId, IDictionary<string, object> data)
        {
            var listItem = new ListItem
            {
                Fields = new FieldValueSet
                {
                    AdditionalData = data,
                }
            };

            try
            {
                await graphServiceClient

                                    .Sites[siteId]
                                    .Lists[listId]
                                    .Items
                               .Request()
                               .AddAsync(listItem);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static async Task<bool> UpdateListItem(GraphServiceClient graphServiceClient, string siteId, string listId, string itemId, IDictionary<string, object> data)
        {
            var fieldValueSet = new FieldValueSet
            {
                AdditionalData = data,
            };

            try
            {
                await graphServiceClient
                                .Sites[siteId]
                                .Lists[listId]
                                .Items[itemId]
                                .Fields
                                .Request()
                                .UpdateAsync(fieldValueSet);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async static Task<bool> DeleteListItem(GraphServiceClient graphServiceClient, string siteId, string listId, string itemId)
        { 
            try
            {
                await graphServiceClient

                                .Sites[siteId]
                                .Lists[listId]
                                .Items[itemId]
                                .Request()
                                .DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

