using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface ISubscribeReadRepository : IReadRepository<Subscribe>
    {
        Task<List<Subscribe>> GetSubscribesWithPagingAsync(int take, int page);
        Task<double> PageCountSubscribeAsync(double take);
    }
}
