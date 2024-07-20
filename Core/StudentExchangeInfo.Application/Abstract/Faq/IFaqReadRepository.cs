using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IFaqReadRepository : IReadRepository<Faq>
    {
        Task<List<Faq>> GetActiveFaqsTakeAsync(int take);
        Task<List<Faq>> GetFaqsAsync();
    }
}
