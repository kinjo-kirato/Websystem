using WebEmployeeManagement.Infrastructures.Context;
using WebEmployeeManagement.Models;
using WebEmployeeManagement.Infrastructures.Entities;
using WebEmployeeManagement.Applications.Services;
using Microsoft.AspNetCore.Identity;
using EmployeeContext = WebEmployeeManagement.Infrastructures.Context.AppDbContext;
using Npgsql;
namespace WebEmployeeManagement.Models;
public class DbAccsess
{
    private static readonly string ConnectionString =
        Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
        ?? "Host=PostgreSQL 18;Port=5077;Database=sql_training;Username=postgres;Password=postgres"; 
 public static void Initialize()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

         var createDepartments = connection.CreateCommand();
        createDepartments.CommandText = @"
CREATE TABLE IF NOT EXISTS departments (
    department_id INTEGER PRIMARY KEY,
    department_name TEXT NOT NULL
);";
        createDepartments.ExecuteNonQuery();

        var createEmployees = connection.CreateCommand();
        createEmployees.CommandText = @"
CREATE TABLE IF NOT EXISTS employees (
    employee_id INTEGER PRIMARY KEY,
    employee_name TEXT NOT NULL,
    department_id INTEGER NOT NULL REFERENCES departments(department_id)
);";
        createEmployees.ExecuteNonQuery();

        var seedDepartments = connection.CreateCommand();
        seedDepartments.CommandText = @"
INSERT INTO departments (department_id, department_name)
VALUES (10, '営業部')
ON CONFLICT (department_id) DO NOTHING;

INSERT INTO departments (department_id, department_name)
VALUES (20, '開発部')
ON CONFLICT (department_id) DO NOTHING;";
        seedDepartments.ExecuteNonQuery();

        var seedEmployees = connection.CreateCommand();
        seedEmployees.CommandText = @"
INSERT INTO employees (employee_id, employee_name, department_id)
VALUES (1, '山田太郎', 10)
ON CONFLICT (employee_id) DO NOTHING;

INSERT INTO employees (employee_id, employee_name, department_id)
VALUES (2, '佐藤花子', 20)
ON CONFLICT (employee_id) DO NOTHING;";
        seedEmployees.ExecuteNonQuery();
    }

    public static List<Department> GetDepartments()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT department_id, department_name FROM departments ORDER BY department_id";

        var list = new List<Department>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Department
            {
                DepartmentId = reader.GetInt32(0),
                DepartmentName = reader.GetString(1)
            });
        }

        return list;
    }

    public static List<Employee> GetEmployees()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT employee_id, employee_name, department_id FROM employees ORDER BY employee_id";

        var list = new List<Employee>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Employee
            {
                EmployeeId = reader.GetInt32(0),
                EmployeeName = reader.GetString(1),
                DepartmentId = reader.GetInt32(2)
            });
        }

        return list;
    }

    public static bool ExistsEmployeeId(int employeeId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM employees WHERE employee_id = @employeeId";
        command.Parameters.AddWithValue("employeeId", employeeId);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public static bool ExistsDepartmentId(int departmentId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM departments WHERE department_id = @departmentId";
        command.Parameters.AddWithValue("departmentId", departmentId);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public static void AddEmployee(Employee employee)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO employees (employee_id, employee_name, department_id)
VALUES (@employeeId, @employeeName, @departmentId);";
        command.Parameters.AddWithValue("employeeId", employee.EmployeeId);
        command.Parameters.AddWithValue("employeeName", employee.EmployeeName);
        command.Parameters.AddWithValue("departmentId", employee.DepartmentId);
        command.ExecuteNonQuery();
    }

    public static void AddDepartment(Department department)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO departments (department_id, department_name)
VALUES (@departmentId, @departmentName);";
        command.Parameters.AddWithValue("departmentId", department.DepartmentId);
        command.Parameters.AddWithValue("departmentName", department.DepartmentName);
        command.ExecuteNonQuery();
    }
}