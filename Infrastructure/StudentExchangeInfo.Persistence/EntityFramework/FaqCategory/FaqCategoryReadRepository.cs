using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class FaqCategoryReadRepository : ReadRepository<FaqCategory>,IFaqCategoryReadRepository
    {
    }
}
