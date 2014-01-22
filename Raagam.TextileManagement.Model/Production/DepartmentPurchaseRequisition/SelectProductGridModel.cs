using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class SelectProductGridModel
    {
        [DataMember]
        public bool Select { get; set; }

        [DataMember]
        public long ProductGroupSequenceNumber { get; set; }

        [DataMember]
        public long ProductSequenceNumber { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ManufacturerName { get; set; }

        [DataMember]
        public decimal StockCurrentQuantity { get; set; }

        [DataMember]
        public decimal ProductReOrderQuantity { get; set; }

        [DataMember]
        public long LotTypeSequenceNumber { get; set; }

        [DataMember]
        public decimal ProductTax { get; set; }
    }
}