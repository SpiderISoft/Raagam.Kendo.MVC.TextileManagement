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
    public class FabricCuttingChartDataAccess : IFabricCuttingChartDataAccess 
    {

        IDBHelper _dbHelper;

        public FabricCuttingChartDataAccess()
        {
            _dbHelper = new DBHelper();
        }

        #region IFabricCuttingChartDataAccess Members

        public FabricCuttingChartModel GetOrderDetails(long OrderNumber)
        {
            FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();

            using (DbCommand getOrderDetailsCommand = _dbHelper.GetStoredProcCommand("rx_sel_fabric_cutting_chart_order"))
            {
                _dbHelper.AddInParameter(getOrderDetailsCommand, "@order_id",
                                     DbType.Int32, OrderNumber);

                using (DataSet getOrderDetailsDataSet = _dbHelper.ExecuteDataSet(getOrderDetailsCommand))
                {

                    fabricCuttingChartModel.BuyerDropDownList = new List<SelectListItem>();
                    fabricCuttingChartModel.StyleDropDownList = new List<SelectListItem>();
                    fabricCuttingChartModel.FabricLinkDropDownList = new List<LinkDropDownModel>();
                    fabricCuttingChartModel.PanelLinkDropDownList  = new List<LinkDropDownModel>();
                    fabricCuttingChartModel.StyleSizeLinkDropDownList = new List<LinkDropDownModel>();
                    fabricCuttingChartModel.orderDetailsModelList = new List<FabricCuttingChartOrderDetailsModel>();
                    fabricCuttingChartModel.fabricCuttingChartMainList = new List<FabricCuttingChartMainModel>();
                    fabricCuttingChartModel.fabricCuttingChartDetailList = new List<FabricCuttingChartDetailModel>();
                    fabricCuttingChartModel.panelColorModelList = new List<FabricCuttingChartPanelColorModel>();
                    
                    foreach(DataRow buyerDataRow in getOrderDetailsDataSet.Tables[0].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value =  buyerDataRow["buyer_id"].ToString();
                        dropDown.Text = buyerDataRow["buyer_name"].ToString();
                        dropDown.Selected = false;
                        fabricCuttingChartModel.BuyerDropDownList.Add(dropDown);
                    }

                    if (getOrderDetailsDataSet.Tables[1].Rows.Count > 0)
                    {
                        DataRow buyerDataRow = getOrderDetailsDataSet.Tables[1].Rows[0];
                        fabricCuttingChartModel.OrderDate = Convert.ToDateTime(buyerDataRow["order_date"]);
                        fabricCuttingChartModel.BuyerSequenceNumber = Convert.ToInt64 (buyerDataRow["order_buyer_id"]);
                        fabricCuttingChartModel.DeliveryDate = Convert.ToDateTime(buyerDataRow["order_delivery_date"]);
                        fabricCuttingChartModel.BuyerReferenceNumber = buyerDataRow["order_buyer_refno"].ToString();
                        fabricCuttingChartModel.OrderQuantity = Convert.ToInt16(buyerDataRow["order_excessqty"]);
                    }

                    foreach (DataRow buyerDataRow in getOrderDetailsDataSet.Tables[2].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value  =  buyerDataRow["style_id"].ToString();
                        dropDown.Text = buyerDataRow["style_name"].ToString();
                        dropDown.Selected = false;
                        fabricCuttingChartModel.StyleDropDownList.Add(dropDown);
                    }


                    foreach (DataRow fabricDataRow in getOrderDetailsDataSet.Tables[3].Rows)
                    {
                        LinkDropDownModel linkDropDown = new LinkDropDownModel();
                        linkDropDown.Key = Convert.ToInt64(fabricDataRow["style_fabric_id"]);
                        linkDropDown.Value = fabricDataRow["prod_name"].ToString();
                        linkDropDown.ForeignKey = Convert.ToInt64(fabricDataRow["style_fabric_style_id"]);
                        fabricCuttingChartModel.FabricLinkDropDownList.Add(linkDropDown);
                    }

                    foreach (DataRow panelDataRow in getOrderDetailsDataSet.Tables[4].Rows)
                    {
                        LinkDropDownModel linkDropDown = new LinkDropDownModel();
                        linkDropDown.Key = Convert.ToInt64(panelDataRow["style_panel_id"]);
                        linkDropDown.Value = panelDataRow["style_panel_name"].ToString();
                        linkDropDown.ForeignKey = Convert.ToInt64(panelDataRow["style_panel_style_id"]);
                        fabricCuttingChartModel.PanelLinkDropDownList.Add(linkDropDown);
                    }


                    foreach (DataRow styleSizeDataRow in getOrderDetailsDataSet.Tables[5].Rows)
                    {
                        LinkDropDownModel linkDropDown = new LinkDropDownModel();
                        linkDropDown.Key = Convert.ToInt64(styleSizeDataRow["order_details_style_size_id"]);
                        linkDropDown.Value = styleSizeDataRow["size_name"].ToString();
                        linkDropDown.ForeignKey = Convert.ToInt64(styleSizeDataRow["order_details_style_id"]);
                        fabricCuttingChartModel.StyleSizeLinkDropDownList.Add(linkDropDown);
                    }

                    foreach (DataRow orderDetailsDataRow in getOrderDetailsDataSet.Tables[5].Rows)
                    {
                        FabricCuttingChartOrderDetailsModel orderDetail = new FabricCuttingChartOrderDetailsModel();
                        orderDetail.StyleSizeSequenceNumber = Convert.ToInt64(orderDetailsDataRow["order_details_style_size_id"]);
                        orderDetail.SizeName = orderDetailsDataRow["size_name"].ToString();
                        orderDetail.StyleSequenceNumber = Convert.ToInt64(orderDetailsDataRow["order_details_style_id"]);
                        orderDetail.OrderExcessQty = Convert.ToInt16(orderDetailsDataRow["order_details_excessqty"]);
                        fabricCuttingChartModel.orderDetailsModelList.Add(orderDetail);
                    }

                    foreach (DataRow fabricCuttingChartMainDataRow in getOrderDetailsDataSet.Tables[6].Rows)
                    {
                        FabricCuttingChartMainModel fabricCuttingChartMain = new FabricCuttingChartMainModel();
                        fabricCuttingChartMain.SequenceNumber = Convert.ToInt64(fabricCuttingChartMainDataRow["fabric_cutting_chart_id"]);
                        fabricCuttingChartMain.OrderSequenceNumber = Convert.ToInt64(fabricCuttingChartMainDataRow["fabric_cutting_chart_order_id"]);
                        fabricCuttingChartMain.StyleFabricSequenceNumber = Convert.ToInt64(fabricCuttingChartMainDataRow["fabric_cutting_chart_style_fabric_id"]);
                        fabricCuttingChartMain.StyleSizeSequenceNumber = Convert.ToInt64(fabricCuttingChartMainDataRow["fabric_cutting_chart_style_size_id"]);
                        fabricCuttingChartMain.StylePanelName = fabricCuttingChartMainDataRow["style_panel_name"].ToString();
                        fabricCuttingChartMain.ProductName = fabricCuttingChartMainDataRow["prod_name"].ToString();
                        fabricCuttingChartMain.TableDia  = Convert.ToDecimal(fabricCuttingChartMainDataRow["fabric_cutting_chart_tabledia"]);
                        fabricCuttingChartMain.KnitDia = Convert.ToDecimal(fabricCuttingChartMainDataRow["fabric_cutting_chart_knitdia"]);
                        fabricCuttingChartMain.SizeName = fabricCuttingChartMainDataRow["size_name"].ToString();
                        fabricCuttingChartMain.Pieces = Convert.ToDecimal(fabricCuttingChartMainDataRow["fabric_cutting_chart_pieces"]);
                        fabricCuttingChartMain.Weight  = Convert.ToDecimal(fabricCuttingChartMainDataRow["fabric_cutting_chart_weight"]);
                        fabricCuttingChartMain.WastagePercentage = Convert.ToDecimal(fabricCuttingChartMainDataRow["fabric_cutting_chart_wastage_percentage"]);
                        fabricCuttingChartMain.TotalWeight  = Convert.ToDecimal(fabricCuttingChartMainDataRow["fabric_cutting_chart_total_weight"]);
                        fabricCuttingChartMain.TempGUID = fabricCuttingChartMainDataRow["fabric_cutting_chart_temp_id"].ToString();
                        fabricCuttingChartMain.StyleColorSequenceNumber = Convert.ToInt64(fabricCuttingChartMainDataRow["fabric_cutting_chart_style_color_id"]);
                        fabricCuttingChartMain.ColorName = fabricCuttingChartMainDataRow["color_name"].ToString();
                        fabricCuttingChartMain.LoopLength = fabricCuttingChartMainDataRow["fabric_cutting_chart_looplength"].ToString();
                        fabricCuttingChartMain.KnitGSM  = fabricCuttingChartMainDataRow["fabric_cutting_chart_knitgsm"].ToString();
                        fabricCuttingChartMain.State = EnumConstants.ModelCurrentState.UnChanged;
                        fabricCuttingChartModel.fabricCuttingChartMainList.Add(fabricCuttingChartMain);
                    }


                    foreach (DataRow fabricCuttingChartDetailDataRow in getOrderDetailsDataSet.Tables[7].Rows)
                    {
                        FabricCuttingChartDetailModel  fabricCuttingChartDetail = new FabricCuttingChartDetailModel();
                        fabricCuttingChartDetail.SequenceNumber  = Convert.ToInt64(fabricCuttingChartDetailDataRow["fabric_cutting_chart_details_id"]);
                        fabricCuttingChartDetail.FabricCuttingChartMainSequenceNumber = Convert.ToInt64(fabricCuttingChartDetailDataRow["fabric_cutting_chart_details_fabric_cutting_chart_id"]);
                        fabricCuttingChartDetail.StylePanelSequenceNumber = Convert.ToInt64(fabricCuttingChartDetailDataRow["fabric_cutting_chart_details_style_panel_id"]);
                        fabricCuttingChartDetail.PanelName  = fabricCuttingChartDetailDataRow["style_panel_name"].ToString();
                        fabricCuttingChartDetail.TempGUID = fabricCuttingChartDetailDataRow["fabric_cutting_chart_details_temp_id"].ToString();
                        fabricCuttingChartDetail.State = EnumConstants.ModelCurrentState.UnChanged;
                        fabricCuttingChartModel.fabricCuttingChartDetailList.Add(fabricCuttingChartDetail);
                    }


                    foreach (DataRow fabricCuttingChartPanelColorDataRow in getOrderDetailsDataSet.Tables[8].Rows)
                    {
                        FabricCuttingChartPanelColorModel fabricCuttingChartPanelColor = new FabricCuttingChartPanelColorModel();
                        fabricCuttingChartPanelColor.StyleSequenceNumber = Convert.ToInt64(fabricCuttingChartPanelColorDataRow["order_details_style_id"]);
                        fabricCuttingChartPanelColor.StylePanelSequenceNumber = Convert.ToInt64(fabricCuttingChartPanelColorDataRow["style_panel_id"]);
                        fabricCuttingChartPanelColor.StyleSizeSequenceNumber = Convert.ToInt64(fabricCuttingChartPanelColorDataRow["order_details_style_size_id"]);
                        fabricCuttingChartPanelColor.SizeName = fabricCuttingChartPanelColorDataRow["size_name"].ToString();
                        fabricCuttingChartPanelColor.StyleColorSequenceNumber  = Convert.ToInt64(fabricCuttingChartPanelColorDataRow["style_color_id"]);
                        fabricCuttingChartPanelColor.ColorName = fabricCuttingChartPanelColorDataRow["color_name"].ToString();
                        fabricCuttingChartPanelColor.OrderExcessQty = Convert.ToInt16(fabricCuttingChartPanelColorDataRow["order_details_excessqty"]);
                        fabricCuttingChartPanelColor.TempGUID = Guid.NewGuid().ToString();

                        fabricCuttingChartModel.panelColorModelList.Add(fabricCuttingChartPanelColor);
                    }
                }
            }
            return fabricCuttingChartModel;
        }

        #endregion
    }
}
