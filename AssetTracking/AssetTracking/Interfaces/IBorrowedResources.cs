using AssetTracking.Models;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Interfaces
{
    public interface IBorrowedResources
    {
        Task<List<BorrowedResources>> GetBorrowedBooks(BorrowedResources borrowedResources, GraphServiceClient graphClient, string siteId);
    }
}
