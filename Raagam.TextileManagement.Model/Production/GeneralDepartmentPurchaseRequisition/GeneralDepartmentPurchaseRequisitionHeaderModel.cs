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
    public class GeneralDepartmentPurchaseRequisitionHeaderModel
    {

        public GeneralDepartmentPurchaseRequisitionHeaderModel()
        {
            ProcessDropDownList = new List<SelectListItem>();
            ProductGroupModelList = new List<ProductGroupModel>();
            ProductModelList = new List<ProductModel>();
            OrderDropDownList = new List<SelectListItem>();
            LotTypeModelList = new List<LotTypeModel>();
            DepartmentPurchaseRequisitionTrailerModelList = new List<GeneralDepartmentPurchaseRequisitionTrailerModel>();
            Mode = EnumConstants.ScreenMode.New;
            Remarks = "";
            CreatedBy = "";
            UpdatedBy = "";
            RejectReason = "";
            ActiveStatus = true;
            DepartmentPurchaseRequisitionDate = DateTime.Now;
        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DisplayName("Pur Req No")]
        [DataMember]
        public long DepartmentPurchaseRequisitionNumber { get; set; }

        [DisplayName("Pur Req Date")]
        [DataMember]
        public DateTime DepartmentPurchaseRequisitionDate { get; set; }

        [DisplayName("Process")]
        [DataMember]
        public long ProcessSequenceNumber { get; set; }

 
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
        public bool ProcessedStatus { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RejectReason { get; set; }

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
        public List<GeneralDepartmentPurchaseRequisitionTrailerModel> DepartmentPurchaseRequisitionTrailerModelList { get; set; }


        [DataMember]
        public EnumConstants.ScreenMode Mode { get; set; }

    }
}
