using Microsoft.AspNetCore.Mvc;

namespace StudentExchangeInfo.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
