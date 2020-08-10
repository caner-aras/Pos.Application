using Application;
using Application.Helpers.XmlHelper;
using Gateway.Domain.ViewModel.Gate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Gateway.Core.Model.PosNet;

namespace Gateway.Core.Providers
{
    /// <summary>
    /// Yapıkredi sanal pos işlemleri Vakıfbank ile benzer şekilde, girilen kart bilgisinin 3D doğrulamasını yapıp eğer sonuç başarılıysa banka sms sayfasına yönlendirme yapılmasını istiyor.
    /// Kart bilgisi 3D ödeme için uygun olması durumunda yönlendirilecek sayfa bilgisini xml içerisinde dönüyor
    /// </summary>
    public class YapikrediPaymentProvider : IPaymentProvider
    {
        private readonly IConfiguration _Config;
        private string MerchantId { get; set; }
        private string TerminalId { get; set; }
        private string PosNetId { get; set; }
        public YapikrediPaymentProvider(IConfiguration config)
        {
            _Config = config;
            MerchantId = _Config["YapiKredi:MerchantId"];
            TerminalId = _Config["YapiKredi:TerminalId"];
            PosNetId = _Config["YapiKredi:PosNetId"];
        }

        public Response<TheedResult> GetPaymentParameters(AuthorizationRequest request)
        {
            TheedResult parameterResult = new TheedResult();

            try
            {
                string cardNumber = request.CardNumber.Replace("-", string.Empty);
                cardNumber = cardNumber.Replace(" ", string.Empty).Trim();
                string amount = (request.TotalAmount * 100m).ToString().Replace(",", "").Replace(".", "");


                //Begin(request);

                PosnetRequest PosNetRequest = new PosnetRequest
                {
                    Mid = MerchantId,
                    Tid = TerminalId,
                    OosRequestData = new OosRequestData()
                    {
                        Ccno = cardNumber,
                        Cvc = request.CvvCode,
                        ExpDate = $"{request.ExpireYear}{request.ExpireMonth.ToString().PadLeft(2, '0')}",
                        CardHolderName = request.CardHolderName,
                        TranType = "Sale",
                        Posnetid = PosNetId,
                        Amount = amount,
                        CurrencyCode = "TL",
                        Installment = string.Format("{0:00}", request.Installment),
                        XID = request.OrderNumber
                    }
                };

                PosnetResponse Validate = PostWithQuery<PosnetResponse>(_Config["YapiKredi:Gateway"], SerializeDeserialize.SerializeObjectToXmlString(PosNetRequest));

                if (Validate != null && Validate.Approved == "1")
                {
                    var parameters = new Dictionary<string, object>
                    {
                        { "mid", MerchantId },
                        { "posnetID", PosNetId },
                        { "posnetData", Validate.OosRequestDataResponse.PostNetData1 },
                        { "posnetData2", Validate.OosRequestDataResponse.PostNetData2 },
                        { "digest", Validate.OosRequestDataResponse.Sign },
                        { "merchantReturnURL", $"{_Config["Host"]}/api/callback/{request.OrderNumber}"},
                        { "lang", "tr" },
                        { "currencyCode", "TL" }
                    };

                    parameterResult.Parameters = parameters;
                    parameterResult.PaymentUrl = new Uri(_Config["YapiKredi:Gate"]);
                }

                return new Response<TheedResult>()
                {
                    Data = parameterResult,
                    ErrorMessage = Validate.RespText
                };

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
            var result = new TransactionResult();

            StringBuilder Mac = new StringBuilder();
            Mac.Append(form["Xid"]);
            Mac.Append(";");
            Mac.Append(form["amount"]);
            Mac.Append(";");
            Mac.Append("TL");
            Mac.Append(";");
            Mac.Append(form["MerchantId"]);
            Mac.Append(";");
            Mac.Append(Hash(_Config["YapiKredi:EncKey"] + ';' + TerminalId));

            PosnetRequest PosNetRequest = new PosnetRequest
            {
                Mid = MerchantId,
                Tid = TerminalId,
                OosResolveMerchantData = new OosResolveMerchantData()
                {
                    BankData = form["BankPacket"],
                    MerchantData = form["MerchantPacket"],
                    Mac = Hash(Mac.ToString()),
                    Sign = form["Sign"]
                }
            };

            PosnetResponse ValidateMac = PostWithQuery<PosnetResponse>(_Config["YapiKredi:Gateway"], SerializeDeserialize.SerializeObjectToXmlString(PosNetRequest).ToString());

            if (ValidateMac != null && ValidateMac.Approved == "1")
            {
                var req = new PosnetRequest()
                {
                    Mid = MerchantId,
                    Tid = TerminalId,
                    OosTranData = new OosTranData()
                    {
                        BankData = PosNetRequest.OosResolveMerchantData.BankData,
                        Mac = PosNetRequest.OosResolveMerchantData.Mac
                    }
                };

                PosnetResponse AuthPayment = PostWithQuery<PosnetResponse>(_Config["YapiKredi:Gateway"], SerializeDeserialize.SerializeObjectToXmlString(req));

                result = new TransactionResult()
                {
                    ErrorCode = AuthPayment.RespCode,
                    ErrorMessage = AuthPayment.RespText,
                    Success = AuthPayment.Approved == "1",
                    OrderId = form["Xid"],
                    TransactionId = AuthPayment.Hostlogkey,
                    ResponseCode = ValidateMac.OosResolveMerchantDataResponse.MdStatus
                };
            }

            //return End(result);

            return new Response<TransactionResult>()
            {
                Data = result
            };
        }

        public Response<TransactionResult> Cancel(AuthorizationRequest request)
        {
            //Begin(request);

            var posnetRequest = new PosnetRequest()
            {
                Mid = MerchantId,
                Tid = TerminalId,
                ReverseInfo = new ReverseInfo()
                {
                    HostLogKey = request.AuthNumber,
                    OrderID = request.OrderNumber,
                    Transaction = "sale"
                }
            };

            PosnetResponse Void =
                PostWithQuery<PosnetResponse>(_Config["YapiKredi:Gateway"], SerializeDeserialize.SerializeObjectToXmlString(posnetRequest).ToString());

            var response = new TransactionResult()
            {
                ErrorCode = Void.RespCode,
                TransactionId = Void.Hostlogkey,
                OrderId = request.OrderNumber,
                ResponseCode = Void.Approved,
                Success = Void.Approved == "1",
                ErrorMessage = Void.RespText
            };

            //return End(response);

            return new Response<TransactionResult>()
            {
                Data = response
            };
        }

        public Response<TransactionResult> Refund(AuthorizationRequest request)
        {
            //Begin(request);

            var posnetRequest = new PosnetRequest()
            {
                Mid = MerchantId,
                Tid = TerminalId,
                ReverseInfo = new ReverseInfo()
                {
                    HostLogKey = request.AuthNumber,
                    OrderID = request.OrderNumber,
                    Transaction = "return"
                }
            };

            PosnetResponse Void =
                PostWithQuery<PosnetResponse>(_Config["YapiKredi:Gateway"], SerializeDeserialize.SerializeObjectToXmlString(posnetRequest).ToString());

            var response = new TransactionResult()
            {
                ErrorCode = Void.RespCode,
                TransactionId = Void.Hostlogkey,
                OrderId = request.OrderNumber,
                ResponseCode = Void.Approved,
                Success = Void.Approved == "1",
                ErrorMessage = Void.RespText
            };

            //return End(response);

            return new Response<TransactionResult>()
            {
                Data = response
            };
        }

        public Response<TransactionResult> Sales(AuthorizationRequest request)
        {
            //Begin(request);
            var response= new TransactionResult();

            string cardNumber = request.CardNumber.Replace("-", string.Empty);
            cardNumber = cardNumber.Replace(" ", string.Empty).Trim();
            string amount = (request.TotalAmount).ToString().Replace(",", "").Replace(".", "");

            PosnetRequest xmlrequest = new PosnetRequest()
            {
                Mid = MerchantId,
                Tid = TerminalId,
                TranDateRequired = "1",
                Sale = new Sale()
                {
                    Amount = amount,
                    CreditCardNumber = cardNumber,
                    Currency = "TL",
                    Cvc = request.CvvCode,
                    ExpDate = $"{request.ExpireYear}{request.ExpireMonth.ToString().PadLeft(2, '0')}",
                    OrderID = request.OrderNumber,
                    Installment = string.Format("{0:00}", request.Installment)
                }
            };

            PosnetResponse Sales =
                PostWithQuery<PosnetResponse>(_Config["YapiKredi:Gateway"], SerializeDeserialize.SerializeObjectToXmlString(xmlrequest).ToString());

            if (Sales != null)
            {
                response= new TransactionResult()
                {
                    TransactionId = Sales.Hostlogkey,
                    OrderId = request.OrderNumber,
                    ErrorCode = Sales.RespCode,
                    ErrorMessage = Sales.RespText,
                    ResponseCode = Sales.RespCode,
                    Success = Sales.Approved == "1"
                };
            }

            //return End(response);

            return new Response<TransactionResult>()
            {
                Data = response
            };
        }

        private string Hash(string originalString)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));
            return Convert.ToBase64String(bytes);
        }


        private PosnetResponse PostWithQuery<Respose>(string destinationUrl, string StringXml)
        {
            try
            {
                string Encoded = UrlEncoder.Default.Encode(StringXml);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl + "?xmldata=" + Encoded);
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(StringXml);
                request.ContentType = "application/xwww-form-urlencoded; charset=utf-8";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return SerializeDeserialize.DeSerializeObject<PosnetResponse>(responseStr);
                }
                return null;

            }
            catch (Exception ex)
            {
                return new PosnetResponse()
                {
                    RespText = ex.Message
                };
            }
        }

    }
}