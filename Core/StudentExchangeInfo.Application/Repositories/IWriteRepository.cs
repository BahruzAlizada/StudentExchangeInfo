using StudentExchangeInfo.Domain.Common;

namespace StudentExchangeInfo.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}
