using Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Gateway.Core.Providers;
using Application.Viewmodels;
using Domain;

namespace Gateway.Core.Factory
{
    public interface IPaymentProviderFactory
    {
        Response<TransactionResult> Cancel(AuthorizationRequest paymentRequest);
        Response<TheedResult> Create(AuthorizationRequest model);
        string CreateForm(IDictionary<string, object> parameters, Uri paymentUrl, bool appendFormSubmitScript);
        IPaymentProvider GetPaymentProvider(AuthorizationRequest request);
        Response<TransactionResult> Refund(AuthorizationRequest paymentRequest);
        Response<TransactionResult> Result(IFormCollection form, string OrderNumber);
        Response<TransactionResult> Sales(AuthorizationRequest paymentRequest);
    }
}