using System;

namespace Application.Exceptions
{
    public class BusinessException : ExceptionBase
    {
        public BusinessException(string responseCode) : base(responseCode)
        {
        }

        public BusinessException(string responseCode, Exception exception) : base(responseCode, exception)
        {
        }

        public BusinessException(string responseCode, params object[] parameters) : base(responseCode, parameters)
        {
        }

        public BusinessException(string responseCode, Exception exception, params object[] parameters) : base(responseCode, exception, parameters)
        {
        }
    }
}
