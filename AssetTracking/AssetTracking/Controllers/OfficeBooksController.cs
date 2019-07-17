using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracking.Controllers
{
    [Route("[controller]")]
    public class OfficeBooksController : Controller
    {
        [Route("[controller]/[action]")]
        public IActionResult OfficeBooks()
        {
            return View("~/Views/Admin/OfficeBooks.cshtml");
        }
        [Route("")]
        public IActionResult ViewOfficeBooks()
        {
            return View("~/Views/User/Books.cshtml");
        }
    }
}