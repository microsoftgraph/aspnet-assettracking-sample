using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace AssetTracking.Repositories
{
    public class Sites : ISites
    {
        public async Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphClient, string _siteId)
        {
            try
            {
                var result = await graphClient
                                .Sites[_siteId]
                                .Lists.Request().GetAsync();
                return result;
            }
            catch (ServiceException e)
            {
                return null;
            }
        }
        public async Task<IListItemsCollectionPage> GetListItems(GraphServiceClient graphClient, string _siteId, string listId)
        {
            IListItemsCollectionPage listItems = await graphClient
                            .Sites[_siteId]
                            .Lists[listId]
                            .Items
                            .Request().Expand("fields")
                            .GetAsync();
            return listItems;
        }
        public async Task<bool> AddListItem(GraphServiceClient graphClient, string _siteId, string listId, IDictionary<string, object> data)
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
                await graphClient

                                    .Sites[_siteId]
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
        public  async Task<bool> UpdateListItem(GraphServiceClient graphClient, string _siteId, string listId, string itemId, IDictionary<string, object> data)
        {
            var fieldValueSet = new FieldValueSet
            {
                AdditionalData = data,
            };
            try
            {
                await graphClient
                                .Sites[_siteId]
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
        public async Task<bool> DeleteListItem(GraphServiceClient graphClient, string _siteId, string listId, string itemId)
        { 
            try
            {
                await graphClient

                                .Sites[_siteId]
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

