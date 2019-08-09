using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    public interface IOfficeBookRepository
    {
        Task<List<OfficeBook>> GetBooks();
        Task<bool> AddBook(OfficeBook officeBook);
        Task<bool> UpdateBook(OfficeBook officeBook);
        Task<bool> DeleteBook(OfficeBook officeBook);
    }
}
