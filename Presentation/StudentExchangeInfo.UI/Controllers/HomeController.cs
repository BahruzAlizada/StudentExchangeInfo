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

        public HomeController(ISliderReadRepository sliderReadRepository)
        {
            this.sliderReadRepository = sliderReadRepository;
        }

        #region Index
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliderReadRepository.GetAll()
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
