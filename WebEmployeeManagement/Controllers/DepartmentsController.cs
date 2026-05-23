using Microsoft.AspNetCore.Mvc;
using WebEmployeeManagement.Infrastructures.Entities;
using WebEmployeeManagement.Models;

namespace WebEmployeeManagement.Controllers;

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
            if (DbAccsess.ExistsDepartmentId(department.DepartmentId))
            {
                ViewBag.ErrorMessage = "同じ部署IDが既に存在します。";
                return View(department);
            }

            DbAccsess.AddDepartment(department);
            return RedirectToAction("Index");
        }

        return View(department);
    }
}
