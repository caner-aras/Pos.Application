using Application;
using Gateway.Domain.ViewModel.Gate;
using Microsoft.AspNetCore.Http;

namespace Gateway.Core.Providers
{
    public interface IPaymentProvider
    {
        Response<TheedResult> GetPaymentParameters(AuthorizationRequest request);

        Response<TransactionResult> GetPaymentResult(IFormCollection form);


        /// <summary>
        /// Sat��
        /// </summary>
        /// <returns></returns>
        Response<TransactionResult> Sales(AuthorizationRequest request);

        /// <summary>
        /// Geri �ade
        /// </summary>
        /// <returns></returns>
        Response<TransactionResult> Refund(AuthorizationRequest request);

        /// <summary>
        /// �ptal
        /// </summary>
        /// <param name="RetrefNum"></param>
        /// <returns></returns>
        Response<TransactionResult> Cancel(AuthorizationRequest request);
    }
}