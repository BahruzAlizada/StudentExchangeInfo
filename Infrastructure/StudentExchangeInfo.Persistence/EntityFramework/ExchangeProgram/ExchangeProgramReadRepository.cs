using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class ExchangeProgramReadRepository : ReadRepository<ExchangeProgram>, IExchangeProgramReadRepository
    {
        public async Task<List<ExchangeProgram>> GetActiveExchangeProgramsByUniversityAsync(int uniId)
        {
            using var context = new Context();

            List<ExchangeProgram> exchangePrograms = await context.ExchangePrograms.Include(x=>x.AppUser).
                Where(x => x.Status && x.AppUserId == uniId).ToListAsync();
            return exchangePrograms;
        }


        public async Task<List<ExchangeProgram>> GetExchangeProgramsByUniversityAsync(int id)
        {
            using var context = new Context();

            List<ExchangeProgram> exchangePrograms = await context.ExchangePrograms.Include(x => x.AppUser).
                Where(x => x.AppUserId == id).OrderByDescending(x => x.Id).ToListAsync();
            return exchangePrograms;
        }

        public async Task<ExchangeProgram> GetExchangeProgramWithUserAsync(int? id)
        {
            using var context = new Context();

            ExchangeProgram exchangeProgram = await context.ExchangePrograms.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            return exchangeProgram;
        }
    }
}
