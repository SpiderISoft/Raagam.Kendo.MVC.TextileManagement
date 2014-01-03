using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class JqGridData
    {
        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public int Page { get; set; }

        [DataMember]
        public int Records { get; set; }

        [DataMember]
        public IEnumerable Rows { get; set; }

        [DataMember]
        public object UserData { get; set; }
 


    }
}
