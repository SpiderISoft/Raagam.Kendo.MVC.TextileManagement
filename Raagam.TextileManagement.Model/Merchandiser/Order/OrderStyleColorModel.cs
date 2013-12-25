using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class OrderStyleColorModel
    {
        public OrderStyleColorModel()
        {
            ColorName = "";
        }
        [DataMember]
        public long StyleColorSequence { get; set; }

        [DataMember]
        public string ColorName { get; set; }

    }
}
