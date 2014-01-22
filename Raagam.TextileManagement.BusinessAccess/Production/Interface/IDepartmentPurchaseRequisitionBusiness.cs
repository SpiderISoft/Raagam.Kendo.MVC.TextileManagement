using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess
{
    public interface IDepartmentPurchaseRequisitionBusiness
    {

        #region IDepartmentPurchaseRequisition Members
            DepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown();
            DepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(DepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel);
            DepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber);
        #endregion
    }
}
