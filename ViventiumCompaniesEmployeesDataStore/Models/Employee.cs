using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViventiumAPI.Models
{
    [Table("DATA$")]
    [Keyless]
    public class Employee : EmployeeHeader
    {
        [Column("EmployeeEmail")]
        public required String Email { get; set; }

        [Column("EmployeeDepartment")]
        public required String Department { get; set; }

        [Column("HireDate")]
        public DateTime? HireDate { get; set; }

        public EmployeeHeader[] Managers { get; set; } // List of EmployeeHeaders of the managers, ordered ascending by seniority(i.e.the immediate manage first)
    }
}
