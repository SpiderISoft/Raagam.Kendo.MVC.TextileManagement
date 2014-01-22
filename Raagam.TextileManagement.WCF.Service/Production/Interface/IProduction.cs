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
    interface IProduction
    {
        #region IFabricCuttingChart Members
            [OperationContract]
            FabricCuttingChartModel GetOrderDetails(long OrderNumber);
            [OperationContract]
            EnumConstants.SaveStatus SaveFabricCuttingChart(FabricCuttingChartModel fabricCuttingChartModel);
        #endregion

        #region IDepartmentPurchaseRequisition Members
            [OperationContract]
            DepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown();
            [OperationContract]
            DepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(DepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel);
            [OperationContract]
            DepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber);
        #endregion

        #region IGeneralDepartmentPurchaseRequisition Members
            [OperationContract]
            GeneralDepartmentPurchaseRequisitionHeaderModel GeneralDepartmentPurReqPopulateDropDown();
            [OperationContract]
            GeneralDepartmentPurchaseRequisitionHeaderModel SaveGeneralDepartmentPurchaseRequisition(GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel);
            [OperationContract]
            GeneralDepartmentPurchaseRequisitionHeaderModel SelectGeneralDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber);
        #endregion

        #region ISelectProductGrid  Members
        [OperationContract]
        List<SelectProductGridModel> SelectProductGrid(long SupplierSequenceNumber, string ProductName);

        #endregion
    }
}
