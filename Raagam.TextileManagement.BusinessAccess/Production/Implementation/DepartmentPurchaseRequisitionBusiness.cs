using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class DepartmentPurchaseRequisitionBusiness : IDepartmentPurchaseRequisitionBusiness 
    {

        IDepartmentPurchaseRequisitionDataAccess _departmentPurchaseRequisitionDataAccess;

        public DepartmentPurchaseRequisitionBusiness()
        {
            _departmentPurchaseRequisitionDataAccess = new DepartmentPurchaseRequisitionDataAccess();
        }

        #region IDepartmentPurchaseRequisitionBusiness Members

        public DepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown()
        {
            return _departmentPurchaseRequisitionDataAccess.DeptPurReqPopulateDropDown();
        }

        public DepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(DepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel)
        {
            return _departmentPurchaseRequisitionDataAccess.SaveDepartmentPurchaseRequisition(departmentPurchaseRequisitionHeaderModel);
        }

        public DepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber)
        {
            return _departmentPurchaseRequisitionDataAccess.SelectDepartmentPurchaseRequisition(departmentPurchaseRequisitionNumber);
        }
        #endregion
    }
}
