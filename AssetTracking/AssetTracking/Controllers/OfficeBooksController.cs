using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetTracking.Helpers;
using AssetTracking.Interfaces;
using AssetTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace AssetTracking.Controllers
{
    public class OfficeBooksController : Controller
    {
        private readonly IGraphSdkHelper _graphSdkHelper;
        private readonly IOfficeBookRepository _officeBookRepository;
        private GraphServiceClient _graphClient;
        public readonly string siteId;

        public OfficeBooksController(IGraphSdkHelper graphSdkHelper, IOfficeBookRepository officeBookRepository, IConfiguration configuration)
        {
            _graphSdkHelper = graphSdkHelper;
            _officeBookRepository = officeBookRepository;
            siteId = configuration["SiteId"];
        }
    }
}