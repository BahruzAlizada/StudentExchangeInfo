using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentExchangeInfo.UI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class UniversityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
