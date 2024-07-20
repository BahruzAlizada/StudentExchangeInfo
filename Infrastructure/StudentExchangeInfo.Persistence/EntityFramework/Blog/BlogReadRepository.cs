using Azure;
using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class BlogReadRepository : ReadRepository<Blog>, IBlogReadRepository
    {
        public async Task<List<Blog>> GetActiveBlogsWithPageAsync(int take, int page)
        {
            using var context = new Context();

            List<Blog> blogs = await context.Blogs.Where(x=>x.Status).OrderByDescending(x => x.Id).Skip((page - 1) * take).Take(take).ToListAsync();
            return blogs;
        }

        public async Task<List<Blog>> GetBlogsWithPageAsync(int take, int page)
        {
            using var context = new Context();

            List<Blog> blogs = await context.Blogs.OrderByDescending(x => x.Id).Skip((page - 1) * take).Take(take).ToListAsync();
            return blogs;
        }

        public async Task<List<Blog>> GetBlogsWithTakeAsync(int take)
        {
            using var context = new Context();

            List<Blog> blogs = await context.Blogs.Where(x=>x.Status).OrderByDescending(x => x.Id).Take(take).ToListAsync();
            return blogs;
        }

        public async Task<double> PageActiveCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Blogs.Where(x=>x.Status).CountAsync() / take);
            return pageCount;
        }

        public async Task<double> PageCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Blogs.CountAsync() / take);
            return pageCount;
        }
    }
}
