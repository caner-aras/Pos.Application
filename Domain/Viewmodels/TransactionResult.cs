using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Viewmodels
{
    public class TransactionResult
    {
        public bool Success { get; set; }
        public string ResponseCode { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string Raw { get; set; }
        public string ReturnUrl { get; set; }
    }
}
