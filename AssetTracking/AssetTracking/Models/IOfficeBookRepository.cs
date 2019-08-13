using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    public interface IOfficeBookRepository
    {
        Task<List<OfficeBook>> GetBooks(GraphServiceClient _graphClient);
        Task<bool> AddBook(OfficeBook officeBook, GraphServiceClient graphClient);
        Task<bool> UpdateBook(OfficeBook officeBook, GraphServiceClient graphClient);
        Task<bool> DeleteBook(OfficeBook officeBook, GraphServiceClient graphClient);
    }
}
