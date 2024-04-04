using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IExchangeProgramWriteRepository : IWriteRepository<ExchangeProgram>
    {
        void Activity(ExchangeProgram exchangeProgram);
    }
}
