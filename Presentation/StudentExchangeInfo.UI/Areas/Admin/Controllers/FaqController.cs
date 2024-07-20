using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin,ContactManager")]
	public class FaqController : Controller
    {
        private readonly IFaqReadRepository faqReadRepository;
        private readonly IFaqWriteRepository faqWriteRepository;
        private readonly IFaqCategoryReadRepository faqCategoryReadRepository;
        public FaqController(IFaqReadRepository faqReadRepository, IFaqWriteRepository faqWriteRepository,
            IFaqCategoryReadRepository faqCategoryReadRepository)
        {
            this.faqReadRepository = faqReadRepository;
            this.faqWriteRepository = faqWriteRepository;
            this.faqCategoryReadRepository = faqCategoryReadRepository;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Faq> faqs = await faqReadRepository.GetFaqsAsync();
            return View(faqs);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewBag.Categories = faqCategoryReadRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Faq faq, int catId)
        {
            ViewBag.Categories = faqCategoryReadRepository.GetAll();
            faq.CategoryId = catId;

            faqWriteRepository.Add(faq);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Faq? dbFaq = faqReadRepository.Get(x => x.Id == id);
            if (dbFaq == null) return BadRequest();

            ViewBag.Categories = faqCategoryReadRepository.GetAll();

            return View(dbFaq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id,Faq faq, int catId)
        {
            if (id == null) return NotFound();
            Faq? dbFaq = faqReadRepository.Get(x => x.Id == id);
            if (dbFaq == null) return BadRequest();

            ViewBag.Categories = faqCategoryReadRepository.GetAll();

            dbFaq.Answer = faq.Answer;
            dbFaq.Quetsion = faq.Quetsion;
            dbFaq.CategoryId = catId;

            faqWriteRepository.Update(dbFaq);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Faq? faq = faqReadRepository.Get(x=>x.Id == id);
            if (faq == null) return BadRequest();

            faqWriteRepository.Delete(faq);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public IActionResult Activity(int? id)
        {
            if (id == null) return NotFound();
            Faq? faq = faqReadRepository.Get(x => x.Id == id);
            if (faq == null) return BadRequest();

            faqWriteRepository.Activity(faq);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
