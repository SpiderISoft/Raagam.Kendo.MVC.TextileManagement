using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Web.Mvc;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class OrderMainModel
    {
        [DataMember]
        [DisplayName("Order Number")]
        public long OrderNumber { get; set; }

        [DataMember]
        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DataMember]
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [DataMember]
        [DisplayName("Buyer Reference Number")]
        public string BuyerReferenceNumber { get; set; }

        [DataMember]
        [DisplayName("Order Quantity")]
        public int OrderQuantity { get; set; }

        [DataMember]
        [DisplayName("Excess Quantity")]
        public int ExcessQuantity { get; set; }

        [DataMember]
        [DisplayName("Excess %")]
        public int ExcessPercentage { get; set; }

        [DataMember]
        [DisplayName("Buyer")]
        public long BuyerSequenceNumber { get; set; }

        [DataMember]
        [DisplayName("Style")]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public List<SelectListItem> BuyerDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> StyleDropDownList { get; set; }
    }
}
