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

        public bool PostDataStore(string filePath)
        {
            //_companiesContext.Database.CloseConnection();
            //_employeesContext.Database.CloseConnection();

            //CompaniesDATAContext.FileName = filePath;
            //EmployeesDATAContext.FileName = filePath;

            //_companiesContext.Database.OpenConnection();
            //_employeesContext.Database.OpenConnection();

            CompaniesDATAContext newCompaniesContext = new CompaniesDATAContext(filePath);
            EmployeesDATAContext newEmployeesContext = new EmployeesDATAContext(filePath);
            
            if (Validate(newCompaniesContext, newEmployeesContext))
            {
                _companiesContext = newCompaniesContext;
                _employeesContext = newEmployeesContext;

                return true;
            }

            return false;
               
        }

        public bool Validate(CompaniesDATAContext newCompaniesContext, EmployeesDATAContext newEmployeesContext)
        {
            var listOfCompaniesRows = newCompaniesContext.companiesDATA.ToList();

            // Use group by for distinct method to find the list of companies
            List<CompanyHeader> companies = listOfCompaniesRows.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.Id).ToList<CompanyHeader>();

            foreach (Company company in companies)
            {
                var employees = newEmployeesContext.employeesDATA.Where(e => e.CompanyId == company.Id).Select(x => x.EmployeeNumber);

                if (employees.Distinct().ToList().Count != employees.ToList().Count)
                {
                    return false;
                }
            }

            return true;

        }
    }
}
