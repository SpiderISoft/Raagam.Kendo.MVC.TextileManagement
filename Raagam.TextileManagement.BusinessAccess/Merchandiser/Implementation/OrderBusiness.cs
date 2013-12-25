using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class OrderBusiness : IOrderBusiness
    {
         IOrderDataAccess _orderDataAccess;

         public OrderBusiness()
        {
            _orderDataAccess = new  OrderDataAccess();
        }

        public OrderMainModel PopulateDropDown(OrderMainModel orderMainModel)
        {
            return _orderDataAccess.PopulateDropDown(orderMainModel);
        }

        public OrderMainModel SelectColorSize(long StyleSequenceNumber)
        {
            return _orderDataAccess.SelectColorSize(StyleSequenceNumber);
        }
    }
}
