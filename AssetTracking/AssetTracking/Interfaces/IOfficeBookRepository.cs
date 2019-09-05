using AssetTracking.Models;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Interfaces
{
    public interface IOfficeBookRepository
    {
        Task<List<OfficeBook>> GetBooks(GraphServiceClient graphClient, string siteId);
        Task<bool> AddBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId);
        Task<bool> UpdateBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId);
        Task<bool> DeleteBook(OfficeBook officeBook, GraphServiceClient graphClient, string siteId);
    }
}
