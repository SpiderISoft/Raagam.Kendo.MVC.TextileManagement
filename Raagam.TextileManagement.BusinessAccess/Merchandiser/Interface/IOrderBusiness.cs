using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public interface IOrderBusiness
    {
        OrderMainModel PopulateDropDown(OrderMainModel orderMainModel);
        OrderMainModel  SelectColorSize(long StyleSequenceNumber);
        long SaveOrder(OrderMainModel orderMainModel);
        OrderMainModel GetOrderDetails(long OrderNumber);
    }
}
