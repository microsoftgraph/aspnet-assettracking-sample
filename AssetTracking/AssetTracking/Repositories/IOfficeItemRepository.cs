using AssetTracking.Models;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Repositories
{
    public interface IOfficeItemRepository
    {
        Task<List<OfficeItem>> GetItems(GraphServiceClient _graphClient);
        Task<bool> AddItem(OfficeItem officeItem, GraphServiceClient graphClient);
        Task<bool> UpdateItem(OfficeItem officeItem, GraphServiceClient graphClient);
        Task<bool> DeleteItem(OfficeItem officeItem, GraphServiceClient graphClient);
    }
}
