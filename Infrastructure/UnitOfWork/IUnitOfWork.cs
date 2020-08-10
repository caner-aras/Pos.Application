using Infrastructure.Repositories;
using System;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepositories Transaction { get; }
        int Complete();
    }
}
