using Microsoft.AspNetCore.Mvc;
using DepartmentList.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentList.Controllers;

public class DepartmentsController : Controller
{
    public IActionResult Index()
    {
        var departmentList = new List<Department>
        {
            new Department { DepartmentId = 10, DepartmentName = "営業部" },
            new Department { DepartmentId = 20, DepartmentName = "開発部" }
        };

        return View(departmentList);
    }
    public IActionResult Create()
    {
        return View();
    }
}