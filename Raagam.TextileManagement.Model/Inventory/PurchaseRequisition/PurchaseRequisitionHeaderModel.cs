using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class PurchaseRequisitionHeaderModel
    {

        public PurchaseRequisitionHeaderModel()
        {
            ProcessDropDownList = new List<SelectListItem>();
            ProductGroupModelList = new List<ProductGroupModel>();
            ProductModelList = new List<ProductModel>();
            OrderDropDownList = new List<SelectListItem>();
            LotTypeModelList = new List<LotTypeModel>();
            PurchaseRequisitionTrailerModelList = new List<PurchaseRequisitionTrailerModel>();
            Mode = EnumConstants.ScreenMode.New;
            Remarks = "";
            CreatedBy = "";
            UpdatedBy = "";
            ActiveStatus = true;
            PurchaseRequisitionDate = DateTime.Now;
            SupplierDropDownList = new List<SelectListItem>();
        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DisplayName("Pur Req No")]
        [DataMember]
        public long PurchaseRequisitionNumber { get; set; }

        [DisplayName("Pur Req Date")]
        [DataMember]
        public DateTime PurchaseRequisitionDate { get; set; }

        [DisplayName("Order Number")]
        [DataMember]
        public long OrderNumber { get; set; }

        [DisplayName("Remarks")]
        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remarks { get; set; }

        [DataMember]
        public bool ActiveStatus { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDateTime { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UpdatedBy { get; set; }

        [DataMember]
        public DateTime UpdatedDateTime { get; set; }

       
        [DataMember]
        public List<SelectListItem> ProcessDropDownList { get; set; }


        [DataMember]
        public List<ProductGroupModel> ProductGroupModelList { get; set; }

        [DataMember]
        public List<ProductModel> ProductModelList { get; set; }

        [DataMember]
        public List<SelectListItem> OrderDropDownList { get; set; }


        [DataMember]
        public List<LotTypeModel> LotTypeModelList { get; set; }

        [DataMember]
        public List<PurchaseRequisitionTrailerModel> PurchaseRequisitionTrailerModelList { get; set; }
        
        [DataMember]
        public List<SelectListItem> SupplierDropDownList { get; set; }


        [DataMember]
        public EnumConstants.ScreenMode Mode { get; set; }

    }
}
