﻿using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IBlogWriteRepository : IWriteRepository<Blog>
    {
    }
}
