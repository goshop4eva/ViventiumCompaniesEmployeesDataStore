using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViventiumAPI.Models
{
    public class CompanyHeader
    {
        [Key]
        [Column("CompanyId")]
        public required int Id { get; set; }

        [Column("CompanyCode")]
        public required string Code { get; set; }

        [Column("CompanyDescription")]
        public required string Description { get; set; }

        [NotMapped]
        // Number of Employees in the given company
        public int EmployeeCount { get; set; }
    }
}
