using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Infrastructure.Helpers;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UniversityController : Controller
    {
        private readonly IUniversityReadRepository universityReadRepository;
        private readonly IUniversityWriteRepository universityWriteRepository;
        private readonly IWebHostEnvironment env;
        public UniversityController(IUniversityReadRepository universityReadRepository, IUniversityWriteRepository universityWriteRepository
            , IWebHostEnvironment env)
        {
            this.universityReadRepository = universityReadRepository;
            this.universityWriteRepository = universityWriteRepository;
            this.env = env;
        }
        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            double take = 20;
            ViewBag.PageCount = await universityReadRepository.UniversitiesPageCountAsync(take);
            ViewBag.CurrentPage = page;

            List<University> universities = await universityReadRepository.GetUniversitiesWithPageCountAsync((int)take, page);
            return View(universities);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(University university)
        {
            #region Image
            if (university.Photo is null)
            {
                ModelState.AddModelError("Photo", "Bu xana boş ola bilməz");
                return View();
            }
            if (!university.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayl");
                return View();
            }
            if (university.Photo.IsOlder256Kb())
            {
                ModelState.AddModelError("Photo", "Maksimum 256Kb");
                return View();
            }
            string folder = Path.Combine(env.WebRootPath, "assets", "img", "university");
            university.Image = await university.Photo.SaveFileAsync(folder);
            #endregion

            await universityWriteRepository.AddAsync(university);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            University dbUni = universityReadRepository.Get(x => x.Id == id);
            if (dbUni == null) return BadRequest();

            return View(dbUni);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, University uni)
        {
            if (id == null) return NotFound();
            University dbUni = universityReadRepository.Get(x => x.Id == id);
            if (dbUni == null) return BadRequest();

            #region Image
            if (uni.Photo is not null)
            {
                if (!uni.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yalnız şəkil tipli fayllar");
                    return View();
                }
                if (uni.Photo.IsOlder256Kb())
                {
                    ModelState.AddModelError("Photo", "Maksimum 256Kb");
                    return View();
                }
                string folder = Path.Combine(env.WebRootPath, "assets", "img", "university");
                uni.Image = await uni.Photo.SaveFileAsync(folder);
                string path = Path.Combine(env.WebRootPath, folder, dbUni.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            else
                uni.Image = dbUni.Image;
            #endregion

            dbUni.Id = uni.Id;
            dbUni.Status = uni.Status;
            dbUni.Name = uni.Name;
            dbUni.IsRegistred = uni.IsRegistred;

            await universityWriteRepository.UpdateAsync(uni);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            University university = universityReadRepository.Get(x=>x.Id == id);
            if (university == null) return BadRequest();

            universityWriteRepository.Delete(university);
            return RedirectToAction("Index");
        }
        #endregion

        #region Status
        public IActionResult Activity(int? id)
        {
            if (id == null) return NotFound();
            University university = universityReadRepository.Get(x => x.Id == id);
            if (university == null) return BadRequest();

            universityWriteRepository.Activity(university);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
