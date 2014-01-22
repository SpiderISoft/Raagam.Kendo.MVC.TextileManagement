using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class LotTypeModel
    {

        [DataMember]
        public long LotTypeSequenceNumber { get; set; }

        [DataMember]
        public string LotTypeName { get; set; }

        [DataMember]
        public decimal LotTypeQuantity { get; set; }

        [DataMember]
        public long LotTypeLinkSequenceNumber { get; set; }

        [DataMember]
        public long UnitCategorySequenceNumber { get; set; }
    }
}
