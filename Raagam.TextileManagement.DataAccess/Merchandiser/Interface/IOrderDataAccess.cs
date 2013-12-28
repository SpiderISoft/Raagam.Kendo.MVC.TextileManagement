using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface IOrderDataAccess
    {

        OrderMainModel PopulateDropDown(OrderMainModel orderMainModel);
        OrderMainModel SelectColorSize(long StyleSequenceNumber);
        long SaveOrder(OrderMainModel orderMainModel);
        OrderMainModel GetOrderDetails(long OrderNumber);
        
    }
}
