using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.BusinessAccess;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.WCF.Service 
{
    public class Production : IProduction
    {



        #region IFabricCuttingChart Members
        
        IFabricCuttingChartBusiness _fabricCuttingChartBusiness;
        
        public FabricCuttingChartModel GetOrderDetails(long OrderNumber)
        {
            _fabricCuttingChartBusiness = new FabricCuttingChartBusiness();
            return _fabricCuttingChartBusiness.GetOrderDetails(OrderNumber);
        }

        public EnumConstants.SaveStatus SaveFabricCuttingChart(FabricCuttingChartModel fabricCuttingChartModel)
        {
            _fabricCuttingChartBusiness = new FabricCuttingChartBusiness();
            return _fabricCuttingChartBusiness.SaveFabricCuttingChart(fabricCuttingChartModel);
        }

        #endregion


        #region IDepartmentPurchaseRequisition Members

        IDepartmentPurchaseRequisitionBusiness _departmentPurchaseRequisitionBusiness;

        public DepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown()
        {
            _departmentPurchaseRequisitionBusiness = new  DepartmentPurchaseRequisitionBusiness();
            return _departmentPurchaseRequisitionBusiness.DeptPurReqPopulateDropDown();
        }
        public DepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(DepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel)
        {
            _departmentPurchaseRequisitionBusiness = new DepartmentPurchaseRequisitionBusiness();
            return _departmentPurchaseRequisitionBusiness.SaveDepartmentPurchaseRequisition(departmentPurchaseRequisitionHeaderModel);
        }

        public DepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber)
        {
            _departmentPurchaseRequisitionBusiness = new DepartmentPurchaseRequisitionBusiness();
            return _departmentPurchaseRequisitionBusiness.SelectDepartmentPurchaseRequisition(departmentPurchaseRequisitionNumber);

        }
        #endregion

        #region IGeneralDepartmentPurchaseRequisition Members

        IGeneralDepartmentPurchaseRequisitionBusiness _generalDepartmentPurchaseRequisitionBusiness;

        public GeneralDepartmentPurchaseRequisitionHeaderModel GeneralDepartmentPurReqPopulateDropDown()
        {
            _generalDepartmentPurchaseRequisitionBusiness = new GeneralDepartmentPurchaseRequisitionBusiness();
            return _generalDepartmentPurchaseRequisitionBusiness.DeptPurReqPopulateDropDown();
        }
        public GeneralDepartmentPurchaseRequisitionHeaderModel SaveGeneralDepartmentPurchaseRequisition(GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel)
        {
            _generalDepartmentPurchaseRequisitionBusiness = new GeneralDepartmentPurchaseRequisitionBusiness();
            return _generalDepartmentPurchaseRequisitionBusiness.SaveDepartmentPurchaseRequisition(departmentPurchaseRequisitionHeaderModel);
        }

        public GeneralDepartmentPurchaseRequisitionHeaderModel SelectGeneralDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber)
        {
            _generalDepartmentPurchaseRequisitionBusiness = new GeneralDepartmentPurchaseRequisitionBusiness();
            return _generalDepartmentPurchaseRequisitionBusiness.SelectDepartmentPurchaseRequisition(departmentPurchaseRequisitionNumber);

        }
        #endregion
        #region ISelectProductGrid Members

        ISelectProductGridBusiness _selectProductGridBusiness;

        public List<SelectProductGridModel> SelectProductGrid(long SupplierSequenceNumber, string ProductName)
        {
            _selectProductGridBusiness = new SelectProductGridBusiness();
            return _selectProductGridBusiness.SelectProductGrid(SupplierSequenceNumber,ProductName);
        }

        #endregion
    }
}
