using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Domain
{
    [DataContract]
    public class Response<T>
    {
        public Response()
        {
            ErrorMessage = new List<string>();
        }

        [DataMember(Name = "Data", EmitDefaultValue = false)]
        public T Data { get; set; }

        public bool IsSuccessfull { get; set; } = true;

        private IEnumerable<string> errorMessage;


        [DataMember(Name = "ErrorMessage", EmitDefaultValue = false)]
        public IEnumerable<string> ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                if (value != null && value.Count() > 0)
                {
                    IsSuccessfull = false;
                }

                errorMessage = value;
            }
        }
    }
}