using Npgsql;
using WebEmployeeManagement.Applications.Services;
using WebEmployeeManagement.Infrastructures.Entities;

namespace WebEmployeeManagement.Infrastructures.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly string _connectionString;

    public DepartmentRepository(IConfiguration configuration)
    {
        _connectionString = configuration["DB_CONNECTION_STRING"]
            ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=sql_training";
    }

    public List<Department> GetAll()
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT department_id, department_name FROM departments ORDER BY department_id";

        var departments = new List<Department>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            departments.Add(new Department
            {
                DepartmentId = reader.GetInt32(0),
                DepartmentName = reader.GetString(1)
            });
        }

        return departments;
    }

    public Department? Find(int departmentId)
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT department_id, department_name FROM departments WHERE department_id = @departmentId";
        command.Parameters.AddWithValue("departmentId", departmentId);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Department
        {
            DepartmentId = reader.GetInt32(0),
            DepartmentName = reader.GetString(1)
        };
    }

    public bool ExistsById(int departmentId)
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM departments WHERE department_id = @departmentId";
        command.Parameters.AddWithValue("departmentId", departmentId);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public bool HasEmployees(int departmentId)
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM employees WHERE department_id = @departmentId";
        command.Parameters.AddWithValue("departmentId", departmentId);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public void Add(Department department)
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO departments (department_id, department_name)
VALUES (@departmentId, @departmentName);";
        command.Parameters.AddWithValue("departmentId", department.DepartmentId);
        command.Parameters.AddWithValue("departmentName", department.DepartmentName);
        command.ExecuteNonQuery();
    }

    public void Remove(Department department)
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM departments WHERE department_id = @departmentId";
        command.Parameters.AddWithValue("departmentId", department.DepartmentId);
        command.ExecuteNonQuery();
    }

    public void MoveEmployees(int fromDepartmentId, int toDepartmentId)
    {
        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
UPDATE employees
SET department_id = @toDepartmentId
WHERE department_id = @fromDepartmentId;";
        command.Parameters.AddWithValue("toDepartmentId", toDepartmentId);
        command.Parameters.AddWithValue("fromDepartmentId", fromDepartmentId);
        command.ExecuteNonQuery();
    }

    public void SaveChanges()
    {
        // Npgsql direct execution persists each command immediately.
    }

    private NpgsqlConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
