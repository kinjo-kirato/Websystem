using Microsoft.AspNetCore.Mvc;

namespace EmployeeList.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
