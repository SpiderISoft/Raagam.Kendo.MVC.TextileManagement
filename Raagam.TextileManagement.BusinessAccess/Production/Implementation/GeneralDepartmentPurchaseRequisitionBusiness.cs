using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class GeneralDepartmentPurchaseRequisitionBusiness : IGeneralDepartmentPurchaseRequisitionBusiness 
    {

        IGeneralDepartmentPurchaseRequisitionDataAccess _departmentPurchaseRequisitionDataAccess;

        public GeneralDepartmentPurchaseRequisitionBusiness()
        {
            _departmentPurchaseRequisitionDataAccess = new GeneralDepartmentPurchaseRequisitionDataAccess();
        }

        #region IGeneralDepartmentPurchaseRequisitionBusiness Members

        public GeneralDepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown()
        {
            return _departmentPurchaseRequisitionDataAccess.DeptPurReqPopulateDropDown();
        }

        public GeneralDepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel)
        {
            return _departmentPurchaseRequisitionDataAccess.SaveDepartmentPurchaseRequisition(departmentPurchaseRequisitionHeaderModel);
        }

        public GeneralDepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber)
        {
            return _departmentPurchaseRequisitionDataAccess.SelectDepartmentPurchaseRequisition(departmentPurchaseRequisitionNumber);
        }
        #endregion
    }
}
