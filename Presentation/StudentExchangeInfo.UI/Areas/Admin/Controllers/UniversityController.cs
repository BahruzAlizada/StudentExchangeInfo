using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UniversityController : Controller
    {
        private readonly IUniversityReadRepository universityReadRepository;
        private readonly IUniversityWriteRepository universityWriteRepository;
        public UniversityController(IUniversityReadRepository universityReadRepository, IUniversityWriteRepository universityWriteRepository)
        {
            this.universityReadRepository = universityReadRepository;
            this.universityWriteRepository = universityWriteRepository;
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

            dbUni.Id = uni.Id;
            dbUni.Status = uni.Status;
            dbUni.Created = uni.Created;
            dbUni.Name = uni.Name;

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
