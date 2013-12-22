using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.WCF.ServiceLibrary
{
    [ServiceContract]
    interface IFabricCuttingChart
    {
        [OperationContract]
        FabricCuttingChartModel GetOrderDetails(long OrderNumber);
    }
}
