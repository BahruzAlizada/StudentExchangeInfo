﻿using StudentExchangeInfo.Application.Repositories;
using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.Abstract
{
    public interface IUniversityWriteRepository : IWriteRepository<University>
    {
        void Activity(University university);
        Task RegisterChangedUniversity(University university);

        void IsRegistred(University university);
    }
}
