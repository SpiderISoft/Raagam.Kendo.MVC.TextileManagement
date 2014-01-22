using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class DepartmentPurchaseRequisitionTrailerModel
    {
        public DepartmentPurchaseRequisitionTrailerModel()
        {
            ProductGroupName = "";
            ProductName = "";
            Color = "";
            Size = "";
            SizeSpec = "";
            RejectReason = "";
            LotType = "";

        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long DepartmentPurchaseRequisitionHeaderSequenceNumber { get; set; }

        [DataMember]
        public long ProductGroupSequenceNumber { get; set; }

        [DataMember]
        public long ProductSequenceNumber { get; set; }

        [DataMember]
        public string ProductGroupName { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Color { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Size { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SizeSpec { get; set; }

        [DataMember]
        public decimal LotQuantity { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public decimal ApprovedQuantity { get; set; }

        [DataMember]
        public bool ActiveStatus { get; set; }

        [DataMember]
        public bool ApprovalStatus { get; set; }

        [DataMember]
        public string RejectReason { get; set; }

        [DataMember]
        public long LotTypeSequenceNumber { get; set; }

        [DataMember]
        public string LotType { get; set; }

        [DataMember]
        public byte Status { get; set; }

        [DataMember]
        public bool ManagerApprovalStatus { get; set; }

        [DataMember]
        public decimal ManagerApprovedQuantity { get; set; }

        [DataMember]
        public string ManagerRejectReason { get; set; }

        [DataMember]
        public decimal ReceivedQuantity { get; set; }

        [DataMember]
        public string  TransitBefore { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
 
    }
}
