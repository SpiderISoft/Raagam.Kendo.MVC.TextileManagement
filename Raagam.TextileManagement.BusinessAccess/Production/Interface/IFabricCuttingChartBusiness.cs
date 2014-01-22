using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess
{
    public interface IFabricCuttingChartBusiness
    {
        #region IFabricCuttingChart Members
            FabricCuttingChartModel GetOrderDetails(long OrderNumber);
            EnumConstants.SaveStatus SaveFabricCuttingChart(FabricCuttingChartModel fabricCuttingChartModel);
        #endregion
 
    }
}
