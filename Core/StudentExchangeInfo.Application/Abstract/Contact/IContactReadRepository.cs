using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IContactReadRepository : IReadRepository<Contact>
    {
        Task<List<Contact>> GetContactsWithPagedAsync(int take, int page);
        Task<double> ContactPageCountAsync(double take);
    }
}
