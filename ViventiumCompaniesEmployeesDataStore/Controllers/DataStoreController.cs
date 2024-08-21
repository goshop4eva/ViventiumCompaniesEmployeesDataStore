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
        public async Task<IActionResult> PostDataStore(string filePath)
        {
            if (_dataStoreService.Validate())
            {
                _dataStoreService.PostDataStore(filePath);

                return Ok();
            }
            else
            {
                return BadRequest("Validation failed");
            }
        }
    }
}
