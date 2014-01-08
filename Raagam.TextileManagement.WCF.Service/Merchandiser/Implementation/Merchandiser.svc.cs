using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Raagam.TextileManagement.BusinessAccess;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Merchandiser" in code, svc and config file together.
    public class Merchandiser : IMerchandiser
    {

        #region IMerchandiser Members

        IOrderBusiness _orderBusiness;

        IStyleBusiness _styleBusiness;

        public OrderMainModel PopulateDropDown(OrderMainModel orderMainModel)
        {
            _orderBusiness = new OrderBusiness();
            return _orderBusiness.PopulateDropDown(orderMainModel);
        }

        public OrderMainModel SelectColorSize(long StyleSequenceNumber)
        {
            _orderBusiness = new OrderBusiness();
            return _orderBusiness.SelectColorSize(StyleSequenceNumber);
        }

        public long SaveOrder(OrderMainModel orderMainModel)
        {
            _orderBusiness = new OrderBusiness();
            return _orderBusiness.SaveOrder(orderMainModel);
        }

        public OrderMainModel GetOrderDetails(long OrderNumber)
        {
            _orderBusiness = new OrderBusiness();
            return _orderBusiness.GetOrderDetails(OrderNumber);
        }

        public StyleListModel StylePopulateDropDown()
        {
            _styleBusiness = new StyleBusiness();
            return _styleBusiness.PopulateDropDown();
        }

        public StyleListModel SaveStyle(StyleModel styleModel)
        {
            _styleBusiness = new StyleBusiness();
            return _styleBusiness.SaveStyle(styleModel);
        }

        public StyleModel EditStyle(long styleSequenceNumber)
        {
            _styleBusiness = new StyleBusiness();
            return _styleBusiness.EditStyle(styleSequenceNumber);
        }
        #endregion
    }
}
