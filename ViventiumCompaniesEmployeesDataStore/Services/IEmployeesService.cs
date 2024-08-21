using ViventiumAPI.Models;

namespace ViventiumAPI.Services
{
    public interface IEmployeesService
    {
        Task<List<EmployeeHeader>> GetEmployees();
    }
}
