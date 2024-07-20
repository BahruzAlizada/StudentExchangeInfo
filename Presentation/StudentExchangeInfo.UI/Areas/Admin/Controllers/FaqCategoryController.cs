using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,ContactManager")]
    public class FaqCategoryController : Controller
    {
        private readonly IFaqCategoryReadRepository faqCategoryReadRepository;
        private readonly IFaqCategoryWriteRepository faqCategoryWriteRepository;
        public FaqCategoryController(IFaqCategoryReadRepository faqCategoryReadRepository, IFaqCategoryWriteRepository faqCategoryWriteRepository)
        {
            this.faqCategoryReadRepository = faqCategoryReadRepository;
            this.faqCategoryWriteRepository = faqCategoryWriteRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<FaqCategory> faqCategories = faqCategoryReadRepository.GetAll();
            return View(faqCategories);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(FaqCategory faqCategory)
        {
            #region exist
            bool isExist = faqCategoryReadRepository.GetAll().Any(x => x.Name == faqCategory.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adlı obyekt zatən mövcuddur");
                return View();
            }
            #endregion

            faqCategoryWriteRepository.Add(faqCategory);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            FaqCategory faqCategory = faqCategoryReadRepository.Get(x => x.Id == id);
            if (faqCategory == null) return BadRequest();

            return View(faqCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, FaqCategory newFaqCategory)
        {
            if (id == null) return NotFound();
            FaqCategory faqCategory = faqCategoryReadRepository.Get(x => x.Id == id);
            if (faqCategory == null) return BadRequest();

            #region exist
            bool isExist = faqCategoryReadRepository.GetAll().Any(x => x.Name == faqCategory.Name && x.Id!=newFaqCategory.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adlı obyekt zatən mövcuddur");
                return View();
            }
            #endregion

            faqCategory.Name = newFaqCategory.Name;
            faqCategoryWriteRepository.Update(faqCategory);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int? id)
        {
            if (id == null) return NotFound();
            FaqCategory faqCategory = faqCategoryReadRepository.Get(x => x.Id == id);
            if (faqCategory == null) return BadRequest();

            faqCategoryWriteRepository.Activity(faqCategory);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
