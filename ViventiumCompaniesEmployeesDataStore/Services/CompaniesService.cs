using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using ViventiumAPI.Data;
using ViventiumAPI.Models;

namespace ViventiumAPI.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly CompaniesDATAContext _companiesContext;
        private readonly EmployeesDATAContext _employeecontext;

        public CompaniesService(CompaniesDATAContext companiesContext, EmployeesDATAContext employeesContext)
        {
            _companiesContext = companiesContext;
            _employeecontext = employeesContext;
        }

        public async Task<List<CompanyHeader>> GetCompanies()
        {
            var listOfCompaniesRows = await _companiesContext.companiesDATA.ToListAsync();

            // Use group by for distinct method to find the list of companies
            List<CompanyHeader> companies = listOfCompaniesRows.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.Id).ToList<CompanyHeader>();

            foreach (Company company in companies)
            {
                company.Employees = _employeecontext.employeesDATA.Where(e => e.CompanyId == company.Id).ToArray<EmployeeHeader>();
                company.EmployeeCount = company.Employees.Length;
            }

            return companies;
        }

        public async Task<Company> GetCompanies(int companyId)
        {
            var company = (Company)GetCompanies().Result.FirstOrDefault(x => x.Id == companyId);

            return company;
        }

        public async Task<Employee> GetEmployees(int companyId, String employeeNumber)
        {
            var company = GetCompanies(companyId);

            var employee = company.Result.Employees.Where(e => e.EmployeeNumber == employeeNumber).FirstOrDefault();

            return (Employee)employee;
        }
    }
}
