using Microsoft.AspNetCore.Mvc;

namespace Addressbook1.Controllers
{
    public class AccountController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
