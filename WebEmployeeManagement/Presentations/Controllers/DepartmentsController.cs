using Microsoft.AspNetCore.Mvc;
using WebEmployeeManagement.Infrastructures.Entities;
using WebEmployeeManagement.Infrastructures.DataAccess;

namespace WebEmployeeManagement.Presentations.Controllers;

public class DepartmentsController : Controller
{
    public IActionResult Index()
    {
        return View(DbAccsess.GetDepartments());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        if (ModelState.IsValid)
        {
            DbAccsess.AddDepartment(department);
            return RedirectToAction("Index");
        }

        return View(department);
    }
}
