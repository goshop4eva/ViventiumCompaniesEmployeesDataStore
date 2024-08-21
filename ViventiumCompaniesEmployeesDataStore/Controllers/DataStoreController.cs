using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ViventiumAPI.Models;
using ViventiumAPI.Services;

namespace ViventiumAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataStoreController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;

        private readonly IDataStoreService _dataStoreService;

        public DataStoreController(ILogger<CompaniesController> logger, IDataStoreService companiesService)
        {
            _logger = logger;
            _dataStoreService = companiesService;
        }

        [HttpPost]
        public IActionResult PostDataStore(string filePath)
        {
            if (_dataStoreService.PostDataStore(filePath))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Failed Validation on \r\n1) The employeeNumber should be unique within a given company.\r\n2) The manager of the given employee should exist in the same company.");
            }
        }
    }
}
