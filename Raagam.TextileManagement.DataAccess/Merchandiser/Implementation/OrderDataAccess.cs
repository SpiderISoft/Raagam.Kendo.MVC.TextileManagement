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

        public OrderMainModel SelectColorSize(long StyleSequenceNumber)
        {
            OrderMainModel orderMainModel = new OrderMainModel();
            orderMainModel.StyleColorList = new List<OrderStyleColorModel>();
            orderMainModel.StyleSizeList = new List<OrderStyleSizeModel>();

            using (DbCommand colorSizeCommand = _dbHelper.GetStoredProcCommand("rx_sel_style_color_size"))
            {

                _dbHelper.AddInParameter(colorSizeCommand, "@style_id",
                              DbType.Int64, StyleSequenceNumber);

                using (DataSet DropDownDataSet = _dbHelper.ExecuteDataSet(colorSizeCommand))
                {
                    foreach (DataRow colorDataRow in DropDownDataSet.Tables[0].Rows)
                    {
                        OrderStyleColorModel styleColorModel = new OrderStyleColorModel();
                        styleColorModel.StyleColorSequence = Convert.ToInt64(colorDataRow["style_color_id"]);
                        styleColorModel.ColorName = colorDataRow["color_name"].ToString();
                        orderMainModel.StyleColorList.Add(styleColorModel);
                    }

                    foreach (DataRow sizeDataRow in DropDownDataSet.Tables[1].Rows)
                    {
                        OrderStyleSizeModel styleSizeModel = new OrderStyleSizeModel();
                        styleSizeModel.StyleSizeSequence = Convert.ToInt64(sizeDataRow["style_size_id"]);
                        styleSizeModel.SizeName = sizeDataRow["size_name"].ToString();
                        orderMainModel.StyleSizeList.Add(styleSizeModel);
                    }
 
                }
            }

            return orderMainModel;
            
        }

        #endregion
}
}
