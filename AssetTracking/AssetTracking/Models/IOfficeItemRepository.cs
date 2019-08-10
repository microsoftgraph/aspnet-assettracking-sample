using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracking.Models
{
    public interface IOfficeItemRepository
    {
        Task<List<OfficeItem>> GetItems();
        Task<bool> AddItem(OfficeItem officeItem);
        Task<bool> UpdateItem(OfficeItem officeItem);
        Task<bool> DeleteItem(OfficeItem officeItem);
    }
}
