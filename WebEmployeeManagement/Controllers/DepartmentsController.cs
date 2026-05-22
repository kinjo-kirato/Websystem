using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using WebEmployeeManagement.Infrastructures.Context;
using WebEmployeeManagement.Infrastructures.Entities;
using WebEmployeeManagement.Applications.Services;

namespace WebEmployeeManagement.Controllers;

public class DepartmentsController : Controller
{
    private readonly DepartmentService _context;

    public DepartmentsController(DepartmentService context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var departmentList = _context.GetAll();
        return View(departmentList);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Department department)
    {        if (ModelState.IsValid)
        {            
            _context.Create(department);
            return RedirectToAction("Index");
        }
        return View(department);    
    }
    
}