using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracking.Controllers
{
    [Route("[controller]")]
    public class OfficeItemsController : Controller
    {
        [Route("[controller]/[action]")]
        public IActionResult OfficeItems()
        {
            return View("~/Views/Admin/OfficeItems.cshtml");
        }

        [Route("")]
        public IActionResult ViewOfficeItems()
        {
            return View("~/Views/User/Items.cshtml");
        }
    }
}