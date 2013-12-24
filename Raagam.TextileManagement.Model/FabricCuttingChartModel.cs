using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.ComponentModel;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class FabricCuttingChartModel
    {
        public FabricCuttingChartModel()
        {
            BuyerDropDownList = new List<SelectListItem>();
            StyleDropDownList = new List<SelectListItem>();
            FabricLinkDropDownList = new List<LinkDropDownModel>();
            StyleSizeLinkDropDownList = new List<LinkDropDownModel>();
            fabricCuttingChartMainList = new List<FabricCuttingChartMainModel>();
            BuyerReferenceNumber = "";
            fabricCuttingChartDetailList = new List<FabricCuttingChartDetailModel>();
            KnitGSM = "";
            LoopLength = "";
            orderDetailsModelList = new List<FabricCuttingChartOrderDetailsModel>();
            panelColorModelList = new List<FabricCuttingChartPanelColorModel>();
            PanelLinkDropDownList = new List<LinkDropDownModel>();
            PanelSelectedList = new List<long>();
            StyleSizeSelectedList = new List<long>();
        }
       
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
        [DisplayName("Buyer")]
        public long BuyerSequenceNumber{ get; set; }

        [DataMember]
        public List<SelectListItem> BuyerDropDownList { get; set; }


        [DataMember]
        [DisplayName("Style")]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public List<SelectListItem> StyleDropDownList { get; set; }

        [DataMember]
        [DisplayName("Fabric")]
        public long FabricSequenceNumber { get; set; }

        [DataMember]
        public List<LinkDropDownModel> FabricLinkDropDownList { get; set; }

        [DataMember]
        [DisplayName("Panel")]
        public List<long> PanelSelectedList { get; set; }

        [DataMember]
        public List<LinkDropDownModel> PanelLinkDropDownList { get; set; }

        [DataMember]
        [DisplayName("Size")]
        public List<long> StyleSizeSelectedList { get; set; }

        [DataMember]
        public List<LinkDropDownModel> StyleSizeLinkDropDownList { get; set; }

        [DataMember]
        [DisplayName("Loop Length")]
        public string LoopLength { get; set; }

        [DataMember]
        [DisplayName("Knit GSM")]
        public string KnitGSM { get; set; }

        [DataMember]
        [DisplayName("Table Dia")]
        public decimal TableDia { get; set; }

        [DataMember]
        [DisplayName("Knit Dia")]
        public decimal KnitDia { get; set; }

        [DataMember]
        [DisplayName("Weight")]
        public decimal Weight { get; set; }


        [DataMember]
        [DisplayName("Pieces")]
        public decimal Pieces { get; set; }

        [DataMember]
        public List<FabricCuttingChartOrderDetailsModel> orderDetailsModelList { get; set; }



        [DataMember]
        public List<FabricCuttingChartMainModel> fabricCuttingChartMainList { get; set; }


        [DataMember]
        public List<FabricCuttingChartDetailModel> fabricCuttingChartDetailList { get; set; }

        [DataMember]
        public List<FabricCuttingChartPanelColorModel> panelColorModelList { get; set; }



    }

    [DataContract]
    public class LinkDropDownModel
    {
        [DataMember]
        public long Key { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public long ForeignKey { get; set; }

    }

    [DataContract]
    public class FabricCuttingChartMainModel
    {
        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long OrderSequenceNumber { get; set; }

        [DataMember]
        public long StyleFabricSequenceNumber { get; set; }

        [DataMember]
        public long StyleSizeSequenceNumber { get; set; }

        [DataMember]
        public string StylePanelName { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal TableDia { get; set; }

        [DataMember]
        public decimal KnitDia { get; set; }

        [DataMember]
        public string SizeName { get; set; }

        [DataMember]
        public decimal Pieces { get; set; }

        [DataMember]
        public decimal Weight { get; set; }

        [DataMember]
        public decimal WastagePercentage { get; set; }

        [DataMember]
        public decimal TotalWeight { get; set; }

        [DataMember]
        public string TempGUID { get; set; }

        [DataMember]
        public long StyleColorSequenceNumber { get; set; }

        [DataMember]
        public string ColorName { get; set; }

        [DataMember]
        public string LoopLength { get; set; }

        [DataMember]
        public string KnitGSM { get; set; }

    }

    [DataContract]
    public class FabricCuttingChartDetailModel
    {
        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long FabricCuttingChartMainSequenceNumber { get; set; }

        [DataMember]
        public long StylePanelSequenceNumber { get; set; }

        [DataMember]
        public string PanelName { get; set; }

        [DataMember]
        public string TempGUID { get; set; }
    }

    [DataContract]
    public class FabricCuttingChartOrderDetailsModel
    {
        [DataMember]
        public long StyleSizeSequenceNumber { get; set; }

        [DataMember]
        public string SizeName { get; set; }

        [DataMember]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public int OrderExcessQty { get; set; }
    }

    [DataContract]
    public class FabricCuttingChartPanelColorModel
    {
        [DataMember]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public long StylePanelSequenceNumber { get; set; }

        [DataMember]
        public long StyleSizeSequenceNumber { get; set; }

        [DataMember]
        public string SizeName { get; set; }

        [DataMember]
        public long StyleColorSequenceNumber { get; set; }

        [DataMember]
        public string ColorName { get; set; }

        [DataMember]
        public int OrderExcessQty { get; set; }

        [DataMember]
        public string TempGUID { get; set; }


    }

}
