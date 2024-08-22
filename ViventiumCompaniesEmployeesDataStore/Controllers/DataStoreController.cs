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
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File does not exist");
            }
            else
            {
                try
                {
                    _dataStoreService.PostDataStore(filePath);

                    return Ok();
                }
                catch (Exception e) 
                {
                    return StatusCode(500, e.Message);
                }
            }
        }
    }
}
