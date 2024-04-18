using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Infrastructure.Helpers;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class SliderController : Controller
    {
        private readonly ISliderReadRepository sliderReadRepository;
        private readonly ISliderWriteRepository sliderWriteRepository;
        private readonly IWebHostEnvironment env;
        public SliderController(ISliderReadRepository sliderReadRepository, ISliderWriteRepository sliderWriteRepository,
            IWebHostEnvironment env)
        {
            this.sliderReadRepository = sliderReadRepository;
            this.sliderWriteRepository = sliderWriteRepository;
            this.env = env;
        }

        #region Index
        public IActionResult Index()
        {
            List<Slider> sliders = sliderReadRepository.GetAll();
            return View(sliders);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Slider slider)
        {
			#region Image
			if (slider.Photo is null)
			{
				ModelState.AddModelError("Photo", "Bu xana boş ola bilməz");
				return View();
			}
			if (!slider.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayl");
				return View();
			}
			if (slider.Photo.IsOlder256Kb())
			{
				ModelState.AddModelError("Photo", "Maksimum 256Kb");
				return View();
			}
			string folder = Path.Combine(env.WebRootPath, "assets", "img", "slider");
			slider.Image = await slider.Photo.SaveFileAsync(folder);
            #endregion

            await sliderWriteRepository.AddAsync(slider);
			return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Slider? dbSlider = sliderReadRepository.Get(x => x.Id == id);
            if (dbSlider == null) return BadRequest();

            return View(dbSlider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

		public async Task<IActionResult> Update(int? id, Slider slider)
		{
			if (id == null) return NotFound();
			Slider? dbSlider = sliderReadRepository.Get(x => x.Id == id);
			if (dbSlider == null) return BadRequest();

			#region Image
			if (slider.Photo is not null)
			{
				if (!slider.Photo.IsImage())
				{
					ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayllar");
					return View();
				}
				if (slider.Photo.IsOlder256Kb())
				{
					ModelState.AddModelError("Photo", "Maksimum 256Kb");
					return View();
				}
				string folder = Path.Combine(env.WebRootPath, "assets", "img", "slider");
				slider.Image = await slider.Photo.SaveFileAsync(folder);
				string path = Path.Combine(env.WebRootPath, folder, dbSlider.Image);
				if (System.IO.File.Exists(path))
					System.IO.File.Delete(path);				
			}
			else
				slider.Image = dbSlider.Image;
			#endregion

			dbSlider.Id = slider.Id;
            dbSlider.Status = slider.Status;
            dbSlider.Title = slider.Title;
            dbSlider.SubTitle = slider.SubTitle;

            await sliderWriteRepository.UpdateAsync(slider);
            return RedirectToAction("Index");
		}
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
