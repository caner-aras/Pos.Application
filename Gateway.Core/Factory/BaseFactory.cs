using Application.Viewmodels;
using Domain;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gateway.Core.Factory
{
    public abstract class BaseFactory
    {
        protected IContainer Container;
        protected readonly IConfiguration _Config;
        protected readonly IMemoryCache _Cache;
        public readonly ITransactionService transactionService;

        protected BaseFactory(IMemoryCache Cache, ITransactionService transaction)
        {
            this._Cache = Cache;
            transactionService = transaction;
        }

        protected AuthorizationRequest Begin(AuthorizationRequest request)
        {
            transactionService.Add(new Transactions()
            {
                Amount = request.TotalAmount,
                OrderId = request.OrderNumber,
                ProcessType = (int)request.ProcessType,
                RequestMode = (int)request.RequestMode,
                RateId = request.Rate.Id,
            });

            return request;
        }

        protected Response<TransactionResult> End(Response<TransactionResult> request)
        {
            var transaction = transactionService.GetTransaction(request.Data.OrderId);

            transactionService.Add(new Transactions()
            {
                ParentId = transaction.Data.Id,
                Approved = request.Data.Success,
                ConfirmationCode = request.Data.TransactionId,
                Message = string.Join(",", request.ErrorMessage)
            });

            return new Response<TransactionResult>()
            {

            };
        }
    }
}
