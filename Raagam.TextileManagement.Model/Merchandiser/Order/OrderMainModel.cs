using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Web.Mvc;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class OrderMainModel
    {

        public OrderMainModel()
        {
            OrderDetailModelList = new List<OrderDetailModel>();
            BuyerReferenceNumber = "";
            BuyerDropDownList = new List<SelectListItem>();
            StyleDropDownList = new List<SelectListItem>();
            StyleColorList = new List<OrderStyleColorModel>();
            StyleSizeList = new List<OrderStyleSizeModel>();
            PackingType = "";
            PackingInstructions = "";
            AssortmentDetails = "";
            Comments = "";
            IsCompleted = false;
            Mode = EnumConstants.ScreenMode.New;

        }

        [DataMember]
        public EnumConstants.ScreenMode Mode { get; set; }


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
        public decimal OrderQuantity { get; set; }

        [DataMember]
        [DisplayName("Excess Quantity")]
        public decimal ExcessQuantity { get; set; }

        [DataMember]
        [DisplayName("Excess %")]
        public decimal ExcessPercentage { get; set; }

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

        [DataMember]
        public List<OrderStyleColorModel> StyleColorList { get; set; }

        [DataMember]
        public List<OrderStyleSizeModel> StyleSizeList { get; set; }


        [DataMember]
        public List<OrderDetailModel> OrderDetailModelList { get; set; }


        [DataMember]
        [DisplayName("Packing Type")]
        public string PackingType { get; set; }


        [DataMember]
        [DisplayName("Packing Instructions")]
        public string PackingInstructions { get; set; }


        [DataMember]
        [DisplayName("Assortment Details")]
        public string AssortmentDetails { get; set; }

        [DataMember]
        [DisplayName("Comments")]
        public string Comments { get; set; }


        [DataMember]
        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }
    }
}
