using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IBlogReadRepository : IReadRepository<Blog>
    {
        Task<double> PageCountAsync(double take);
        Task<double> PageActiveCountAsync(double take);
        Task<List<Blog>> GetBlogsWithPageAsync(int take, int page);
        Task<List<Blog>> GetActiveBlogsWithPageAsync(int take, int page);
        Task<List<Blog>> GetBlogsWithTakeAsync(int take);
    }
}
