using System.ComponentModel.DataAnnotations.Schema;

namespace ViventiumAPI.Models
{
    [Table("DATA$")]
    public class Company : CompanyHeader
    {
        public required EmployeeHeader[] Employees { get; set; }
    }
}
