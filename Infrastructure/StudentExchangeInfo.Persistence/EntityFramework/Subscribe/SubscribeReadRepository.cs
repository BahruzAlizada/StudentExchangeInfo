using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class SubscribeReadRepository : ReadRepository<Subscribe>, ISubscribeReadRepository
    {
        public async Task<List<Subscribe>> GetSubscribesWithPagingAsync(int take, int page)
        {
            using var context = new Context();

            List<Subscribe> subscribes = await context.Subscribes.OrderByDescending(x => x.Id).
                Skip((page - 1) * take).Take(take).ToListAsync();
            return subscribes;
        }

        public async Task<double> PageCountSubscribeAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Subscribes.CountAsync() / take);
            return pageCount;
        }

    }
}
