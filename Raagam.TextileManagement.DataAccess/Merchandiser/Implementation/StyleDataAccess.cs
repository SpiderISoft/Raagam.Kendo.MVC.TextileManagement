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
    public class StyleDataAccess: IStyleDataAccess 

    {

         IDBHelper _dbHelper;

         public StyleDataAccess()
        {
            _dbHelper = new DBHelper();
        }

    
        #region IStyleDataAccess Members

         public StyleListModel PopulateDropDown()
        {

            StyleListModel styleListModel = new StyleListModel();
          
            

            using (DbCommand getDropDownCommand = _dbHelper.GetStoredProcCommand("rx_lst_style"))
            {
                using (DataSet DropDownDataSet = _dbHelper.ExecuteDataSet(getDropDownCommand))
                {
                    foreach (DataRow processDataRow in DropDownDataSet.Tables[0].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = processDataRow["process_id"].ToString();
                        dropDown.Text = processDataRow["process_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.ProcessDropDownList.Add(dropDown);
                    }

                    foreach (DataRow productGroupDataRow in DropDownDataSet.Tables[1].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = productGroupDataRow["prodgrp_seqno"].ToString();
                        dropDown.Text = productGroupDataRow["prodgrp_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.ProductGroupDropDownList.Add(dropDown);
                    }
                    foreach (DataRow productDataRow in DropDownDataSet.Tables[2].Rows)
                    {
                        LinkDropDownModel dropDown = new LinkDropDownModel();
                        dropDown.Key = long.Parse(productDataRow["prod_seqno"].ToString());
                        dropDown.Value = productDataRow["prod_name"].ToString();
                        dropDown.ForeignKey = long.Parse(productDataRow["prod_productgroup"].ToString());
                        styleListModel.ProductDropDownList.Add(dropDown);
                    }
                    foreach (DataRow colorDataRow in DropDownDataSet.Tables[3].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = colorDataRow["color_id"].ToString();
                        dropDown.Text = colorDataRow["color_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.ColorDropDownList.Add(dropDown);
                    }

                    foreach (DataRow colorDataRow in DropDownDataSet.Tables[4].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = colorDataRow["size_id"].ToString();
                        dropDown.Text = colorDataRow["size_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.SizeDropDownList.Add(dropDown);
                    }

                    foreach (DataRow fabricDataRow in DropDownDataSet.Tables[5].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = fabricDataRow["prod_seqno"].ToString();
                        dropDown.Text = fabricDataRow["prod_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.FabricDropDownList.Add(dropDown);
                    }

                    foreach (DataRow styleDataRow in DropDownDataSet.Tables[6].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = styleDataRow["style_id"].ToString();
                        dropDown.Text = styleDataRow["style_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.StyleDropDownList.Add(dropDown);
                    }

                    foreach (DataRow styleTypeDataRow in DropDownDataSet.Tables[7].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = styleTypeDataRow["style_type_id"].ToString();
                        dropDown.Text = styleTypeDataRow["style_type_name"].ToString();
                        dropDown.Selected = false;
                        styleListModel.StyleTypeDropDownList.Add(dropDown);
                    }
                    foreach (DataRow lotTypeDataRow in DropDownDataSet.Tables[9].Rows)
                    {
                        LinkDropDownModel dropDown = new LinkDropDownModel();
                        dropDown.Key = long.Parse(lotTypeDataRow["lottyp_seqno"].ToString());
                        dropDown.Value = lotTypeDataRow["lottyp_name"].ToString();
                        dropDown.ForeignKey = long.Parse(lotTypeDataRow["prod_seqno"].ToString());
                        styleListModel.LotTypeDropDownList.Add(dropDown);
                    }
                    foreach (DataRow styleDetailDataRow in DropDownDataSet.Tables[10].Rows)
                    {
                        StyleModel styleDetailModel = new StyleModel();
                        styleDetailModel.StyleSequenceNumber = long.Parse(styleDetailDataRow["style_id"].ToString());
                        styleDetailModel.StyleName = styleDetailDataRow["style_name"].ToString();
                        styleDetailModel.StyleDescription = styleDetailDataRow["style_description"].ToString();
                        styleDetailModel.StyleTypeSequenceNumber = long.Parse(styleDetailDataRow["style_style_type_id"].ToString());
                        styleDetailModel.StyleTypeName = styleDetailDataRow["style_type_name"].ToString();
                        styleDetailModel.IsCompleted = bool.Parse(styleDetailDataRow["style_completed"].ToString());
                        styleListModel.styleModelList.Add(styleDetailModel);
                    }
                }
            }

            return styleListModel;
        }

         public StyleModel EditStyle(long styleSequenceNumber)
         {
             StyleModel styleModel = new StyleModel();

             using (DbCommand getStyleCommand = _dbHelper.GetStoredProcCommand("rx_sel_id_style"))
             {
                 _dbHelper.AddInParameter(getStyleCommand, "@style_id", DbType.Int64, styleSequenceNumber);

                 using (DataSet StyleDataSet = _dbHelper.ExecuteDataSet(getStyleCommand))
                 {
                     foreach (DataRow styleDataRow in StyleDataSet.Tables[0].Rows)
                     {
                         styleModel.StyleSequenceNumber = long.Parse(styleDataRow["style_id"].ToString()) ;
                         styleModel.StyleName = styleDataRow["style_name"].ToString();
                         styleModel.StyleDescription = styleDataRow["style_description"].ToString();
                         styleModel.StyleTypeSequenceNumber = long.Parse(styleDataRow["style_style_type_id"].ToString());
                         styleModel.IsCompleted = bool.Parse(styleDataRow["style_completed"].ToString());
                         styleModel.Mode = EnumConstants.ScreenMode.Edit;
                     }

                     foreach (DataRow stylePanelDataRow in StyleDataSet.Tables[1].Rows)
                     {
                         StylePanelModel stylePanelModel = new StylePanelModel();

                         stylePanelModel.SequenceNumber = long.Parse(stylePanelDataRow["style_panel_id"].ToString());
                         stylePanelModel.PanelName = stylePanelDataRow["style_panel_name"].ToString();
                         stylePanelModel.PanelDescription = stylePanelDataRow["style_panel_description"].ToString();
                         stylePanelModel.StyleSequenceNumber = long.Parse(stylePanelDataRow["style_panel_style_id"].ToString());
                         stylePanelModel.TempGuid = stylePanelDataRow["style_panel_temp_id"].ToString();
                         stylePanelModel.State = EnumConstants.ModelCurrentState.UnChanged;


                         foreach (DataRow stylePanelProcessDataRow in StyleDataSet.Tables[2].Select("style_panel_process_style_panel_id=" + stylePanelModel.SequenceNumber))
                         {
                             StylePanelProcessModel stylePanelProcessModel = new StylePanelProcessModel();

                             stylePanelProcessModel.SequenceNumber = long.Parse(stylePanelProcessDataRow["style_panel_process_id"].ToString());
                             stylePanelProcessModel.ProcessSequenceNumber = long.Parse(stylePanelProcessDataRow["style_panel_process_process_id"].ToString());
                             stylePanelProcessModel.StylePanelSequenceNumber = long.Parse(stylePanelProcessDataRow["style_panel_process_style_panel_id"].ToString());
                             stylePanelProcessModel.TempGuid = stylePanelProcessDataRow["style_panel_process_style_panel_temp_id"].ToString();
                             stylePanelProcessModel.State = EnumConstants.ModelCurrentState.UnChanged;

                             stylePanelModel.SelectedPanelProcessList.Add(stylePanelProcessModel.ProcessSequenceNumber);
                             stylePanelModel.StylePanelProcessModelList.Add(stylePanelProcessModel);
                              
                         }

                         foreach (DataRow stylePanelColorDataRow in StyleDataSet.Tables[8].Select("style_panel_color_panel_id=" + stylePanelModel.SequenceNumber))
                         {
                             StylePanelColorModel stylePanelColorModel = new StylePanelColorModel();
                             stylePanelColorModel.SequenceNumber = long.Parse(stylePanelColorDataRow["style_panel_color_id"].ToString());
                             stylePanelColorModel.StylePanelSequenceNumber = long.Parse(stylePanelColorDataRow["style_panel_color_panel_id"].ToString());
                             stylePanelColorModel.StyleColorSequenceNumber = long.Parse(stylePanelColorDataRow["style_panel_color_style_color_id"].ToString());
                             stylePanelColorModel.ColorSequenceNumber = long.Parse(stylePanelColorDataRow["style_panel_color_color_id"].ToString());
                             stylePanelColorModel.TempGuid = stylePanelColorDataRow["style_panel_color_style_panel_temp_id"].ToString();
                             stylePanelColorModel.ColorPantone = stylePanelColorDataRow["style_panel_color_pantone"].ToString();
                             stylePanelColorModel.State = EnumConstants.ModelCurrentState.UnChanged;
                             stylePanelModel.StylePanelColorModelList.Add(stylePanelColorModel);

                         }

                         styleModel.StylePanelModelList.Add(stylePanelModel);
                     }


                     foreach (DataRow styleProcessSourcesDataRow in StyleDataSet.Tables[3].Rows)
                     {
                         StyleProcessSourcesModel styleProcessSourcesModel = new StyleProcessSourcesModel();
                         styleProcessSourcesModel.SequenceNumber = long.Parse(styleProcessSourcesDataRow["style_process_sources_id"].ToString());
                         styleProcessSourcesModel.SourcesSequenceNumber = long.Parse(styleProcessSourcesDataRow["style_process_sources_sources_id"].ToString());
                         styleProcessSourcesModel.ProductGroupSequenceNumber = long.Parse(styleProcessSourcesDataRow["prod_productgroup"].ToString());
                         styleProcessSourcesModel.ProcessSequenceNumber = long.Parse(styleProcessSourcesDataRow["style_process_sources_process_id"].ToString());
                         styleProcessSourcesModel.StyleSequenceNumber = long.Parse(styleProcessSourcesDataRow["style_process_sources_style_id"].ToString());
                         styleProcessSourcesModel.ProcessSourcesTempGuid = styleProcessSourcesDataRow["style_process_sources_temp_id"].ToString();
                         styleProcessSourcesModel.Quantity = decimal.Parse(styleProcessSourcesDataRow["style_process_sources_qty"].ToString());
                         styleProcessSourcesModel.LotSequenceNumber  = long.Parse(styleProcessSourcesDataRow["style_process_sources_lottype_seqno"].ToString());
                         styleProcessSourcesModel.IsSizeApplicable = bool.Parse(styleProcessSourcesDataRow["style_process_sources_sizeapplicable"].ToString());
                         styleProcessSourcesModel.Process = styleProcessSourcesDataRow["process_name"].ToString();
                         styleProcessSourcesModel.Sources = styleProcessSourcesDataRow["prod_name"].ToString();
                         styleProcessSourcesModel.UOM = styleProcessSourcesDataRow["lottyp_name"].ToString();
                         styleProcessSourcesModel.State = EnumConstants.ModelCurrentState.UnChanged;


                         foreach (DataRow styleProcessSourcesColorDataRow in StyleDataSet.Tables[6].Select("style_process_sources_color_style_process_sources_id=" + styleProcessSourcesModel.SequenceNumber))
                         {
                             StyleProccessSourcesColorModel styleProcessSourcesColorModel = new StyleProccessSourcesColorModel();
                             styleProcessSourcesColorModel.SequenceNumber = long.Parse(styleProcessSourcesColorDataRow["style_process_sources_color_id"].ToString());
                             styleProcessSourcesColorModel.StyleProcessSourcesSequenceNumber = long.Parse(styleProcessSourcesColorDataRow["style_process_sources_color_style_process_sources_id"].ToString());
                             styleProcessSourcesColorModel.StyleColorSequenceNumber = long.Parse(styleProcessSourcesColorDataRow["style_process_sources_color_primary_color_id"].ToString());
                             styleProcessSourcesColorModel.ColorSequenceNumber = long.Parse(styleProcessSourcesColorDataRow["style_process_sources_color_color_id"].ToString());
                             styleProcessSourcesColorModel.TempGuid = styleProcessSourcesColorDataRow["style_process_sources_color_style_process_sources_temp_id"].ToString();
                             styleProcessSourcesColorModel.State = EnumConstants.ModelCurrentState.UnChanged;
                             styleProcessSourcesModel.StyleProccessSourcesColorModelList.Add(styleProcessSourcesColorModel);
                         }

                         styleModel.StyleProcessSourcesModelList.Add(styleProcessSourcesModel);
                     }

                     foreach (DataRow colorDataRow in StyleDataSet.Tables[4].Rows)
                     {
                         StyleColorModel styleColorModel = new StyleColorModel();
                         styleColorModel.SequenceNumber = long.Parse(colorDataRow["style_color_id"].ToString());
                         styleColorModel.ColorSequenceNumber = long.Parse(colorDataRow["style_color_color_id"].ToString());
                         styleColorModel.StyleSequenceNumber = long.Parse(colorDataRow["style_color_style_id"].ToString());
                         styleColorModel.ColorPantone  = colorDataRow["style_color_pantone"].ToString();
                         styleColorModel.ColorName = colorDataRow["color_name"].ToString();
                         styleColorModel.State = EnumConstants.ModelCurrentState.UnChanged;
                         styleModel.SelectedComboColorList.Add(styleColorModel.SequenceNumber);
                         styleModel.StyleColorModelList.Add(styleColorModel);
                     }


                     foreach (DataRow sizeDataRow in StyleDataSet.Tables[5].Rows)
                     {
                         StyleSizeModel styleSizeModel = new StyleSizeModel();
                         styleSizeModel.SequenceNumber = long.Parse(sizeDataRow["style_size_id"].ToString());
                         styleSizeModel.SizeSequenceNumber = long.Parse(sizeDataRow["style_size_size_id"].ToString());
                         styleSizeModel.StyleSequenceNumber = long.Parse(sizeDataRow["style_size_style_id"].ToString());

                         styleSizeModel.State = EnumConstants.ModelCurrentState.UnChanged;
                         styleModel.SelectedComboSizeList.Add(styleSizeModel.SizeSequenceNumber);
                         styleModel.StyleSizeModelList.Add(styleSizeModel);
                     }

                     foreach (DataRow fabricDataRow in StyleDataSet.Tables[7].Rows)
                     {
                         StyleFabricModel styleFabricModel = new StyleFabricModel();
                         styleFabricModel.SequenceNumber = long.Parse(fabricDataRow["style_fabric_id"].ToString());
                         styleFabricModel.SourcesSequenceNumber = long.Parse(fabricDataRow["style_fabric_style_id"].ToString());
                         styleFabricModel.StyleSequenceNumber = long.Parse(fabricDataRow["style_fabric_sources_id"].ToString());
                         styleFabricModel.State = EnumConstants.ModelCurrentState.UnChanged;
                         styleModel.SelectedFabricList.Add(styleFabricModel.SourcesSequenceNumber);
                         styleModel.StyleFabricModelList.Add(styleFabricModel);
                     }

                    
                     
                 }
             }
             return styleModel;
         }

         public StyleListModel SaveStyle(StyleModel styleModel)
         {

             if (styleModel.Mode == EnumConstants.ScreenMode.New)
             {
                 List<StyleColorModel> styleColorModelList = new List<StyleColorModel>();

                 if (styleModel.StyleColorModelList != null)
                     styleColorModelList = styleModel.StyleColorModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                

                 DataTable styleColorDataTable = UtilityMethods.ToDataTable(styleColorModelList);

                 List<StyleSizeModel> styleSizeModelList = new List<StyleSizeModel>();
                 if (styleModel.StyleSizeModelList != null)
                 styleSizeModelList = styleModel.StyleSizeModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 DataTable styleSizeDataTable = UtilityMethods.ToDataTable(styleSizeModelList);


                 List<StyleFabricModel> styleFabricModelList = new List<StyleFabricModel>();
                 if (styleModel.StyleFabricModelList != null)
                 styleFabricModelList = styleModel.StyleFabricModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 DataTable styleFabricDataTable = UtilityMethods.ToDataTable(styleFabricModelList);


                 List<StylePanelModel> stylePanelModelList = new List<StylePanelModel>();
                 if (styleModel.StylePanelModelList != null)
                 stylePanelModelList = styleModel.StylePanelModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 DataTable stylePanelDataTable = UtilityMethods.ToDataTable(stylePanelModelList);


                 List<StylePanelColorModel> stylePanelColorList = new List<StylePanelColorModel>();
                 List<StylePanelProcessModel> stylePanelProcessList = new List<StylePanelProcessModel>();

                 foreach (StylePanelModel stylePanelModel in stylePanelModelList)
                 {
                     stylePanelColorList.AddRange(stylePanelModel.StylePanelColorModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid));
                     stylePanelProcessList.AddRange(stylePanelModel.StylePanelProcessModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid));
                 }

                 DataTable stylePanelColorDataTable = UtilityMethods.ToDataTable(stylePanelColorList);
                 DataTable stylePanelProcessDataTable = UtilityMethods.ToDataTable(stylePanelProcessList);


                 List<StyleProcessSourcesModel> styleProcessSourcesModelList = new List<StyleProcessSourcesModel>();
                 if (styleModel.StyleProcessSourcesModelList != null)
                 styleProcessSourcesModelList = styleModel.StyleProcessSourcesModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 DataTable styleProcessSourcesDataTable = UtilityMethods.ToDataTable(styleProcessSourcesModelList);

                 List<StyleProccessSourcesColorModel> styleProccessSourcesColorList = new List<StyleProccessSourcesColorModel>();

                 foreach (StyleProcessSourcesModel styleProcessSourcesModel in styleProcessSourcesModelList)
                 {
                     styleProccessSourcesColorList.AddRange(styleProcessSourcesModel.StyleProccessSourcesColorModelList.Where(x => x.TempGuid == styleProcessSourcesModel.ProcessSourcesTempGuid));
                      
                 }

                 DataTable styleProccessSourcesColorDataTable = UtilityMethods.ToDataTable(styleProccessSourcesColorList);

                 using (DbCommand saveStyleMainCommand = _dbHelper.GetStoredProcCommand("rx_ins_style"))
                 {

                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_name", DbType.String, styleModel.StyleName);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_description", DbType.String, styleModel.StyleDescription);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_style_type_id", DbType.Int64, styleModel.StyleTypeSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_completed", DbType.Boolean, styleModel.IsCompleted);
                     IDataReader dbReader = _dbHelper.ExecuteReader(saveStyleMainCommand);
                     dbReader.Read();
                     styleModel.StyleSequenceNumber = long.Parse(dbReader["style_id"].ToString());
                 }

                 using (DbCommand saveStyleColorCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_color"))
                 {


                     _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_pantone", DbType.String, "ColorPantone", DataRowVersion.Current);
                      
                     _dbHelper.Fill(saveStyleColorCommand, styleColorDataTable);

                 }

                 using (DbCommand saveStyleSizeCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_size"))
                 {


                     _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_size_id", DbType.Int64, "SizeSequenceNumber", DataRowVersion.Current);
                      
                     _dbHelper.Fill(saveStyleSizeCommand, styleSizeDataTable);

                 }
                 using (DbCommand saveStyleFabricCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_fabric"))
                 {


                     _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_sources_id", DbType.Int64, "SourcesSequenceNumber", DataRowVersion.Current);

                     _dbHelper.Fill(saveStyleFabricCommand, styleFabricDataTable);

                 }
                 using (DbCommand saveStylePanelCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_panel"))
                 {


                     _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                     _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_name", DbType.String, "PanelName", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_description", DbType.String, "PanelDescription", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                     _dbHelper.Fill(saveStylePanelCommand, stylePanelDataTable);

                 }

                 using (DbCommand saveStylePanelColorCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_panel_color"))
                 {


                     _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_pantone", DbType.String, "ColorPantone", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                     _dbHelper.Fill(saveStylePanelColorCommand, stylePanelColorDataTable);

                 }

                 using (DbCommand saveStylePanelProcessCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_panel_process"))
                 {


                     _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_process_id", DbType.Int64, "ProcessSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                     _dbHelper.Fill(saveStylePanelProcessCommand, stylePanelProcessDataTable);

                 }

                 using (DbCommand saveStyleProcessSourcesCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_process_sources"))
                 {

                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_sources_id", DbType.Int64, "SourcesSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_process_id", DbType.Int64, "ProcessSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_temp_id", DbType.String, "ProcessSourcesTempGuid", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_lottype_seqno", DbType.Int64, "LotSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_sizeapplicable", DbType.Boolean, "IsSizeApplicable", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);

                     _dbHelper.Fill(saveStyleProcessSourcesCommand, styleProcessSourcesDataTable);

                 }

                 using (DbCommand saveStyleProcessSourcesColorCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_process_sources_color"))
                 {


                     _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_primary_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                     _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_style_process_sources_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                     _dbHelper.Fill(saveStyleProcessSourcesColorCommand, styleProccessSourcesColorDataTable);

                 }
             }
             else
             {
                 

             }


             return PopulateDropDown();
         }

 
        #endregion
}
}
