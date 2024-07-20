using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Application.ViewModels;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.EntityFramework;

namespace StudentExchangeInfo.UI.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqReadRepository faqReadRepository;
        private readonly IFaqCategoryReadRepository faqCategoryReadRepository;
        public FaqController(IFaqReadRepository faqReadRepository, IFaqCategoryReadRepository faqCategoryReadRepository)
        {                                                                              
            this.faqReadRepository=faqReadRepository;
            this.faqCategoryReadRepository=faqCategoryReadRepository;
        }

        #region Index
        public IActionResult Index(int catId=1)
        {
            FaqVM faqVM = new FaqVM
            {
                FaqCategories = faqCategoryReadRepository.GetAll(x => x.Status),
                Faqs = faqReadRepository.GetAll(x => x.Status && x.CategoryId==catId)
            };
            return View(faqVM);
        }
        #endregion
    }
}
