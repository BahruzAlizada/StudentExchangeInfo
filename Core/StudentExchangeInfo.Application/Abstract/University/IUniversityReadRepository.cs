using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IUniversityReadRepository : IReadRepository<University>
    {
        Task<List<University>> GetActiveCachingUniversitiesAsync();
        Task<List<University>> GetActiveUniversitiesAsync();
        Task<List<University>> GetActiveForRegisteredUniversitiesAsync();


        Task<University> FindUniversityByNameAsync(string name);
        Task<int> FindUniversityStudentCountAsync(string name);

        Task<List<University>> GetUniversitiesWithPageCountAsync(int take, int page);
        Task<double> UniversitiesPageCountAsync(double take);
    }
}
