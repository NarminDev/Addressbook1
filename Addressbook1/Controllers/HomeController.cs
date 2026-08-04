using Microsoft.AspNetCore.Mvc;

namespace Addressbook1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
