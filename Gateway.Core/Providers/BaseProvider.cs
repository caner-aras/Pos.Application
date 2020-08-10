using Application;
using Application.Helpers.BinHelper;
using Application.Helpers.NetworkGateway;
using Gateway.Domain.Entity;
using Gateway.Domain.ViewModel.Gate;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;

namespace Gateway.Core.Providers
{
    public class BaseProvider
    {
        public IConfiguration Configuration { get; set; }

        #region Helpers
        public readonly IBinLookUpService BinLookup;
        public readonly IClientIpService ClientIpHelper;
        #endregion

        public BaseProvider()
        {
            BinLookup = new BinLookUpService();
            ClientIpHelper = new ClientIpService(Configuration);
        }

        public Response<Log> Begin(AuthorizationRequest request)
        {
            //ResponseInfo<TransactionEntity> transaction = TransactionsRepository.Order(request.OrderNumber);
            //TransactionEntity parent = new TransactionEntity();
            //if (transaction != null && transaction.IsSuccessfull && transaction.Data != null)
            //{
            //    parent = transaction.Data;
            //}

            //ResponseInfo<LogEntity> beginRequest = LogRepository.Add(new LogEntity()
            //{
            //    Transaction = new TransactionEntity()
            //    {
            //        Amount = request.TotalAmount,
            //        BankId = request.Bank,
            //        Installment = request.Installment,
            //        OrderId = request.OrderNumber,
            //        ProcessType = request.ProcessType,
            //        RequestMode = request.RequestMode,
            //        SecurityLevel = request.SecurityLevel,
            //        Parent = parent.Id != Guid.Empty ? parent : null
            //    },
            //    Raw = JsonConvert.SerializeObject(request),
            //});

            //if (unitOfWork.SaveChanges() != 1)
            //{
            //    beginRequest.ErrorMessage = "BeginTransaction Error";
            //}
            //else
            //{
            //    return beginRequest;
            //}

            return new Response<Log>()
            {

            };
        }

        public TransactionResult End(TransactionResult request)
        {
            //ResponseInfo<TransactionEntity> transaction = TransactionsRepository.Order(request.OrderId);
            //TransactionEntity parent = new TransactionEntity();
            //if (transaction != null && transaction.IsSuccessfull && transaction.Data != null)
            //{
            //    parent = transaction.Data;
            //}

            //LogRepository.Add(new LogEntity()
            //{
            //    Transaction = new TransactionEntity()
            //    {
            //        OrderId = request.OrderId,
            //        Approved = request.Success,
            //        ConfirmationCode = request.ResponseCode,
            //        Message = request.ErrorMessage,
            //        Parent = parent.Id != Guid.Empty ? parent : null
            //    },
            //    Raw = JsonConvert.SerializeObject(request),
            //});

            return request;
        }
    }
}
