

using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class ExchangeProgramWriteRepository : WriteRepository<ExchangeProgram>, IExchangeProgramWriteRepository
    {
        public void Activity(ExchangeProgram exchangeProgram)
        {
            using var context = new Context();

            if (exchangeProgram.Status)
                exchangeProgram.Status = false;
            else
                exchangeProgram.Status = true;

            context.ExchangePrograms.Update(exchangeProgram);
            context.SaveChanges();
        }
    }
}
