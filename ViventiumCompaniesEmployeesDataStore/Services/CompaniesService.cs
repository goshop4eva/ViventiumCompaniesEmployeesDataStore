using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Versioning;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using ViventiumAPI.Data;
using ViventiumAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ViventiumAPI.Services
{
    public class CompaniesService : ICompaniesService
    {
        const string CASE1 = "Case #1: Employee ID is not unique";
        const string CASE2 = "Case #2: Manager Id and Employee mismatched";

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

                Dictionary<String, EmployeeHeader> companyEmployees;

                try
                {
                    companyEmployees = company.Employees.ToDictionary(e => e.EmployeeNumber, e => e);
                }
                catch (System.ArgumentException)
                {
                    throw new Exception(CASE1);
                }

                foreach (Employee employee in company.Employees)
                {
                    String? managerId = employee.ManagerEmployeeNumber;

                    while (managerId != null)
                    {
                        EmployeeHeader? manager;

                        companyEmployees.TryGetValue(managerId, out manager);

                        if (manager != null)
                        {
                            employee.Managers.Add(manager);
                            managerId = manager.ManagerEmployeeNumber;
                        }
                        else if (manager == null && managerId != null)
                        {
                            // Error case
                            throw new Exception(CASE2);
                        }
                        else
                        {
                            managerId = null;
                        }
                    }
                }
            }

            return companies;
        }

        public async Task<Company?> GetCompanies(int companyId)
        {
            var companies = await GetCompanies();

            var company = companies.FirstOrDefault(x => x.Id == companyId) as Company;

            return company;
        }

        public async Task<Employee?> GetEmployees(int companyId, String employeeNumber)
        {
            var company = await GetCompanies(companyId);

            if (company is null)
            {
                return null;
            }
            else
            {
                var employee = company.Employees.Where(e => e.EmployeeNumber == employeeNumber).FirstOrDefault() as Employee;

                return employee;
            }
        }
    }
}
