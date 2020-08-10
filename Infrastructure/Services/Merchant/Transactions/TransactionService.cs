using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace Infrastructure.Service.Merchant.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepositories transactionRepositories;


        public TransactionService(ITransactionRepositories _uow)
        {
            transactionRepositories = _uow;
        }

        public Response<IEnumerable<Domain.Entities.Transactions>> GetTransactions(int transactionId)
        {
            var obj = transactionRepositories.GetTransactions(transactionId);

            throw new NotImplementedException();
        }
    }
}
