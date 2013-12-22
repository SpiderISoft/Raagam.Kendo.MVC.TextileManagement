using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.BusinessAccess;

namespace Raagam.TextileManagement.WCF.ServiceLibrary
{
    public class FabricCuttingChart : IFabricCuttingChart
    {

        IFabricCuttingChartBusiness _fabricCuttingChartBusiness;

      

        #region IFabricCuttingChart Members

        public FabricCuttingChartModel GetOrderDetails(long OrderNumber)
        {
                _fabricCuttingChartBusiness = new FabricCuttingChartBusiness();
                return _fabricCuttingChartBusiness.GetOrderDetails(OrderNumber);
        }

        #endregion
    }
}
