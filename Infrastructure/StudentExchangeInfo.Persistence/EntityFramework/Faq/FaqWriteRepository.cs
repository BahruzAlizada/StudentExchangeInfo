using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class FaqWriteRepository : WriteRepository<Faq>, IFaqWriteRepository
    {
        public void Activity(Faq faq)
        {
            using var context = new Context();

            if (faq.Status)
                faq.Status = false;
            else
                faq.Status = true;

            context.Faqs.Update(faq);
            context.SaveChanges();
        }
    }
}
