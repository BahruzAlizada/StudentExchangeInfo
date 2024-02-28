using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface ISocialMediaWriteRepository : IWriteRepository<SocialMedia>
    {
        void Activity(SocialMedia socialMedia);
    }
}
