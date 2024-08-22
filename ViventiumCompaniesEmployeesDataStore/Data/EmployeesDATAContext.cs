using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ViventiumAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ViventiumAPI.Data
{
    public class EmployeesDATAContext : DbContext
    {
        public EmployeesDATAContext(string fileName)
        : base()
        {
            FileName = fileName;
        }

        public EmployeesDATAContext(DbContextOptions<EmployeesDATAContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                FileName = "DATA.xlsx";
            }

            options.UseJet(@"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = "+ FileName + ";Extended Properties = 'Excel 12.0 Xml';");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static string FileName { get; set; } = string.Empty;

        public DbSet<Employee> employeesDATA { get; set; }
    }
}
