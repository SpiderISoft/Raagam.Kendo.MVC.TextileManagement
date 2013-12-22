using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.BusinessAccess
{
    public interface IFabricCuttingChartBusiness
    {
        FabricCuttingChartModel GetOrderDetails(long OrderNumber);
    }
}
