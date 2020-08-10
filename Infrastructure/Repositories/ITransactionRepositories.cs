using Common.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public interface ITransactionRepositories : IRepository<Domain.Entities.Transactions>
    {
        IEnumerable<Domain.Entities.Transactions> GetTransactions(int transactionId);
    }
}
