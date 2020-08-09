using System;

namespace Application.Exceptions
{
    public class ExceptionBase : System.Exception
    {
        public string ResponseCode { get; set; }
        public object[] Parameters { get; set; }

        public ExceptionBase(string responseCode)
            : base(responseCode)
        {
            this.ResponseCode = responseCode;
        }

        public ExceptionBase(string responseCode, Exception exception)
            : base(responseCode, exception)
        {
            this.ResponseCode = responseCode;
        }

        public ExceptionBase(string responseCode, params object[] parameters)
            : base(responseCode)
        {
            this.ResponseCode = responseCode;
            this.Parameters = parameters;
        }

        public ExceptionBase(string responseCode, Exception exception, params object[] parameters)
            : base(responseCode, exception)
        {
            this.ResponseCode = responseCode;
            this.Parameters = parameters;
        }
    }
}
