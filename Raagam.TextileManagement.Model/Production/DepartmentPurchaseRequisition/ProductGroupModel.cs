using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class ProductGroupModel
    {
        [DataMember]
        public long ProductGroupSequenceNumber { get; set; }

        [DataMember]
        public string ProductGroupName { get; set; }

        [DataMember]
        public long ProductSupplierSequenceNumber { get; set; }
    }
}
