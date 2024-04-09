using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class FaqReadRepository : ReadRepository<Faq>, IFaqReadRepository
    {
        public async Task<List<Faq>> GetActiveFaqsTakeAsync(int take)
        {
            using var context = new Context();

            List<Faq> faqs = await context.Faqs.Where(x => x.Status).Take(take).ToListAsync();
            return faqs;
        }
    }
}
