using Microsoft.AspNetCore.Mvc;
using EmployeeList.Models;

namespace EmployeeList.Controllers;

public class EmployeesController : Controller
{
    public IActionResult Index()
    {
        var employeeList = new List<Employee>
        {
            new Employee { EmployeeId = 1, EmployeeName = "山田太郎", DepartmentId = 10 },
            new Employee { EmployeeId = 2, EmployeeName = "佐藤花子", DepartmentId = 20 }
        };

        return View(employeeList);
    }
}
    