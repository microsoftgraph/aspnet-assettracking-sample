using AssetTracking.Models;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Interfaces
{
    public interface IOfficeItemRepository
    {
        Task<List<OfficeItem>> GetItems(GraphServiceClient _graphClient, string siteId);
        Task<bool> AddItem(OfficeItem officeItem, GraphServiceClient graphClient, string siteId);
        Task<bool> UpdateItem(OfficeItem officeItem, GraphServiceClient graphClient, string siteId);
        Task<bool> DeleteItem(OfficeItem officeItem, GraphServiceClient graphClient, string siteId);
    }
}
