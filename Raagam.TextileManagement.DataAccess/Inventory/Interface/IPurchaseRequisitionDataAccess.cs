using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface IPurchaseRequisitionDataAccess
    {

         #region IPurchaseRequisitionDataAccess Members

            PurchaseRequisitionHeaderModel PurReqPopulateDropDown();

            PurchaseRequisitionHeaderModel SavePurchaseRequisition(PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel);

            PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber);

        #endregion
    }
}
