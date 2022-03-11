using EmployeeManagementWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementWebApp.Data
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employees> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>().ToTable("Departments");
            modelBuilder.Entity<Employees>().ToTable("Employees");
        }
    }
}
