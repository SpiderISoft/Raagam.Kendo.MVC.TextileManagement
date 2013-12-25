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
    }
}
