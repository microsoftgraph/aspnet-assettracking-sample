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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="groupsId"></param>
        /// <param name="siteId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static async Task<Site> GetSites(GraphServiceClient graphServiceClient, string groupsId, string siteId, string listId)
        {
            var sites = await graphServiceClient
                            .Groups[groupsId]
                            .Sites[siteId]
                            .Request()
                            .GetAsync();
            return sites;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public static async Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphServiceClient, string siteId)
        {
            try
            {
                var result = await graphServiceClient
                                .Sites[siteId]
                                .Lists.Request().GetAsync();

                return result;
            }
            catch (ServiceException e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="groupsId"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public static async Task<ISiteListsCollectionPage> GetLists(GraphServiceClient graphServiceClient, string groupsId, string siteId)
        {
            var result = await graphServiceClient
                            .Groups[groupsId]
                            .Sites[siteId]
                            .Lists.Request().GetAsync();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="groupsId"></param>
        /// <param name="siteId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static async Task<List> GetListById(GraphServiceClient graphServiceClient, string groupsId, string siteId, string listId)
        {
            var list = graphServiceClient
                            .Groups[groupsId]
                            .Sites[siteId]
                            .Lists[listId]
                            .Request().Expand("fields")
                            .GetAsync();
            return await list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="listItem"></param>
        /// <param name="groupId"></param>
        /// <param name="siteId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static async Task<ListItem> AddListItem(GraphServiceClient graphServiceClient, ListItem listItem, string groupId, string siteId, string listId)
        {
            ListItem result = new ListItem();
            try
            {
                result = await graphServiceClient.Groups[groupId]
                            .Sites[siteId]
                            .Lists[listId]
                              .Items
                              .Request()
                              .AddAsync(listItem);
            }
            catch (Exception ex)
            {
      
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="fieldValueSet"></param>
        /// <param name="groupId"></param>
        /// <param name="siteId"></param>
        /// <param name="listId"></param>
        /// <param name="listItemId"></param>
        /// <returns></returns>
        public static async Task<FieldValueSet> UpdateListItem(GraphServiceClient graphServiceClient, FieldValueSet fieldValueSet, string groupId, string siteId, string listId, string listItemId)
        {
            FieldValueSet result = new FieldValueSet();
            try
            {
                result = await graphServiceClient.Groups[groupId]
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
       
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphServiceClient"></param>
        /// <param name="listItem"></param>
        /// <param name="groupId"></param>
        /// <param name="siteId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static async Task<ListItem> DeleteListItem(GraphServiceClient graphServiceClient, ListItem listItem, string groupId, string siteId, string listId)
        {
            throw new NotImplementedException();
        }
    }
}

