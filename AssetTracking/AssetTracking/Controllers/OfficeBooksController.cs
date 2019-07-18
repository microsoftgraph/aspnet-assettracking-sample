using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracking.Controllers
{
    //[Route("[controller]")]
    public class OfficeBooksController : Controller
    {
        [Route("admin/[controller]")]
        public IActionResult OfficeBooks()
        {
            return View("~/Views/Admin/OfficeBooks.cshtml");
        }
        [Route("[controller]")]
        public IActionResult ViewOfficeBooks()
        {
            return View("~/Views/User/Books.cshtml");
        }
    }
}