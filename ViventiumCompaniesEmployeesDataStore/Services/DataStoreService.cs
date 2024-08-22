using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ViventiumAPI.Data;
using ViventiumAPI.Models;

namespace ViventiumAPI.Services
{
    public class DataStoreService : IDataStoreService
    {
        private CompaniesDATAContext _companiesContext;
        private EmployeesDATAContext _employeesContext;

        public DataStoreService(CompaniesDATAContext companiesContext, EmployeesDATAContext employeesContext)
        {
            _companiesContext = companiesContext;
            _employeesContext = employeesContext;
        }

        public void PostDataStore(string filePath)
        {
            CompaniesDATAContext newCompaniesContext = new CompaniesDATAContext(filePath);
            EmployeesDATAContext newEmployeesContext = new EmployeesDATAContext(filePath);

            Validate(newCompaniesContext, newEmployeesContext);

            // no exceptions are thrown
            _companiesContext = newCompaniesContext;
            _employeesContext = newEmployeesContext;
        }

        public void Validate(CompaniesDATAContext newCompaniesContext, EmployeesDATAContext newEmployeesContext)
        {
            CompaniesService companyService = new CompaniesService(newCompaniesContext, newEmployeesContext);

            var companies = companyService.GetCompanies();

            if (companies.Exception != null) 
            {
                throw new Exception(companies.Exception.ToString());
            }
        }
    }
}
