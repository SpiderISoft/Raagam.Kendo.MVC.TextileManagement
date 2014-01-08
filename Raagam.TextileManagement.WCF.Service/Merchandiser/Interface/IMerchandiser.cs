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
    public interface IMerchandiser
    {
        [OperationContract]
        OrderMainModel PopulateDropDown(OrderMainModel orderMainModel);

        [OperationContract]
        OrderMainModel SelectColorSize(long StyleSequenceNumber);

        [OperationContract]
        long SaveOrder(OrderMainModel orderMainModel);

        [OperationContract]
        OrderMainModel GetOrderDetails(long OrderNumber);

        [OperationContract]
        StyleListModel StylePopulateDropDown();

        [OperationContract]
        StyleListModel SaveStyle(StyleModel styleModel);

        [OperationContract]
        StyleModel EditStyle(long styleSequenceNumber);
    }
}
