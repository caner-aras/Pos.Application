using Application;
using Application.Enums;
using Application.Helpers.XmlHelper;
using Gateway.Domain.ViewModel.Gate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Gateway.Core.Models.Asseco;

namespace Gateway.Core.Providers
{
    public class AssecoPaymentProvider : IPaymentProvider
    {
        private readonly IXmlSender<Cc5Request> XmlSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AssecoPaymentProvider(IHttpContextAccessor httpContextAccessor)
        {
            XmlSender = new XmlSender<Cc5Request>();
            _httpContextAccessor = httpContextAccessor;
        }

        public Response<TheedResult> GetPaymentParameters(AuthorizationRequest request)
        {
            string processType = "Auth";

            string ClientId =
                request.Rate.Gateway.Merchants.FirstOrDefault(k=> k.Key == "ClientId")?.Value;
            string StoreKey =
                 request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "StoreKey")?.Value;
            string StoreType =
                request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "StoreType")?.Value;

            string callBack = 
                $"{(string.IsNullOrEmpty(request.Rate.Gateway.Client.CallBackAddress) ? _httpContextAccessor.HttpContext.Request.Host.Value : request.Rate.Gateway.Client.CallBackAddress)}/api/callback/{request.OrderNumber}";

            string RandonKey = DateTime.Now.ToString();

            var parameterResult = new TheedResult();
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "clientid", ClientId },
                    { "amount", request.TotalAmount },
                    { "oid", request.OrderNumber },
                    { "orderid", request.OrderNumber },
                    { "okUrl", callBack },
                    { "failUrl", callBack },
                    { "islemtipi", processType },
                    { "taksit", request.Installment },
                    { "rnd", RandonKey }
                };

                string HashString =
                    ClientId + request.OrderNumber + request.TotalAmount + callBack + callBack +
                    processType + request.Installment + RandonKey + StoreKey;

                SHA1 ShaCrypto = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                var Hash = Convert.ToBase64String(ShaCrypto.ComputeHash(Encoding.UTF8.GetBytes(HashString)));


                parameters.Add("hash", Hash);
                parameters.Add("currency", request.CurrencyIsoCode);
                //TL ISO code | EURO 978 | Dolar 840

                string cardNumber = request.CardNumber.Replace("-", string.Empty);
                cardNumber = cardNumber.Replace(" ", string.Empty).Trim();
                parameters.Add("pan", cardNumber);

                parameters.Add("cardHolderName", request.CardHolderName);
                parameters.Add("Ecom_Payment_Card_ExpDate_Month", request.ExpireMonth);
                parameters.Add("Ecom_Payment_Card_ExpDate_Year", request.ExpireYear);
                parameters.Add("cv2", request.CvvCode);
                parameters.Add("cardType", cardNumber.StartsWith("4") ? "1" : "2");
                //kart tipi visa 1 | master 2 | amex 3
                parameters.Add("storetype", StoreType);
                parameters.Add("lang", "tr");

                parameterResult.Parameters = parameters;
                parameterResult.PaymentUrl = new Uri(request.Rate.Gateway.MerchantUri.GatewayUri);

                //Begin(request);
            }
            catch (Exception ex)
            {
                return new Response<TheedResult>()
                {
                    ErrorMessage = ex.ToString()
                };
            }

            return new Response<TheedResult>()
            {
                Data = parameterResult
            };
        }

        public Response<TransactionResult> GetPaymentResult(IFormCollection form)
        {
            var result = new TransactionResult();
            if (form == null)
            {
                result.ErrorMessage = "Form verisi alınamadı.";
                return new Response<TransactionResult>()
                {
                    Data = result
                };
            }
            result.OrderId = form["oid"];
            result.TransactionId = form["hostrefnum"];
            string mdStatus = form["mdStatus"];

            if (StringValues.IsNullOrEmpty(mdStatus))
            {
                result.ErrorMessage = form["mdErrorMsg"];
                result.ErrorCode = form["ProcReturnCode"];
                return new Response<TransactionResult>()
                {
                    Data = result
                };
            }

            var response = form["Response"];

            //mdstatus 1,2,3 veya 4 olursa 3D doğrulama geçildi anlamına geliyor
            string[] MdArray = { "1", "2", "3", "4" };
            if (!MdArray.Contains(mdStatus))
            {
                result.ErrorMessage = $"{form["mdErrorMsg"]} {form["errmsg"]}";
                result.ErrorCode = form["ProcReturnCode"];
                return new Response<TransactionResult>()
                {
                    Data = result
                };
            }


            if (StringValues.IsNullOrEmpty(response) || !response.Equals("Approved"))
            {
                result.ErrorMessage = $"{response} - {form["ErrMsg"]}";
                result.ErrorCode = form["ProcReturnCode"];
                return new Response<TransactionResult>()
                {
                    Data = result
                };
            }

            result.Success = true;
            result.ResponseCode = mdStatus;
            result.ErrorMessage = $"{response} - {form["ErrMsg"]}";

            return new Response<TransactionResult>()
            {
                Data = result
            };
        }


        public Response<TransactionResult> Cancel(AuthorizationRequest request)
        {
            //Begin(request);

            Cc5Request cc5Request = new Cc5Request()
            {
                Name = request.Rate.Gateway.Merchants.FirstOrDefault(x=> x.Key == "UserName")?.Value,
                Password = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "Password")?.Value,
                ClientId = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "ClientId")?.Value,
                Type = "Void",
                OrderId = request.OrderNumber,
                Currency = 949
            };

            Cc5Response VoidResponse = XmlSender.Post<Cc5Response>(request.Rate.Gateway.MerchantUri.GatewayUri, cc5Request);

            TransactionResult response = new TransactionResult
            {
                TransactionId = VoidResponse.TransId,
                ErrorMessage = VoidResponse.ErrMsg,
                ErrorCode = VoidResponse.ProcReturnCode,
                Success = VoidResponse.ProcReturnCode == "00",
                OrderId = request.OrderNumber
            };

            return new Response<TransactionResult>()
            {
                Data = response
            };
        }

        public Response<TransactionResult> Sales(AuthorizationRequest request)
        {
            //Begin(request);

            string cardNumber = request.CardNumber.Replace("-", string.Empty);
            cardNumber = cardNumber.Replace(" ", string.Empty).Trim();

            Cc5Response AuthResponse = XmlSender.Post<Cc5Response>(request.Rate.Gateway.MerchantUri.GatewayUri, new Cc5Request()
            {
                Name = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "UserName")?.Value,
                Password = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "Password")?.Value,
                ClientId = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "ClientId")?.Value,
                Type = "Auth",
                Total = request.TotalAmount.ToString(new CultureInfo("en-US")),
                OrderId = request.OrderNumber,
                Currency = Convert.ToInt32(CurrencyCodes.TRL),
                Number = cardNumber,
                Expires = $"{request.ExpireMonth}/{request.ExpireYear}",
                Cvv2Val = request.CvvCode
            });

            var result = new TransactionResult
            {
                TransactionId = AuthResponse.TransId,
                ErrorMessage = AuthResponse.ErrMsg,
                ErrorCode = AuthResponse.ProcReturnCode,
                Success = AuthResponse.ProcReturnCode == "00",
                OrderId = request.OrderNumber
            };

            //return End(result);

            return new Response<TransactionResult>()
            {
                Data = result
            };
        }

        public Response<TransactionResult> Refund(AuthorizationRequest request)
        {
            //Begin(request);

            string Type = "Credit";
            if (request.OrderDate != DateTime.Now.Date)
            {
                Type = "Credit";
            }

            Cc5Request cc5Request = new Cc5Request()
            {
                Name = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "UserName")?.Value,
                Password = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "Password")?.Value,
                ClientId = request.Rate.Gateway.Merchants.FirstOrDefault(x => x.Key == "ClientId")?.Value,
                Type = Type,
                OrderId = request.OrderNumber,
                Currency = 949,
                Total = request.TotalAmount.ToString(new CultureInfo("en-US"))
            };

            Cc5Response VoidResponse = XmlSender.Post<Cc5Response>(request.Rate.Gateway.MerchantUri.GatewayUri, cc5Request);

            var result = new TransactionResult
            {
                TransactionId = VoidResponse.TransId,
                ErrorMessage = VoidResponse.ErrMsg,
                ErrorCode = VoidResponse.ProcReturnCode,
                Success = VoidResponse.ProcReturnCode == "00",
                OrderId = request.OrderNumber
            };

            //return End(result);

            return new Response<TransactionResult>()
            {
                Data = result
            };
        }
    }
}