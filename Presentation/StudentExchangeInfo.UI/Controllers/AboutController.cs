using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutReadRepository aboutReadRepository;
        public AboutController(IAboutReadRepository aboutReadRepository)
        {
            this.aboutReadRepository = aboutReadRepository;
        }

        #region Index
        public IActionResult Index()
        {
            About about = aboutReadRepository.Get();
            return View(about);
        }
        #endregion
    }
}
