using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    internal class SliderReadRepository : ReadRepository<Slider>,ISliderReadRepository
    {
    }
}
