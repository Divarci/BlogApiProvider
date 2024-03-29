﻿using CoreLayer.BaseEntity;
using Microsoft.EntityFrameworkCore.Storage;
using RepositoryLayer.Repository.Abstract;

namespace RepositoryLayer.UnitOfWOrk.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : BaseEntity, new();
        void Commit();
        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
