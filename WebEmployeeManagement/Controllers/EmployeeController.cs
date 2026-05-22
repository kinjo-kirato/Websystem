using Microsoft.AspNetCore.Mvc;
using WebEmployeeManagement.Applications.Services;
using WebEmployeeManagement.Infrastructures.Entities;
using WebEmployeeManagement.Infrastructures.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace WebEmployeeManagement.Controllers;

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
    public IActionResult Create()
    {
        return View();
    }
        [HttpPost]
    public IActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            
            // データベースに保存する処理をここに追加
            return RedirectToAction("Index");
        }
        return View(employee);
    }
}
    