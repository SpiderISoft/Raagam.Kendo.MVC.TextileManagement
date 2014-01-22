using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface IDepartmentPurchaseRequisitionDataAccess
    {

         #region IDepartmentPurchaseRequisitionDataAccess Members

            DepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown();

            DepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(DepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel);

            DepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber);

        #endregion
    }
}
