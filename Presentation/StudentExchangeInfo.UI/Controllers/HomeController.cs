using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.UI.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace StudentExchangeInfo.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderReadRepository sliderReadRepository;
        private readonly IUniversityReadRepository universityReadRepository;
        private readonly IFaqReadRepository faqReadRepository;
        private readonly ISubscribeReadRepository subscribeReadRepository;
        private readonly ISubscribeWriteRepository subscribeWriteRepository;

        public HomeController(ISliderReadRepository sliderReadRepository, IUniversityReadRepository universityReadRepository,
            IFaqReadRepository faqReadRepository, ISubscribeReadRepository subscribeReadRepository, ISubscribeWriteRepository subscribeWriteRepository)
        {
            this.sliderReadRepository = sliderReadRepository;
            this.universityReadRepository = universityReadRepository;
            this.faqReadRepository = faqReadRepository;
            this.subscribeReadRepository = subscribeReadRepository;
            this.subscribeWriteRepository = subscribeWriteRepository;
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

        #region Subscribe
        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(Subscribe subscribe)
        {
            bool result = subscribeReadRepository.GetAll().Any(x => x.Email == subscribe.Email);
            if (result)
            {
                ModelState.AddModelError("email", "Siz artıq abunə olmusunuz");
                return View();
            }

            await subscribeWriteRepository.AddAsync(subscribe);
            return RedirectToAction("Index", "Home");
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
