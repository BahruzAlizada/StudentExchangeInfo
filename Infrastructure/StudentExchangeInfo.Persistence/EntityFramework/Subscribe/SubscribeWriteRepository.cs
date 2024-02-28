using Microsoft.EntityFrameworkCore;
using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class SubscribeWriteRepository : WriteRepository<Subscribe>, ISubscribeWriteRepository
    {
        public async Task SubscribeAsync(string email)
        {
            using var context = new Context();

            Subscribe subscribe = new Subscribe
            {
                Email = email
            };

            await context.Subscribes.AddAsync(subscribe);
            await context.SaveChangesAsync();
        }

        public async Task UnSubscribeAsync(string email)
        {
            using var context = new Context();

            Subscribe? subscribe = await context.Subscribes.FirstOrDefaultAsync(x=>x.Email == email);
            if (subscribe is not null)
            {
                context.Subscribes.Remove(subscribe);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
