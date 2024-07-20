using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class PostReadRepository : ReadRepository<Post>, IPostReadRepository
    {
        public async Task<List<Post>> GetPostsWithPageAsync(int userId, int take, int page)
        {
            using var context = new Context();

            List<Post> posts = await context.Posts.Include(x => x.User).Where(x => x.UserId == userId).
                OrderByDescending(x=>x.Id).Skip((page - 1) * take).Take(take).ToListAsync();
            return posts;
        }

        public async Task<double> PostPageCountAsync(double take, int userId)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Posts.Where(x=>x.UserId == userId).CountAsync()/take);
            return pageCount;
        }
    }
}
