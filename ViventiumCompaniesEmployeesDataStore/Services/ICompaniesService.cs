using ViventiumAPI.Models;

namespace ViventiumAPI.Services
{
    public interface ICompaniesService
    {
        Task<List<CompanyHeader>> GetCompanies();

        Task<Company> GetCompanies(int companyId);

        Task<Employee> GetEmployees(int companyId, String employeeNumber);
    }
}
