using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class FabricCuttingChartBusiness : IFabricCuttingChartBusiness 
    {

        IFabricCuttingChartDataAccess _fabricCuttingChartDataAccess;

        public FabricCuttingChartBusiness()
        {
            _fabricCuttingChartDataAccess = new  FabricCuttingChartDataAccess();
        }

        #region IFabricCuttingChartBusiness Members

        public FabricCuttingChartModel GetOrderDetails(long OrderNumber)
        {
            return _fabricCuttingChartDataAccess.GetOrderDetails(OrderNumber);
        }

        public EnumConstants.SaveStatus SaveFabricCuttingChart(FabricCuttingChartModel fabricCuttingChartModel)
        {
            return _fabricCuttingChartDataAccess.SaveFabricCuttingChart(fabricCuttingChartModel);
        }
        #endregion
    }
}
