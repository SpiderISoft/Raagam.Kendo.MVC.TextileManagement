using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;
using System.Data.Common;
using System.Data;
using System.Web.Mvc;

namespace Raagam.TextileManagement.DataAccess
{
    public class OrderDataAccess: IOrderDataAccess 

    {

         IDBHelper _dbHelper;

         public OrderDataAccess()
        {
            _dbHelper = new DBHelper();
        }

    
        #region IOrderDataAccess Members

        public OrderMainModel  PopulateDropDown(OrderMainModel orderMainModel)
        {
            orderMainModel.BuyerDropDownList = new List<SelectListItem>();
            orderMainModel.StyleDropDownList = new List<SelectListItem>();
            using (DbCommand getDropDownCommand = _dbHelper.GetStoredProcCommand("rx_lst_order"))
            {
                using (DataSet DropDownDataSet = _dbHelper.ExecuteDataSet(getDropDownCommand))
                {
                    foreach (DataRow styleDataRow in DropDownDataSet.Tables[0].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = styleDataRow["style_id"].ToString();
                        dropDown.Text = styleDataRow["style_name"].ToString();
                        dropDown.Selected = false;
                        orderMainModel.StyleDropDownList.Add(dropDown);
                    }

                    foreach (DataRow buyerDataRow in DropDownDataSet.Tables[1].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = buyerDataRow["buyer_id"].ToString();
                        dropDown.Text = buyerDataRow["buyer_name"].ToString();
                        dropDown.Selected = false;
                        orderMainModel.BuyerDropDownList.Add(dropDown);
                    }
                }
            }
            return orderMainModel;
        }

        #endregion
}
}
