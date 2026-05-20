using WebEmployeeManagement.Infrastructures.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebEmployeeManagement.Applications.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
