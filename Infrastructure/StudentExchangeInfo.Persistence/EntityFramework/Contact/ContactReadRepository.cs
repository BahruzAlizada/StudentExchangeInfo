using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
    {
        public async Task<double> ContactPageCountAsync(double take)
        {
            using var context = new Context();

            double pageCount = Math.Ceiling(await context.Contacts.CountAsync() / take);
            return pageCount;
        }

        public async Task<List<Contact>> GetContactsWithPagedAsync(int take, int page)
        {
            using var context = new Context();

            List<Contact> contacts = await context.Contacts.OrderByDescending(x=>x.Id).Skip((take - 1) * page).
                Take(take).ToListAsync();
            return contacts;
        }
    }
}
