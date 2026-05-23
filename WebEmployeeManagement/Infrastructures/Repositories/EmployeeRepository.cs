using Npgsql;
using WebEmployeeManagement.Applications.Interfaces;
using WebEmployeeManagement.Infrastructures.Entities;

namespace WebEmployeeManagement.Infrastructures.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(IConfiguration configuration)
    {
        _connectionString = configuration["DB_CONNECTION_STRING"]
            ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=sql_training";
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        await using var connection = await CreateConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT employee_id, employee_name, department_id FROM employees ORDER BY employee_id";

        var employees = new List<Employee>();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            employees.Add(new Employee
            {
                EmployeeId = reader.GetInt32(0),
                EmployeeName = reader.GetString(1),
                DepartmentId = reader.GetInt32(2)
            });
        }

        return employees;
    }

    public async Task<Employee?> FindByIdAsync(int id)
    {
        await using var connection = await CreateConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT employee_id, employee_name, department_id FROM employees WHERE employee_id = @employeeId";
        command.Parameters.AddWithValue("employeeId", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Employee
        {
            EmployeeId = reader.GetInt32(0),
            EmployeeName = reader.GetString(1),
            DepartmentId = reader.GetInt32(2)
        };
    }

    public async Task AddAsync(Employee employee)
    {
        await using var connection = await CreateConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO employees (employee_id, employee_name, department_id)
VALUES (@employeeId, @employeeName, @departmentId);";
        command.Parameters.AddWithValue("employeeId", employee.EmployeeId);
        command.Parameters.AddWithValue("employeeName", employee.EmployeeName);
        command.Parameters.AddWithValue("departmentId", employee.DepartmentId);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        await using var connection = await CreateConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = @"
UPDATE employees
SET employee_name = @employeeName, department_id = @departmentId
WHERE employee_id = @employeeId;";
        command.Parameters.AddWithValue("employeeId", employee.EmployeeId);
        command.Parameters.AddWithValue("employeeName", employee.EmployeeName);
        command.Parameters.AddWithValue("departmentId", employee.DepartmentId);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var connection = await CreateConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM employees WHERE employee_id = @employeeId";
        command.Parameters.AddWithValue("employeeId", id);
        await command.ExecuteNonQueryAsync();
    }

    private async Task<NpgsqlConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
