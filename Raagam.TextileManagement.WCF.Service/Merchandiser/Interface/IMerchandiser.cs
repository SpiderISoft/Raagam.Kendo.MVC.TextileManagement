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
    }
}
