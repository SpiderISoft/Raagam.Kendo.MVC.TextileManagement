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

        public OrderMainModel GetOrderDetails(long OrderNumber)
        {
            OrderMainModel orderMainModel = new OrderMainModel();
            orderMainModel.OrderDetailModelList = new List<OrderDetailModel>();
            orderMainModel.StyleColorList = new List<OrderStyleColorModel>();
            orderMainModel.StyleSizeList = new List<OrderStyleSizeModel>();

            using (DbCommand getOrderCommand = _dbHelper.GetStoredProcCommand("rx_sel_order"))
            {
                _dbHelper.AddInParameter(getOrderCommand, "@order_id", DbType.Int64, OrderNumber);

                using (DataSet OrderDataSet = _dbHelper.ExecuteDataSet(getOrderCommand))
                {
                    foreach (DataRow orderMainDataRow in OrderDataSet.Tables[0].Rows)
                    {
                        orderMainModel.OrderNumber = long.Parse(orderMainDataRow["order_id"].ToString());
                        orderMainModel.OrderDate  =  DateTime.Parse(orderMainDataRow["order_date"].ToString());
                        orderMainModel.BuyerSequenceNumber = long.Parse(orderMainDataRow["order_buyer_id"].ToString());
                        orderMainModel.DeliveryDate = DateTime.Parse(orderMainDataRow["order_delivery_date"].ToString());
                        orderMainModel.BuyerReferenceNumber = orderMainDataRow["order_buyer_refno"].ToString();
                        orderMainModel.OrderQuantity = decimal.Parse(orderMainDataRow["order_qty"].ToString());
                        orderMainModel.PackingType = orderMainDataRow["order_packing_type"].ToString();
                        orderMainModel.PackingInstructions = orderMainDataRow["order_packing_instructions"].ToString();
                        orderMainModel.AssortmentDetails  = orderMainDataRow["order_assortment_details"].ToString();
                        orderMainModel.Comments  = orderMainDataRow["order_comments"].ToString();
                        orderMainModel.ExcessPercentage  = decimal.Parse(orderMainDataRow["order_excess"].ToString());
                        orderMainModel.IsCompleted = bool.Parse(orderMainDataRow["order_iscompleted"].ToString());
                        orderMainModel.Mode = EnumConstants.ScreenMode.Edit;

                    }

                    foreach (DataRow orderDetailDataRow in OrderDataSet.Tables[1].Rows)
                    {
                        OrderDetailModel orderDetailModel = new OrderDetailModel();
                        orderDetailModel.SequenceNumber = int.Parse(orderDetailDataRow["order_details_id"].ToString());
                        orderDetailModel.OrderNumber = int.Parse(orderDetailDataRow["order_details_order_id"].ToString());
                        orderDetailModel.StyleSequenceNumber = int.Parse(orderDetailDataRow["order_details_style_id"].ToString());
                        orderDetailModel.StyleColorSequenceNumber  = int.Parse(orderDetailDataRow["order_details_style_color_id"].ToString());
                        orderDetailModel.StyleSizeSequenceNumber  = int.Parse(orderDetailDataRow["order_details_style_size_id"].ToString());
                        orderDetailModel.OrderDetailQuantity = decimal.Parse(orderDetailDataRow["order_details_qty"].ToString());
                        orderMainModel.OrderDetailModelList.Add(orderDetailModel);
                    }

                    foreach (DataRow colorDataRow in OrderDataSet.Tables[2].Rows)
                    {
                        OrderStyleColorModel styleColorModel = new OrderStyleColorModel();
                        styleColorModel.StyleColorSequence = Convert.ToInt64(colorDataRow["style_color_id"]);
                        styleColorModel.ColorName = colorDataRow["color_name"].ToString();
                        orderMainModel.StyleColorList.Add(styleColorModel);
                    }

                    foreach (DataRow sizeDataRow in OrderDataSet.Tables[3].Rows)
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

        public long SaveOrder(OrderMainModel orderMainModel)
        {

            if (orderMainModel.Mode == EnumConstants.ScreenMode.New)
            {
                List<OrderDetailModel> orderDetailModel = new List<OrderDetailModel>();
                orderDetailModel = orderMainModel.OrderDetailModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();

                DataTable orderDetailDataTable = UtilityMethods.ToDataTable(orderDetailModel);

                using (DbCommand saveOrderMainCommand = _dbHelper.GetStoredProcCommand("rx_ins_order"))
                {

                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_date", DbType.DateTime, orderMainModel.OrderDate);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_buyer_id", DbType.Int64, orderMainModel.BuyerSequenceNumber);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_delivery_date", DbType.DateTime, orderMainModel.DeliveryDate);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_buyer_refno", DbType.String, orderMainModel.BuyerReferenceNumber);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_qty", DbType.Int64, orderMainModel.OrderQuantity);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_packing_type", DbType.String, orderMainModel.PackingType);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_packing_instructions", DbType.String, orderMainModel.PackingInstructions);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_assortment_details", DbType.String, orderMainModel.AssortmentDetails);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_comments", DbType.String, orderMainModel.Comments);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_iscompleted", DbType.Boolean, orderMainModel.IsCompleted);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_excess", DbType.Int16, orderMainModel.ExcessPercentage);

                    IDataReader dbReader = _dbHelper.ExecuteReader(saveOrderMainCommand);
                    dbReader.Read();
                    orderMainModel.OrderNumber = long.Parse(dbReader["order_id"].ToString());


                }

                using (DbCommand saveOrderDetailCommand = _dbHelper.GetStoredProcCommand("rx_ins_order_details"))
                {


                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_order_id", DbType.Int64, orderMainModel.OrderNumber);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_style_id", DbType.Int64, "StyleSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_style_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_style_size_id", DbType.Int64, "StyleSizeSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_qty", DbType.Int64, "OrderDetailQuantity", DataRowVersion.Current);

                    _dbHelper.Fill(saveOrderDetailCommand, orderDetailDataTable);

                }
            }
            else
            {
                List<OrderDetailModel> orderDetailModel = new List<OrderDetailModel>();
                orderDetailModel = orderMainModel.OrderDetailModelList;

                DataTable orderDetailDataTable = UtilityMethods.ToDataTable(orderDetailModel);

                using (DbCommand saveOrderMainCommand = _dbHelper.GetStoredProcCommand("rx_upd_order"))
                {

                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_id", DbType.Int64, orderMainModel.OrderNumber );
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_date", DbType.DateTime, orderMainModel.OrderDate);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_buyer_id", DbType.Int64, orderMainModel.BuyerSequenceNumber);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_delivery_date", DbType.DateTime, orderMainModel.DeliveryDate);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_buyer_refno", DbType.String, orderMainModel.BuyerReferenceNumber);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_qty", DbType.Int64, orderMainModel.OrderQuantity);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_packing_type", DbType.String, orderMainModel.PackingType);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_packing_instructions", DbType.String, orderMainModel.PackingInstructions);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_assortment_details", DbType.String, orderMainModel.AssortmentDetails);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_comments", DbType.String, orderMainModel.Comments);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_iscompleted", DbType.Boolean, orderMainModel.IsCompleted);
                    _dbHelper.AddInParameter(saveOrderMainCommand, "@order_excess", DbType.Int16, orderMainModel.ExcessPercentage);

                    _dbHelper.ExecuteNonQuery(saveOrderMainCommand);
                    

                }

                using (DbCommand saveOrderDetailCommand = _dbHelper.GetStoredProcCommand("rx_upd_order_details"))
                {

                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_id", DbType.Int64, "SequenceNumber",DataRowVersion.Current );
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_order_id", DbType.Int64, orderMainModel.OrderNumber);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_style_id", DbType.Int64, "StyleSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_style_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_style_size_id", DbType.Int64, "StyleSizeSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveOrderDetailCommand, "@order_details_qty", DbType.Int64, "OrderDetailQuantity", DataRowVersion.Current);

                    _dbHelper.Fill(saveOrderDetailCommand, orderDetailDataTable);

                }

            }

            if (orderMainModel.IsCompleted)
            {
                using (DbCommand saveTrimScheduleCommand = _dbHelper.GetStoredProcCommand("rx_ins_trims_schedule"))
                {
                    _dbHelper.AddInParameter(saveTrimScheduleCommand, "@order_id", DbType.Int64, orderMainModel.OrderNumber);

                    _dbHelper.ExecuteNonQuery(saveTrimScheduleCommand);

                }
            }
            return orderMainModel.OrderNumber;
        }

        #endregion
}
}
