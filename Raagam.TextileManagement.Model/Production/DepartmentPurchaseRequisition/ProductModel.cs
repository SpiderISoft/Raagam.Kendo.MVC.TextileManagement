using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class ProductModel
    {
        [DataMember]
        public long ProductSequenceNumber { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public long ProductGroupSequenceNumber { get; set; }

        [DataMember]
        public long ProductSupplierSequenceNumber { get; set; }

        [DataMember]
        public long ProductCategorySequenceNumber { get; set; }

        [DataMember]
        public decimal CostPrice { get; set; }

        [DataMember]
        public decimal ProductTax { get; set; }

    }
}
