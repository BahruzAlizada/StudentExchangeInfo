using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IPostReadRepository : IReadRepository<Post>
    {
        Task<double> PostPageCountAsync(double take, int userId);
        Task<List<Post>> GetPostsWithPageAsync(int userId,int take, int page);
    }
}
