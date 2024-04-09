using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.UI.Models;
using System.Diagnostics;

namespace StudentExchangeInfo.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderReadRepository sliderReadRepository;
        private readonly IUniversityReadRepository universityReadRepository;
        private readonly IFaqReadRepository faqReadRepository;

        public HomeController(ISliderReadRepository sliderReadRepository, IUniversityReadRepository universityReadRepository,
            IFaqReadRepository faqReadRepository)
        {
            this.sliderReadRepository = sliderReadRepository;
            this.universityReadRepository = universityReadRepository;
            this.faqReadRepository = faqReadRepository;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = await sliderReadRepository.GetAllAsync(),
                Universities = await universityReadRepository.GetActiveRegisteredTakedUniversitiesAsync(3),
                Faqs = await faqReadRepository.GetActiveFaqsTakeAsync(3)
            };
            return View(homeVM);
        }
        #endregion


        #region Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
