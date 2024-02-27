using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface ISliderWriteRepository : IWriteRepository<Slider>
    {
        void Activity(Slider slider);
    }
}
