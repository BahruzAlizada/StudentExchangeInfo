using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class UniversityWriteRepository : WriteRepository<University>, IUniversityWriteRepository
    {
        public void Activity(University university)
        {
            using var context = new Context();

            if (university.Status)
                university.Status = false;
            else
                university.Status = true;

            context.Universities.Update(university);
            context.SaveChanges();
        }

		public async Task RegisterChangedUniversity(University university)
		{
            using var context = new Context();

            if (university.IsRegistred is false)
                university.IsRegistred = true;

            context.Universities.Update(university);
            await context.SaveChangesAsync();
		}
	}
}
