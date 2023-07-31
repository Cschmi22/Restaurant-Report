using Microsoft.EntityFrameworkCore;
using Restaurant_Reports.Models.Domain;

namespace Restaurant_Reports.Data
{
    public class RedDBContext : DbContext
    {
        public RedDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalesReport> SalesReports { get; set; }
    }
}
