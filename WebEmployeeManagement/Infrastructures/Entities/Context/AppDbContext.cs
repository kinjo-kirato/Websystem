using Microsoft.EntityFrameworkCore;

namespace WebEmployeeManagement.Infrastructures.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
