using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderReadRepository sliderReadRepository;
        private readonly ISliderWriteRepository sliderWriteRepository;
        public SliderController(ISliderReadRepository sliderReadRepository, ISliderWriteRepository sliderWriteRepository)
        {
            this.sliderReadRepository = sliderReadRepository;
            this.sliderWriteRepository = sliderWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Slider> sliders = sliderReadRepository.GetAll();
            return View(sliders);
        }
        #endregion

        #region Create

        #endregion

        #region Update

        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider? slider = sliderReadRepository.Get(x=>x.Id == id);
            if(slider == null) return BadRequest();

            sliderWriteRepository.Delete(slider);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int? id)
        {
            if (id == null) return NotFound();
            Slider? slider = sliderReadRepository.Get(x => x.Id == id);
            if (slider == null) return BadRequest();

            sliderWriteRepository.Activity(slider);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
