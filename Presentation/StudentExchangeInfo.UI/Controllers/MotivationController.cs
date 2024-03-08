using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Controllers
{
    public class MotivationController : Controller
    {
        private readonly IMotivationReadRepository motivationReadRepository;
        public MotivationController(IMotivationReadRepository motivationReadRepository)
        {
            this.motivationReadRepository = motivationReadRepository;
        }

        public IActionResult Index()
        {
            Motivation motivation = motivationReadRepository.Get();
            return View(motivation);
        }
    }
}
