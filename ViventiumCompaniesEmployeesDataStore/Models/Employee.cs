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

        public ICollection<EmployeeHeader> Managers { get; set; } = new List<EmployeeHeader>();
    }
}
