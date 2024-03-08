using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class PrerequisiteReadRepository : ReadRepository<Prerequisite>,IPrerequisiteReadRepository
    {
    }
}
