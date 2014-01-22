using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.WCF.Service
{
    [ServiceContract]
    interface IInventory
    {
        #region IPurchaseRequisition Members
            [OperationContract]
            PurchaseRequisitionHeaderModel PurReqPopulateDropDown();
            [OperationContract]
            PurchaseRequisitionHeaderModel SavePurchaseRequisition(PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel);
            [OperationContract]
            PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber);
        #endregion
 
    }
}
