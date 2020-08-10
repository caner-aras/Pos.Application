using AutoMapper;
using Gateway.Application.Services.Merchant;
using Gateway.Application.Services.Rate;
using Application;
using Application.Constants;
using Application.Enum;
using Application.Exceptions;
using Application.Helpers.BinHelper;
using Application.Helpers.FormGateway;
using Application.Helpers.NetworkGateway;
using Application.Helpers.StringGateway;
using Gateway.Domain.ViewModel.BinLookUp;
using Gateway.Domain.ViewModel.Gate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Gateway.Core.Providers;

namespace Gateway.Core.Factory
{
    public class PaymentProviderFactory : BaseFactory, IPaymentProviderFactory
    {
        private readonly IBinLookUpService _binService;
        private readonly IClientIpService clientIpHelper;
        private readonly IGatewayService gatewayService;
        private readonly IRateService rateService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PaymentProviderFactory(
            IMemoryCache Cache,
            IHttpContextAccessor httpContextAccessor,
            IBinLookUpService binService,
            IClientIpService ClientIpHelper,
            ITransactionService transaction,
            IGatewayService GatewayService, IRateService IRateService) :
            base(Cache, transaction)
        {
            _binService = binService;
            clientIpHelper = ClientIpHelper;
            gatewayService = GatewayService;
            rateService = IRateService;
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthorizationRequest Init(AuthorizationRequest request)
        {

            if (string.IsNullOrEmpty(request.OrderNumber))
            {
                request.OrderNumber = RandomText.GenerateRandomText();
            }

            request.CustomerIpAddress = clientIpHelper.GetClientIp().Data.Ip;

            if (request.Installment == 0)
                throw new BusinessException(ResponseCode.Error);

            request.Rate = rateService.GetRate(request.Installment).Data;

            if (request.Rate == null)
                throw new BusinessException(ResponseCode.Error);

            if (request.Rate.Gateway == null)
                throw new BusinessException(ResponseCode.Error);
            return Begin(request);
        }

        public Response<TheedResult> Create(AuthorizationRequest model)
        {
            if (model == null)
            {
                throw new BusinessException(ResponseCode.Error);
            }

            var binCheck = _binService.Find(model.CardNumber.Substring(0, 6));

            if (binCheck != null && binCheck.Data != null)
            {
                model = Init(model);

                var GetPaymentProviderResult = GetPaymentProvider(model).GetPaymentParameters(model);

                if (GetPaymentProviderResult.IsSuccessfull)
                {
                    _Cache.Set($"request-{model.OrderNumber}", JsonSerializer.SerializeToUtf8Bytes(model), new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                        Priority = CacheItemPriority.Normal
                    });

                    Guid formId = Guid.NewGuid();
                    _Cache.Set(formId, CreateForm(GetPaymentProviderResult.Data.Parameters, GetPaymentProviderResult.Data.PaymentUrl, true), new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                        Priority = CacheItemPriority.Normal
                    });

                    return new Response<TheedResult>()
                    {
                        Data = new TheedResult()
                        {
                            PaymentUrl = new Uri($"{_httpContextAccessor.HttpContext.Request.Host.Value}/auth/{formId}"),
                        }
                    };
                }
                else
                {
                    return GetPaymentProviderResult;
                }
            }
            else
            {
                throw new BusinessException(ResponseCode.Error);
            }
        }

        public Response<TransactionResult> Sales(AuthorizationRequest paymentRequest)
        {
            try
            {
                return End(GetPaymentProvider(paymentRequest).Sales(Init(paymentRequest)));
            }
            catch (Exception Ex)
            {
                return Cancel(paymentRequest);
            }
        }

        public Response<TransactionResult> Refund(AuthorizationRequest paymentRequest)
        {
            return End(GetPaymentProvider(paymentRequest).Refund(Init(paymentRequest)));
        }

        public Response<TransactionResult> Cancel(AuthorizationRequest paymentRequest)
        {
            var gettransaction = transactionService.GetTransaction(paymentRequest.OrderNumber);
            if (gettransaction != null && gettransaction.Data != null)
            {
                paymentRequest.Rate.Gateway.Bank = (Banks)gettransaction.Data.Rates.Gateway.BankId;
                return End(GetPaymentProvider(paymentRequest).Cancel(Init(paymentRequest)));
            }
            else
            {
                throw new BusinessException(ResponseCode.Error);
            }
        }

        public Response<TransactionResult> Result(IFormCollection form, string OrderNumber)
        {
            try
            {
                if (!_Cache.TryGetValue($"request-{OrderNumber}", out byte[] paymentInfo))
                    throw new BusinessException(ResponseCode.Error);

                _Cache.Remove($"request-{OrderNumber}");
                var request = JsonSerializer.Deserialize<AuthorizationRequest>(paymentInfo);
                var response = GetPaymentProvider(request).GetPaymentResult(form);
                response.Data.ReturnUrl = request.Rate.Gateway.Client.CallBackAddress;

                return response;
            }
            catch (Exception)
            {
                return Cancel(new AuthorizationRequest()
                {
                    OrderNumber = OrderNumber,
                });
            }
        }

        public IPaymentProvider GetPaymentProvider(AuthorizationRequest request)
        {
            var _bincheck = _binService.Find(request.CardNumber.Substring(0, 6));

            if (_bincheck != null && _bincheck.Data != null)
            {
                switch (StringHelpers.GetValueFromDescription<Banks>(_bincheck.Data.Bank.Name))
                {
                    case Banks.Akbank:
                    case Banks.IsBankasi:
                    case Banks.HalkBank:
                    case Banks.TurkEkonomiBankasi:
                    case Banks.DenizBank:
                    case Banks.IngBank:
                    case Banks.ZiraatBankasi:
                    case Banks.KuveytTurk:
                    case Banks.QnbFinansbank:
                        return new AssecoPaymentProvider(_httpContextAccessor);
                    case Banks.YapiVeKrediBankasi:
                        return new YapikrediPaymentProvider(_Config);
                    case Banks.GarantiBankasi:
                        return new GarantiPaymentProvider(_httpContextAccessor);
                    case Banks.Vakifbank:
                        return new VakifbankPaymentProvider(_Config);
                    default:
                        return new GarantiPaymentProvider(_httpContextAccessor);
                }
            }
            else
            {
                return new GarantiPaymentProvider(_httpContextAccessor);
            }
        }

        public string CreateForm(IDictionary<string, object> parameters, Uri paymentUrl, bool appendFormSubmitScript)
        {
            if (parameters == null || !parameters.Any())
                throw new ArgumentNullException(nameof(parameters));

            if (paymentUrl == null)
                throw new ArgumentNullException(nameof(paymentUrl));

            var formId = "PaymentForm";
            var formBuilder = new StringBuilder();
            formBuilder.Append($"<form id=\"{formId}\" name=\"{formId}\" action=\"{paymentUrl}\" role=\"form\" method=\"POST\">");
            foreach (var parameter in parameters)
            {
                formBuilder.Append($"<input type=\"hidden\" name=\"{parameter.Key}\" value=\"{parameter.Value}\">");
            }
            formBuilder.Append("</form>");

            if (appendFormSubmitScript)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.Append("<script>");
                scriptBuilder.Append($"document.{formId}.submit();");
                scriptBuilder.Append("</script>");
                formBuilder.Append(scriptBuilder.ToString());
            }

            return formBuilder.ToString();
        }
    }
}
