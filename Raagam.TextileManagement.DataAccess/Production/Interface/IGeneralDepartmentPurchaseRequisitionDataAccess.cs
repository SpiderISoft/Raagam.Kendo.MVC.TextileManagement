using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface IGeneralDepartmentPurchaseRequisitionDataAccess
    {

        #region IGeneralDepartmentPurchaseRequisitionDataAccess Members

            GeneralDepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown();

            GeneralDepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel);

            GeneralDepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber);

        #endregion
    }
}
