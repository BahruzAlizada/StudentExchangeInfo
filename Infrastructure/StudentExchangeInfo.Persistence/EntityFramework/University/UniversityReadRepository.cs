using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;
using System.Net.Http.Headers;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class UniversityReadRepository : ReadRepository<University>, IUniversityReadRepository
    {
        private readonly IMemoryCache memoryCache;
        public UniversityReadRepository(IMemoryCache memoryCache)
        {
           this.memoryCache = memoryCache;
        }

        public async Task<University> FindUniversityByNameAsync(string name)
        {
            using var context = new Context();

            University university = await context.Universities.FirstOrDefaultAsync(x=>x.Name==name);
            return university;
        }

        public async Task<int> FindUniversityStudentCountAsync(string name)
        {
            using var context = new Context();

            University university = await context.Universities.Include(x=>x.Users).FirstOrDefaultAsync(x => x.Name == name);
            int count = university.Users.Where(x=>x.Status).Count();
            return count;
        }

        public async Task<List<University>> GetActiveCachingUniversitiesAsync()
        {
            using var context = new Context();

            const string cachedKey = "universities";
            List<University> universities;

            if (!memoryCache.TryGetValue(cachedKey, out universities))
            {
                universities = await context.Universities.Select(x => new University { Id = x.Id, Name = x.Name }).
                    OrderBy(x => x.Name).ToListAsync();

                memoryCache.Set(cachedKey, universities, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(4),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(12),
                    Priority = CacheItemPriority.High
                });
            }
            return universities;
        }

		public async Task<List<University>> GetActiveForRegisteredUniversitiesAsync()
		{
            using var context = new Context();

            List<University> universities = await context.Universities.Where(x=>!x.IsRegistred).OrderBy(x=>x.Name).ToListAsync();
            return universities;
		}

        public async Task<List<University>> GetActiveRegisteredTakedUniversitiesAsync(int take)
        {
            using var context = new Context();

            List<University> universities = await context.Universities.Where(x=>x.IsRegistred && x.Status).OrderByDescending(x=>x.Id).
                Take(take).ToListAsync();
            return universities;
        }

        public async Task<List<University>> GetActiveUniversitiesAsync()
        {
            using var context = new Context();

            List<University> universities = await context.Universities.OrderBy(x => x.Name).ToListAsync();
            return universities;
        }

        public async Task<List<University>> GetUniversitiesWithPageCountAsync(int take, int page)
        {
            using var context = new Context();

            List<University> universities = await context.Universities.OrderByDescending(x=>x.Id).
                Skip((page-1) * take).Take(take).ToListAsync();
            return universities;
        }

        public async Task<double> UniversitiesPageCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Universities.CountAsync() / take);
            return pageCount;
        }
    }
}
