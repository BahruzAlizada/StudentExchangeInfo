using Microsoft.AspNetCore.Mvc;

namespace StudentExchangeInfo.UI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404(int? code)
        {
            return View();
        }
    }
}
