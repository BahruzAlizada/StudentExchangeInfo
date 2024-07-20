
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.ViewModels
{
    public class FaqVM
    {
        public List<FaqCategory> FaqCategories { get; set; }
        public List<Faq> Faqs { get; set; }
    }
}
