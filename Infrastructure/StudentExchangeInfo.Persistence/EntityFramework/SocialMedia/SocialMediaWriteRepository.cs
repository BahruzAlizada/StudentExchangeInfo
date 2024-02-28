using StudentExchangeInfo.Application.Abstract;
using StudentExchangeInfo.Domain.Entities;
using StudentExchangeInfo.Persistence.Concrete;
using StudentExchangeInfo.Persistence.Repositories;

namespace StudentExchangeInfo.Persistence.EntityFramework
{
    public class SocialMediaWriteRepository : WriteRepository<SocialMedia>, ISocialMediaWriteRepository
    {
        public void Activity(SocialMedia socialMedia)
        {
            using var context = new Context();

            if (socialMedia.Status)
                socialMedia.Status = false;
            else
                socialMedia.Status = true;

            context.SocialMedias.Update(socialMedia);
            context.SaveChanges();
        }
    }
}
