using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentList.Models;
using WebEmployeeManagement.Infrastructures.Entities;
namespace DepartmentList.Models;

public static class SampleDataStore
{
    public static List<Department> Departments { get; } = new()
    {
        new Department { DepartmentId = 10, DepartmentName = "営業部" },
        new Department { DepartmentId = 20, DepartmentName = "開発部" }
    };

    public static List<Employee> Employees { get; } = new()
    {
        new Employee { EmployeeId = 1, EmployeeName = "山田太郎", DepartmentId = 10 },
        new Employee { EmployeeId = 2, EmployeeName = "佐藤花子", DepartmentId = 20 }
    };
}