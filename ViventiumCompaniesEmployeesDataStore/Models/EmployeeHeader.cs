using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViventiumAPI.Models
{
    public class EmployeeHeader
    {
        [Key]
        [Column("EmployeeNumber")]
        public required String EmployeeNumber { get; set; }

        [Column("EmployeeFirstName")]
        public required String FirstName { get; set; }

        [Column("EmployeeLastName")]
        public required String LastName { get; set; }

        //[ForeignKey(nameof(Company.Id))]
        [Column("CompanyId")]
        public int CompanyId { get; set; }

        // Is there another way we can utilize entity framework?
        [Column("ManagerEmployeeNumber")]
        public String? ManagerEmployeeNumber { get; set; }

        [NotMapped]
        String FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
