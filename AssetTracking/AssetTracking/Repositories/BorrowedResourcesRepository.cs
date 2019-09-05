using AssetTracking.Interfaces;
using AssetTracking.Models;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Repositories
{
    public class BorrowedResourcesRepository : IBorrowedResources
    {
        private const string BorrowedBookDisplayName = "BookID";
        private ISiteListsCollectionPage _sharePointList;
        private readonly Sites _sites;
        public BorrowedResourcesRepository()
        {
            _sites = new Sites();
            _sharePointList = new SiteListsCollectionPage();
        }
        public async Task<List<BorrowedResources>> GetBorrowedBooks(BorrowedResources borrowedResources,GraphServiceClient graphClient, string siteId)
        {
            _sharePointList = await _sites.GetLists(graphClient, siteId);
            List<BorrowedResources> borrowedBookDirectoryList = new List<BorrowedResources>();
            DateTime returnDate = borrowedResources.ReturnDate;
            DateTime borrowDate = borrowedResources.BorrowDate;
            DateTime dueDate = borrowedResources.DueDate;
            dueDate = borrowDate.AddDays(14);

            if (returnDate != null)
            {
                List borrowBookList = _sharePointList.Where(x => x.DisplayName.Contains(BorrowedBookDisplayName)).FirstOrDefault();
                string listId = borrowBookList.Id;
                IListItemsCollectionPage borrowedBooks = await _sites.GetListItems(graphClient, siteId, listId);

                foreach (ListItem item in borrowedBooks)
                {
                    IDictionary<string, object> resourceList = item.Fields.AdditionalData;
                    string jsonString = JsonConvert.SerializeObject(resourceList);

                    BorrowedResources borrowedBookResources = JsonConvert.DeserializeObject<BorrowedResources>(jsonString);
                    borrowedBookResources.ItemId = item.Id;
                    borrowedBookDirectoryList.Add(borrowedBookResources);
                }
            }

            return borrowedBookDirectoryList;
        }
        public async Task BorrowBook(BorrowedResources borrowedResources, GraphServiceClient graphClient, string siteId)
        {
            List officeBookList = _sharePointList.Where(x => x.DisplayName.Contains(BorrowedBookDisplayName)).FirstOrDefault();
            string listId = officeBookList.Id;
            DateTime returnDate = borrowedResources.ReturnDate;
            DateTime borrowDate = borrowedResources.BorrowDate;
            DateTime dueDate = borrowedResources.DueDate;
            dueDate = borrowDate.AddDays(14);
            IListItemsCollectionPage officeBookItem = await _sites.GetListItems(graphClient, siteId, listId);
            if (returnDate == null)
            {
                //Book is unavailable                 
            }

            else if ((returnDate != null) && (borrowDate > returnDate))
            {
                //Is available. 

            }
        }
    }
}
