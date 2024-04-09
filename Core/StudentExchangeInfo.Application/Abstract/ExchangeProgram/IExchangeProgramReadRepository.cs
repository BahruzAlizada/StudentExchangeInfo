using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IExchangeProgramReadRepository : IReadRepository<ExchangeProgram>
    {
        Task<List<ExchangeProgram>> GetActiveExchangeProgramsByUniversityAsync(int uniId);
        Task<List<ExchangeProgram>> GetExchangeProgramsByUniversityAsync(int id);
        Task<ExchangeProgram> GetExchangeProgramWithUserAsync(int? id);
    }
}
