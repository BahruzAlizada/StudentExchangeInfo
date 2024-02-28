using Microsoft.AspNetCore.Mvc;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.EntityFramework;

namespace StudentExchangeInfo.UI.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqReadRepository faqReadRepository;
        public FaqController(IFaqReadRepository faqReadRepository)
        {
             this.faqReadRepository=faqReadRepository;
        }

        #region Index
        public IActionResult Index()
        {
            List<Faq> faqs = faqReadRepository.GetAll(x => x.Status);
            return View(faqs);
        }
        #endregion
    }
}
