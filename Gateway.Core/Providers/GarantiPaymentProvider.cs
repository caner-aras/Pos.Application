using Application;
using Gateway.Domain.ViewModel.Gate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Gateway.Core.Models.Garanti;

namespace Gateway.Core.Providers
{
    public class GarantiPaymentProvider : IPaymentProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string HashedPassword { get; set; }
        public GVPSRequest GVPSRequest { get; set; }

        public GarantiPaymentProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Response<TheedResult> GetPaymentParameters(AuthorizationRequest request)
        {
            string terminaluserid = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "UserId").Value;
            string terminalid = request.Rate.Gateway.Merchants.FirstOrDefault(k=> k.Key == "TerminalId").Value;
            string successUrl = $"{_httpContextAccessor.HttpContext.Request.Host.Value}/api/callback/{request.OrderNumber}";
            string errorurl = $"{_httpContextAccessor.HttpContext.Request.Host.Value}/api/callback/{request.OrderNumber}";
            string type = "sales";

            var parameterResult = new TheedResult();
            try
            {
                string cardNumber = request.CardNumber.Replace("-", string.Empty);
                cardNumber = cardNumber.Replace(" ", string.Empty).Trim();
                string amount = (request.TotalAmount).ToString("N").Replace(",", "").Replace(".", "");
                string installment = request.Installment.ToString();
                if (request.Installment <= 1)
                    installment = string.Empty;

                //provizyon şifresi ve 9 haneli terminal numarasının birleşimi ile bir hash oluşturuluyor
                string _terminalid = "0" + terminalid;

                // veriler birleştirilip hash oluşturuluyor
                string securityData = GetSHA1(request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "Password").Value + _terminalid).ToUpper();
                string hashstr = terminalid + request.OrderNumber + amount + successUrl + errorurl + type + installment + request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "StoreKey").Value + securityData;

                var parameters = new Dictionary<string, object>
                {
                    { "cardnumber", cardNumber },
                    { "cardcvv2", request.CvvCode },
                    { "cardexpiredatemonth", request.ExpireMonth },
                    { "cardexpiredateyear", request.ExpireYear },
                    { "secure3dsecuritylevel", "3D_PAY" },
                    { "mode", "PROD" },
                    { "apiversion", "v0.01" },
                    { "terminalprovuserid",request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "ProvUserId").Value },
                    { "terminaluserid", terminaluserid },
                    { "terminalmerchantid", request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "MerchantId").Value },
                    { "terminalid", terminalid },
                    { "txntype", type },
                    { "txncurrencycode", request.CurrencyIsoCode },//TL ISO code | EURO 978 | Dolar 840
                    { "motoind", "N" },
                    { "customeripaddress", request.CustomerIpAddress },
                    { "orderaddressname1", request.CardHolderName },
                    { "orderid", request.OrderNumber },
                    { "successurl", successUrl },
                    { "errorurl", errorurl },
                    { "txnamount", amount },
                    { "txninstallmentcount", installment },//taksit sayısı | boş tek çekim olur
                    { "secure3dhash", GetSHA1(hashstr).ToUpper() }//ToUpper ile tüm karakterlerin büyük harf olması gerekiyor
                };

                parameterResult.Parameters = parameters;
                parameterResult.PaymentUrl = new Uri(request.Rate.Gateway.MerchantUri.GateUri);

                //Begin(request);
            }
            catch (Exception ex)
            {
               
            }

            return new Response<TheedResult>()
            {
                Data = parameterResult
            };
        }

        public Response<TransactionResult> GetPaymentResult(IFormCollection form)
        {
            var paymentResult = new TransactionResult();
            if (form == null)
            {
                return new Response<TransactionResult>()
                {
                    ErrorMessage = "Form verisi alınamadı."
                };
            }
            paymentResult.OrderId = form["oid"];
            paymentResult.TransactionId = form["hostrefnum"];
            string mdStatus = form["mdStatus"];

            if (StringValues.IsNullOrEmpty(mdStatus))
            {
                paymentResult.ErrorMessage = form["mdErrorMsg"];
                paymentResult.ErrorCode = form["ProcReturnCode"];
                return new Response<TransactionResult>()
                {
                    Data = paymentResult
                };
            }

            var response = form["Response"];

            //mdstatus 1,2,3 veya 4 olursa 3D doğrulama geçildi anlamına geliyor
            string[] MdArray = { "1", "2", "3", "4" };
            if (!MdArray.Contains(mdStatus))
            {
                paymentResult.ErrorMessage = $"{response} - {form["mdErrorMsg"]} ({form["errmsg"]})";
                paymentResult.ErrorCode = form["ProcReturnCode"];
                return new Response<TransactionResult>()
                {
                    Data = paymentResult
                };
            }


            if (StringValues.IsNullOrEmpty(response) || !response.Equals("Approved"))
            {
                paymentResult.ErrorMessage = $"{response} - {form["mdErrorMsg"]} ({form["errmsg"]})";
                paymentResult.ErrorCode = form["ProcReturnCode"];
                return new Response<TransactionResult>()
                {
                    Data = paymentResult
                };
            }

            paymentResult.Success = true;
            paymentResult.ResponseCode = mdStatus;
            paymentResult.TransactionId = form["hostrefnum"];

            return new Response<TransactionResult>()
            {
                Data = paymentResult
            };
        }

        private Response<GVPSRequest> Init(AuthorizationRequest request)
        {
            var password = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "Password").Value;
            var terminalId = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "TerminalId").Value;

           this.HashedPassword = GetSHA1(password + IsRequireZero(terminalId, 9)).ToUpper();

            var AsmName = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName();
            string Version = AsmName.Name + " v" + AsmName.Version.Major.ToString() + "." + AsmName.Version.Minor.ToString();

            return new Response<GVPSRequest>()
            {
                Data = new GVPSRequest()
                {
                    Terminal = new GVPSTerminal()
                    {
                        ID = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "TerminalId").Value,
                        MerchantID = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "MerchantId").Value,
                        ProvUserID = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "ProvUserId").Value,
                        UserID = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "UserId").Value
                    },
                    Customer = new GVPSCustomer()
                    {
                        EmailAddress = request.CustomerEmailAddress,
                        IPAddress = request.CustomerIpAddress
                    },
                    Transaction = new GVPSTransaction()
                    {
                        CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal,
                        MotoInd = GVPSMotoIndEnum.ECommerce,
                        Amount = (ulong)(Math.Round(request.TotalAmount, 2) * 100),
                        CurrencyCode = GVPSCurrencyCodeEnum.TRL
                    },
                    Order = new GVPSOrder()
                    {
                        OrderID = request.OrderNumber,
                    },
                    Version = Version,
                    Mode = (GVPSRequestModeEnum.Test)
                }
            };

        }

        public Response<TransactionResult> Sales(AuthorizationRequest request)
        {
            GVPSRequest = Init(request).Data;
            GVPSRequest.Card = new GVPSCard()
            {
                Number = request.CardNumber,
                CardHolder = request.CardHolderName,
                ExpireDate = string.Format("{0}{1}", IsRequireZero(request.ExpireMonth), IsRequireZero(request.ExpireYear))
            };
            GVPSRequest.Transaction.Type = GVPSTransactionTypeEnum.sales;
            GVPSRequest.Terminal.HashData = GetSHA1(GVPSRequest.Order.OrderID +
                                                    GVPSRequest.Terminal.ID +
                                                    GVPSRequest.Card.Number +
                                                    GVPSRequest.Transaction.Amount +
                                                    this.HashedPassword
                                                    ).ToUpper();

            return Send(request.Rate.Gateway.MerchantUri.GateUri);
        }

        public Response<TransactionResult> Refund(AuthorizationRequest request)
        {
            GVPSRequest = Init(request).Data;

            GVPSRequest.Terminal.ProvUserID = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "RefundUser").Value;
            GVPSRequest.Transaction.Type = GVPSTransactionTypeEnum.refund;
            GVPSRequest.Terminal.HashData = GetSHA1(request.OrderNumber +
                                                request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "TerminalId").Value +
                                                (ulong)(Math.Round(request.TotalAmount, 2) * 100) +
                                                this.HashedPassword
                                                ).ToUpper();

            return Send(request.Rate.Gateway.MerchantUri.GatewayUri);
        }

        public Response<TransactionResult> Cancel(AuthorizationRequest request)
        {
            GVPSRequest = Init(request).Data;

            GVPSRequest.Terminal.ProvUserID = request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "RefundUser").Value;
            GVPSRequest.Transaction.Type = GVPSTransactionTypeEnum.@void;
            GVPSRequest.Transaction.OriginalRetrefNum = request.AuthNumber;
            GVPSRequest.Terminal.HashData = GetSHA1(request.OrderNumber +
                                                request.Rate.Gateway.Merchants.FirstOrDefault(k => k.Key == "TerminalId").Value +
                                                (ulong)(Math.Round(request.TotalAmount, 2) * 100) +
                                                this.HashedPassword
                                                ).ToUpper();

            return Send(request.Rate.Gateway.MerchantUri.GatewayUri);
        }

        public Response<TransactionResult> PostauthCancel(AuthorizationRequest request)
        {
            return Cancel(request);
        }


        #region Privates
        private string IsRequireZero(int time)
        {
            return time < 10 ? String.Format("0{0}", time) : time > 2000 ? IsRequireZero(time - 2000) : time.ToString();
        }

        private string GetSHA1(string text)
        {
            var cryptoServiceProvider = new SHA1CryptoServiceProvider();
            var inputbytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(text));

            var builder = new StringBuilder();
            for (int i = 0; i < inputbytes.Length; i++)
            {
                builder.Append(string.Format("{0,2:x}", inputbytes[i]).Replace(" ", "0"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        static string IsRequireZero(string id, int complete)
        {
            var _tmp = id.Trim();

            if (_tmp.Length < complete)
                for (int i = 0; (i < (complete - _tmp.Length)); i++)
                    id = id.Insert(0, "0");

            return id;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TModel"></param>
        /// <returns></returns>
        private static string SerializeObjectToXmlString<T>(T TModel)
        {
            string xmlData = string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add(string.Empty, string.Empty);

                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, Encoding = Encoding.GetEncoding("ISO-8859-1") }))
                {
                    xmlSerializer.Serialize(xmlWriter, TModel, xmlns);
                    xmlData = stringWriter.ToString();
                }
            }

            return xmlData;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private T DeSerializeObject<T>(string xmlData)
        {
            T deSerializeObject = default;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xmlData);
            XmlReader XR = new XmlTextReader(stringReader);

            if (xmlSerializer.CanDeserialize(XR))
            {
                deSerializeObject = (T)xmlSerializer.Deserialize(XR);
            }

            return deSerializeObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="Method"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        private string SendHttpRequest(string Host, string Method, string Params)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate
                                                                            (object sender, X509Certificate certificate, X509Chain chain,
                                                                            SslPolicyErrors sslPolicyErrors)
            { return true; };

            var returnSrting = String.Empty;

            var request = (HttpWebRequest)WebRequest.Create(Host);
            request.Timeout = 30000;
            request.Method = Method;

            var bytes = new ASCIIEncoding().GetBytes(Params);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Params.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                returnSrting = sr.ReadToEnd();
            }

            return returnSrting;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        private Response<TransactionResult> Send(string gatewayUri)
        {
            var gvpResponse = new TransactionResult();
            try
            {
                var responseString = string.Empty;
                responseString = SendHttpRequest(gatewayUri, "Post", String.Format("data={0}", WebUtility.UrlEncode(SerializeObjectToXmlString(GVPSRequest))));
                var Response = DeSerializeObject<GVPSResponse>(responseString);

                gvpResponse.ResponseCode = Response.Transaction.Response.Code;
                gvpResponse.TransactionId = Response.Transaction.RetrefNum;
                gvpResponse.OrderId = Response.Order.OrderID;
                gvpResponse.Success = Response.Transaction.Response.Code == "00";
                gvpResponse.ErrorMessage = Response.Transaction.Response.ErrorMsg;
            }
            catch (Exception ex)
            {
                gvpResponse.ErrorMessage = ex.Message;
                gvpResponse.ErrorCode = "99";
                gvpResponse.Success = false;
            }

            return new Response<TransactionResult>()
            {
                Data = gvpResponse
            };
        }

        #endregion
    }
}