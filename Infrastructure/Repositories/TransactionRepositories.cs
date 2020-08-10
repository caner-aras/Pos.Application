using Common;
using Domain.Entities;
using Persistence;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class TransactionRepositories : Repository<Domain.Entities.Transactions> , ITransactionRepositories
    {
        public MerchantContext merchantContext
        {
            get { return Context as MerchantContext; }
        }

        public TransactionRepositories(MerchantContext context) : base(context)
        {
        }

        public IEnumerable<Transactions> GetTransactions(int transactionId)
        {
            return merchantContext.Transactions;
        }
    }
}
