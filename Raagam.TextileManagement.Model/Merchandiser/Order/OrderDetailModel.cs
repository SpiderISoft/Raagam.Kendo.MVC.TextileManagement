using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class OrderDetailModel
    {
        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long OrderNumber { get; set; }

        [DataMember]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public long StyleColorSequenceNumber { get; set; }

        [DataMember]
        public long StyleSizeSequenceNumber { get; set; }

        [DataMember]
        public decimal OrderDetailQuantity { get; set; }

        [DataMember]
        public decimal OrderDetailExcessQuantity { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }

    }
}
