using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Raagam.TextileManagement.BusinessAccess;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.WCF.Service
{
     
    public class Inventory : IInventory
    {
        #region IPurchaseRequisition Members

        IPurchaseRequisitionBusiness _purchaseRequisitionBusiness;

        public PurchaseRequisitionHeaderModel PurReqPopulateDropDown()
        {
            _purchaseRequisitionBusiness = new PurchaseRequisitionBusiness();
            return _purchaseRequisitionBusiness.PurReqPopulateDropDown();
        }
        public PurchaseRequisitionHeaderModel SavePurchaseRequisition(PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel)
        {
            _purchaseRequisitionBusiness = new PurchaseRequisitionBusiness();
            return _purchaseRequisitionBusiness.SavePurchaseRequisition(purchaseRequisitionHeaderModel);
        }

        public PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber)
        {
            _purchaseRequisitionBusiness = new PurchaseRequisitionBusiness();
            return _purchaseRequisitionBusiness.SelectPurchaseRequisition(purchaseRequisitionNumber);

        }
        #endregion
    }
}
