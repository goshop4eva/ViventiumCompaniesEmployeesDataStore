using Microsoft.AspNetCore.Mvc;
using ViventiumAPI.Models;
using ViventiumAPI.Services;

namespace ViventiumAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;

        private readonly ICompaniesService _companiesService;

        public CompaniesController(ILogger<CompaniesController> logger, ICompaniesService companiesService)
        {
            _logger = logger;
            _companiesService = companiesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyHeader>>> GetCompanies()
        {
            return await _companiesService.GetCompanies();
        }

        [Route("{companyId}")]
        [HttpGet]
        public async Task<ActionResult<Company?>> GetCompanies(int companyId)
        {
            return await _companiesService.GetCompanies(companyId);
        }

        [Route("{companyId}/Employees/{employeeNumber}")]
        [HttpGet]
        public async Task<ActionResult<Employee?>> GetEmployees(int companyId, String employeeNumber)
        {
            return await _companiesService.GetEmployees(companyId, employeeNumber);
        }

    }
}
