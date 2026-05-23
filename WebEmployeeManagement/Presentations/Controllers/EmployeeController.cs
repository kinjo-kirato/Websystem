using Microsoft.AspNetCore.Mvc;
using WebEmployeeManagement.Infrastructures.Entities;
using WebEmployeeManagement.Infrastructures.DataAccess;

namespace WebEmployeeManagement.Presentations.Controllers;

public class EmployeesController : Controller
{
    public IActionResult Index()
    {
        return View(DbAccsess.GetEmployees());
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
            if (DbAccsess.ExistsEmployeeId(employee.EmployeeId))
            {
                ViewBag.ErrorMessage = "同じ社員IDが既に存在します。";
                return View(employee);
            }

            if (!DbAccsess.ExistsDepartmentId(employee.DepartmentId))
            {
                ViewBag.ErrorMessage = "指定された部署IDは存在しません。";
                return View(employee);
            }

            DbAccsess.AddEmployee(employee);
            return RedirectToAction("Index");
        }

        return View(employee);
    }
}
    
