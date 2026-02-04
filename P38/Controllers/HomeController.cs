using Microsoft.AspNetCore.Mvc;

namespace P38.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
