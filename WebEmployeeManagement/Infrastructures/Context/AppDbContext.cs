using WebEmployeeManagement.Infrastructures.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebEmployeeManagement.Infrastructures.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee>EmployeeName{ get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
