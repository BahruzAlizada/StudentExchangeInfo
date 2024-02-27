using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Common;
using StudentExchangeInfo.Persistence.Concrete;
using System.Linq.Expressions;

namespace StudentExchangeInfo.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {


        public T Get(Expression<Func<T, bool>> filter = null)
        {
            using var context = new Context();
            return filter == null
                ? context.Set<T>().FirstOrDefault()
                : context.Set<T>().FirstOrDefault(filter);

        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using var context = new Context();
            return filter == null
                ? context.Set<T>().ToList()
                : context.Set<T>().Where(filter).ToList();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            using var context = new Context();
            return filter == null
                ? await context.Set<T>().ToListAsync()
                : await context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            using var context = new Context();
            return filter == null
                ? await context.Set<T>().FirstOrDefaultAsync()
                : await context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public int GetCount(Expression<Func<T, bool>> expression = null)
        {
            using var context = new Context();
            return expression == null ? context.Set<T>().Count() : context.Set<T>().Count(expression);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null)
        {
            using var context = new Context();
            return await context.Set<T>().CountAsync(expression);
        }

        public IQueryable<T> Query()
        {
            using var context = new Context();
            return context.Set<T>();
        }
    }
}
