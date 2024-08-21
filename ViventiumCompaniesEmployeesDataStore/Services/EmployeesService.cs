using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ViventiumAPI.Data;
using ViventiumAPI.Models;

namespace ViventiumAPI.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly EmployeesDATAContext _context;

        public EmployeesService(EmployeesDATAContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeHeader>> GetEmployees()
        {
            var employees = await _context.employeesDATA.ToListAsync<EmployeeHeader>();

            return employees;
        }
    }
}
