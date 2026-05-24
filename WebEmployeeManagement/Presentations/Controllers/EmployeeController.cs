using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        SetDepartmentOptions();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            if (!DbAccsess.ExistsDepartmentId(employee.DepartmentId))
            {
                ViewBag.ErrorMessage = "指定された部署IDは存在しません。";
                SetDepartmentOptions();
                return View(employee);
            }

            DbAccsess.AddEmployee(employee);
            return RedirectToAction("Index");
        }

        SetDepartmentOptions();
        return View(employee);
    }

    private void SetDepartmentOptions()
    {
        ViewBag.Departments = DbAccsess.GetDepartments()
            .Select(department => new SelectListItem
            {
                Value = department.DepartmentId.ToString(),
                Text = department.DepartmentName
            })
            .ToList();
    }
}
    
