using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;

namespace WebEmployeeManagement.Presentations.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
}
