using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class DropDownModel
    {
        [DataMember]
        public long Value { get; set; }

        [DataMember]
        public string Text { get; set; }
    }

}
