﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class PurchaseRequisitionTrailerModel
    {
        public PurchaseRequisitionTrailerModel()
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
        public long PurchaseRequisitionHeaderSequenceNumber { get; set; }

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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RejectReason { get; set; }

        [DataMember]
        public long LotTypeSequenceNumber { get; set; }

        [DataMember]
        public long SupplierSequenceNumber { get; set; }

        [DataMember]
        public long DepartmentPurchaseRequisitionTrailerSequenceNumber { get; set; }

        [DataMember]
        public long DepartmentPurchaseRequisitionHeaderOrderSequenceNumber { get; set; }

        [DataMember]
        public decimal CostPrice { get; set; }

        [DataMember]
        public decimal Tax { get; set; }


        [DataMember]
        public decimal TaxAmount { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }


        [DataMember]
        public decimal AvailableStock { get; set; }

        [DataMember]
        public decimal Allocated { get; set; }

        [DataMember]
        public string LotType { get; set; }

        [DataMember]
        public string Supplier { get; set; }

        [DataMember]
        public byte Status { get; set; }

        [DataMember]
        public bool ManagerApprovalStatus { get; set; }

        [DataMember]
        public decimal ManagerApprovedQuantity { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ManagerRejectReason { get; set; }

        [DataMember]
        public decimal ReceivedQuantity { get; set; }

        [DataMember]
        public string  TransitBefore { get; set; }


        [DataMember]
        public bool ManagerProcessedStatus { get; set; }

        [DataMember]
        public bool PurchaseOrderStatus { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
 
    }
}
