using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface ISubscribeWriteRepository : IWriteRepository<Subscribe>
    {
        Task SubscribeAsync(string email);
        Task UnSubscribeAsync(string email);
    }
}
