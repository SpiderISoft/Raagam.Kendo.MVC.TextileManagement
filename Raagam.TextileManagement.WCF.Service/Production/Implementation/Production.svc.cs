using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.BusinessAccess;

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

        #endregion
    }
}
