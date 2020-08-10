using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Viewmodels
{
    [DataContract]
    public class RequestBase
    {
        [DataMember]
        public int ClientId { get; set; }
    }
}
