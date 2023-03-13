using Microsoft.AspNetCore.Mvc;

namespace BirdsSweden300.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View("Start");
    }
}
