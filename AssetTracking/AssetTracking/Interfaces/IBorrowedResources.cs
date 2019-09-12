using AssetTracking.Models;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Interfaces
{
    public interface IBorrowedResources
    {
        Task<List<BorrowedResources>> GetBorrowedResources(BorrowedResources borrowedResources, GraphServiceClient graphClient, string siteId);
        Task<bool> BorrowBook(BorrowedResources borrowedResources, GraphServiceClient graphClient, string siteId);
        Task<bool> BorrowItem(BorrowedResources borrowedResources, GraphServiceClient graphClient, string siteId);
    }
}
