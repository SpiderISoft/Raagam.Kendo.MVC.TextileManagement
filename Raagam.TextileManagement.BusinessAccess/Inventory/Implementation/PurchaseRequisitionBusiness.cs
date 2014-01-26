using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class PurchaseRequisitionBusiness : IPurchaseRequisitionBusiness 
    {

        IPurchaseRequisitionDataAccess _purchaseRequisitionDataAccess;

        public PurchaseRequisitionBusiness()
        {
            _purchaseRequisitionDataAccess = new PurchaseRequisitionDataAccess();
        }

        #region IPurchaseRequisitionBusiness Members

        public PurchaseRequisitionHeaderModel PurReqPopulateDropDown()
        {
            return _purchaseRequisitionDataAccess.PurReqPopulateDropDown();
        }

        public PurchaseRequisitionHeaderModel SavePurchaseRequisition(PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel)
        {
            return _purchaseRequisitionDataAccess.SavePurchaseRequisition(purchaseRequisitionHeaderModel);
        }

        public PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber)
        {
            return _purchaseRequisitionDataAccess.SelectPurchaseRequisition(purchaseRequisitionNumber);
        }

        public PurchaseRequisitionHeaderModel SelectOrderForApproval(long orderNumber)
        {
            return _purchaseRequisitionDataAccess.SelectOrderForApproval(orderNumber);
        }
        #endregion
    }
}
