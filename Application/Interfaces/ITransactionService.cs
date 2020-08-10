using Domain;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Response<IEnumerable<Domain.Entities.Transactions>> GetTransactions(int transactionId);
    }
}
