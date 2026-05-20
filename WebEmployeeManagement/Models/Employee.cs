namespace EmployeeList.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
}