using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class SliderWriteRepository : WriteRepository<Slider>, ISliderWriteRepository
    {
        public void Activity(Slider slider)
        {
            using var context = new Context();

            if (slider.Status)
                slider.Status = false;
            else
                slider.Status = true;

            context.SaveChanges();
        }
    }
}
