using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class ExchangeProgramReadRepository : ReadRepository<ExchangeProgram>, IExchangeProgramReadRepository
    {
        public async Task<List<ExchangeProgram>> GetActiveExchangeProgramsByUniversityAsync(int id)
        {
            using var context = new Context();

            List<ExchangeProgram> exchangePrograms = await context.ExchangePrograms.Include(x=>x.AppUser).
                Where(x => x.Status && x.AppUserId == id).OrderByDescending(x => x.Id).ToListAsync();
            return exchangePrograms;
        }
    }
}
