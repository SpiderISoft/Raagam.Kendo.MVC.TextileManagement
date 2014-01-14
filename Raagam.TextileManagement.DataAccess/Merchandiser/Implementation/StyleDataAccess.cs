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
                         styleModel.SelectedComboColorList.Add(styleColorModel.ColorSequenceNumber);
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
                         styleFabricModel.StyleSequenceNumber = long.Parse(fabricDataRow["style_fabric_style_id"].ToString());
                         styleFabricModel.SourcesSequenceNumber = long.Parse(fabricDataRow["style_fabric_sources_id"].ToString());
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

             List<StyleColorModel> AddedStyleColorModelList = new List<StyleColorModel>();
             List<StyleColorModel> UpdatedStyleColorModelList = new List<StyleColorModel>();
             List<StyleColorModel> DeletedStyleColorModelList = new List<StyleColorModel>();

             if (styleModel.StyleColorModelList != null)
             {
                 AddedStyleColorModelList = styleModel.StyleColorModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 UpdatedStyleColorModelList = styleModel.StyleColorModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();
                 DeletedStyleColorModelList = styleModel.StyleColorModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted).ToList();
             }

             DataTable AddedStyleColorDataTable = UtilityMethods.ToDataTable(AddedStyleColorModelList);
             DataTable UpdatedStyleColorDataTable = UtilityMethods.ToDataTable(UpdatedStyleColorModelList);
             DataTable DeletedStyleColorDataTable = UtilityMethods.ToDataTable(DeletedStyleColorModelList);

             List<StyleSizeModel> AddedStyleSizeModelList = new List<StyleSizeModel>();
             List<StyleSizeModel> UpdatedStyleSizeModelList = new List<StyleSizeModel>();
             List<StyleSizeModel> DeletedStyleSizeModelList = new List<StyleSizeModel>();


             if (styleModel.StyleSizeModelList != null)
             {
                 AddedStyleSizeModelList = styleModel.StyleSizeModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 UpdatedStyleSizeModelList = styleModel.StyleSizeModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();
                 DeletedStyleSizeModelList = styleModel.StyleSizeModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted).ToList();
             }
             DataTable AddedStyleSizeDataTable = UtilityMethods.ToDataTable(AddedStyleSizeModelList);
             DataTable UpdatedStyleSizeDataTable = UtilityMethods.ToDataTable(UpdatedStyleSizeModelList);
             DataTable DeletedStyleSizeDataTable = UtilityMethods.ToDataTable(DeletedStyleSizeModelList);


             List<StyleFabricModel> AddedStyleFabricModelList = new List<StyleFabricModel>();
             List<StyleFabricModel> UpdatedStyleFabricModelList = new List<StyleFabricModel>();
             List<StyleFabricModel> DeletedStyleFabricModelList = new List<StyleFabricModel>();

             if (styleModel.StyleFabricModelList != null)
             {
                 AddedStyleFabricModelList = styleModel.StyleFabricModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 UpdatedStyleFabricModelList = styleModel.StyleFabricModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();
                 DeletedStyleFabricModelList = styleModel.StyleFabricModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted).ToList();
             }
             DataTable AddedStyleFabricDataTable = UtilityMethods.ToDataTable(AddedStyleFabricModelList);
             DataTable UpdatedStyleFabricDataTable = UtilityMethods.ToDataTable(UpdatedStyleFabricModelList);
             DataTable DeletedStyleFabricDataTable = UtilityMethods.ToDataTable(DeletedStyleFabricModelList);


             List<StylePanelModel> AddedStylePanelModelList = new List<StylePanelModel>();
             List<StylePanelModel> UpdatedStylePanelModelList = new List<StylePanelModel>();
             List<StylePanelModel> DeletedStylePanelModelList = new List<StylePanelModel>();

             if (styleModel.StylePanelModelList != null)
             {
                 AddedStylePanelModelList = styleModel.StylePanelModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 UpdatedStylePanelModelList = styleModel.StylePanelModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();
                 DeletedStylePanelModelList = styleModel.StylePanelModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted).ToList();
             }
             DataTable AddedStylePanelDataTable = UtilityMethods.ToDataTable(AddedStylePanelModelList);
             DataTable UpdatedStylePanelDataTable = UtilityMethods.ToDataTable(UpdatedStylePanelModelList);
             DataTable DeletedStylePanelDataTable = UtilityMethods.ToDataTable(DeletedStylePanelModelList);

             List<StylePanelColorModel> AddedStylePanelColorList = new List<StylePanelColorModel>();
             List<StylePanelProcessModel> AddedStylePanelProcessList = new List<StylePanelProcessModel>();



             List<StylePanelColorModel> UpdatedStylePanelColorList = new List<StylePanelColorModel>();
             List<StylePanelProcessModel> UpdatedStylePanelProcessList = new List<StylePanelProcessModel>();


             List<StylePanelColorModel> DeletedStylePanelColorList = new List<StylePanelColorModel>();
             List<StylePanelProcessModel> DeletedStylePanelProcessList = new List<StylePanelProcessModel>();

             foreach (StylePanelModel stylePanelModel in AddedStylePanelModelList)
             {
                 AddedStylePanelColorList.AddRange(stylePanelModel.StylePanelColorModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Added));
                 AddedStylePanelProcessList.AddRange(stylePanelModel.StylePanelProcessModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Added));
             }

            
             foreach (StylePanelModel stylePanelModel in UpdatedStylePanelModelList)
             {
                 AddedStylePanelColorList.AddRange(stylePanelModel.StylePanelColorModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Added));
                 AddedStylePanelProcessList.AddRange(stylePanelModel.StylePanelProcessModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Added));
                 UpdatedStylePanelColorList.AddRange(stylePanelModel.StylePanelColorModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Updated));
                 UpdatedStylePanelProcessList.AddRange(stylePanelModel.StylePanelProcessModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Updated));
                 DeletedStylePanelColorList.AddRange(stylePanelModel.StylePanelColorModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Deleted));
                 DeletedStylePanelProcessList.AddRange(stylePanelModel.StylePanelProcessModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Deleted));

             }

             foreach (StylePanelModel stylePanelModel in DeletedStylePanelModelList)
             {
                 DeletedStylePanelColorList.AddRange(stylePanelModel.StylePanelColorModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Deleted));
                 DeletedStylePanelProcessList.AddRange(stylePanelModel.StylePanelProcessModelList.Where(x => x.TempGuid == stylePanelModel.TempGuid && x.State == EnumConstants.ModelCurrentState.Deleted));
             }


             DataTable AddedStylePanelColorDataTable = UtilityMethods.ToDataTable(AddedStylePanelColorList);
             DataTable AddedStylePanelProcessDataTable = UtilityMethods.ToDataTable(AddedStylePanelProcessList);

             DataTable UpdatedStylePanelColorDataTable = UtilityMethods.ToDataTable(UpdatedStylePanelColorList);
             DataTable UpdatedStylePanelProcessDataTable = UtilityMethods.ToDataTable(UpdatedStylePanelProcessList);

             DataTable DeletedStylePanelColorDataTable = UtilityMethods.ToDataTable(DeletedStylePanelColorList);
             DataTable DeletedStylePanelProcessDataTable = UtilityMethods.ToDataTable(DeletedStylePanelProcessList);


             List<StyleProcessSourcesModel> AddedStyleProcessSourcesModelList = new List<StyleProcessSourcesModel>();
             List<StyleProcessSourcesModel> UpdatedStyleProcessSourcesModelList = new List<StyleProcessSourcesModel>();
             List<StyleProcessSourcesModel> DeletedStyleProcessSourcesModelList = new List<StyleProcessSourcesModel>();

             if (styleModel.StyleProcessSourcesModelList != null)
             {
                 AddedStyleProcessSourcesModelList = styleModel.StyleProcessSourcesModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();
                 UpdatedStyleProcessSourcesModelList = styleModel.StyleProcessSourcesModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();
                 DeletedStyleProcessSourcesModelList = styleModel.StyleProcessSourcesModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted ).ToList();
             }
             DataTable AddedStyleProcessSourcesDataTable = UtilityMethods.ToDataTable(AddedStyleProcessSourcesModelList);
             DataTable UpdatedStyleProcessSourcesDataTable = UtilityMethods.ToDataTable(UpdatedStyleProcessSourcesModelList);
             DataTable DeletedStyleProcessSourcesDataTable = UtilityMethods.ToDataTable(DeletedStyleProcessSourcesModelList);

             List<StyleProccessSourcesColorModel> AddedStyleProccessSourcesColorList = new List<StyleProccessSourcesColorModel>();
             List<StyleProccessSourcesColorModel> UpdatedStyleProccessSourcesColorList = new List<StyleProccessSourcesColorModel>();
             List<StyleProccessSourcesColorModel> DeletedStyleProccessSourcesColorList = new List<StyleProccessSourcesColorModel>();

             foreach (StyleProcessSourcesModel styleProcessSourcesModel in AddedStyleProcessSourcesModelList)
             {
                 AddedStyleProccessSourcesColorList.AddRange(styleProcessSourcesModel.StyleProccessSourcesColorModelList.Where(x => x.TempGuid == styleProcessSourcesModel.ProcessSourcesTempGuid && x.State == EnumConstants.ModelCurrentState.Added));
             }

             foreach (StyleProcessSourcesModel styleProcessSourcesModel in UpdatedStyleProcessSourcesModelList)
             {
                 AddedStyleProccessSourcesColorList.AddRange(styleProcessSourcesModel.StyleProccessSourcesColorModelList.Where(x => x.TempGuid == styleProcessSourcesModel.ProcessSourcesTempGuid && x.State == EnumConstants.ModelCurrentState.Added));
                 UpdatedStyleProccessSourcesColorList.AddRange(styleProcessSourcesModel.StyleProccessSourcesColorModelList.Where(x => x.TempGuid == styleProcessSourcesModel.ProcessSourcesTempGuid && x.State == EnumConstants.ModelCurrentState.Updated));
                 DeletedStyleProccessSourcesColorList.AddRange(styleProcessSourcesModel.StyleProccessSourcesColorModelList.Where(x => x.TempGuid == styleProcessSourcesModel.ProcessSourcesTempGuid && x.State == EnumConstants.ModelCurrentState.Deleted));

             }
             foreach (StyleProcessSourcesModel styleProcessSourcesModel in DeletedStyleProcessSourcesModelList)
             { 
                 DeletedStyleProccessSourcesColorList.AddRange(styleProcessSourcesModel.StyleProccessSourcesColorModelList.Where(x => x.TempGuid == styleProcessSourcesModel.ProcessSourcesTempGuid && x.State == EnumConstants.ModelCurrentState.Deleted));

             }
             DataTable AddedStyleProccessSourcesColorDataTable = UtilityMethods.ToDataTable(AddedStyleProccessSourcesColorList);
             DataTable UpdatedStyleProccessSourcesColorDataTable = UtilityMethods.ToDataTable(UpdatedStyleProccessSourcesColorList);
             DataTable DeletedStyleProccessSourcesColorDataTable = UtilityMethods.ToDataTable(DeletedStyleProccessSourcesColorList);

             if (styleModel.Mode == EnumConstants.ScreenMode.New)
             {
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

                
             }
             else
             {

                 using (DbCommand saveStyleMainCommand = _dbHelper.GetStoredProcCommand("rx_upd_style"))
                 {
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_name", DbType.String, styleModel.StyleName);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_description", DbType.String, styleModel.StyleDescription);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_style_type_id", DbType.Int64, styleModel.StyleTypeSequenceNumber);
                     _dbHelper.AddInParameter(saveStyleMainCommand, "@style_completed", DbType.Boolean, styleModel.IsCompleted);
                     _dbHelper.ExecuteNonQuery(saveStyleMainCommand);
                 }
             }

             using (DbCommand saveStyleColorCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_color"))
             {


                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_pantone", DbType.String, "ColorPantone", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleColorCommand, AddedStyleColorDataTable);

             }

             using (DbCommand saveStyleColorCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_color"))
             {

                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_pantone", DbType.String, "ColorPantone", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleColorCommand, UpdatedStyleColorDataTable);

             }

             using (DbCommand saveStyleColorCommand = _dbHelper.GetStoredProcCommand("rx_del_style_color"))
             {

                 _dbHelper.AddInParameter(saveStyleColorCommand, "@style_color_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleColorCommand, DeletedStyleColorDataTable);

             }

            


             using (DbCommand saveStyleSizeCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_size"))
             {


                 _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_size_id", DbType.Int64, "SizeSequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleSizeCommand, AddedStyleSizeDataTable);

             }

             using (DbCommand saveStyleSizeCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_size"))
             {

                 _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_size_id", DbType.Int64, "SizeSequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleSizeCommand, UpdatedStyleSizeDataTable);

             }

             using (DbCommand saveStyleSizeCommand = _dbHelper.GetStoredProcCommand("rx_del_style_size"))
             {

                 _dbHelper.AddInParameter(saveStyleSizeCommand, "@style_size_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleSizeCommand, DeletedStyleSizeDataTable);

             }

             using (DbCommand saveStyleFabricCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_fabric"))
             {


                 _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_sources_id", DbType.Int64, "SourcesSequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleFabricCommand, AddedStyleFabricDataTable);

             }
             using (DbCommand saveStyleFabricCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_fabric"))
             {

                 _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_sources_id", DbType.Int64, "SourcesSequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleFabricCommand, UpdatedStyleFabricDataTable);

             }

             using (DbCommand saveStyleFabricCommand = _dbHelper.GetStoredProcCommand("rx_del_style_fabric"))
             {

                 _dbHelper.AddInParameter(saveStyleFabricCommand, "@style_fabric_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleFabricCommand, DeletedStyleFabricDataTable);

             }
             using (DbCommand saveStylePanelCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_panel"))
             {


                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_name", DbType.String, "PanelName", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_description", DbType.String, "PanelDescription", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelCommand, AddedStylePanelDataTable);

             }
             using (DbCommand saveStylePanelCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_panel"))
             {

                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_name", DbType.String, "PanelName", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_description", DbType.String, "PanelDescription", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelCommand, UpdatedStylePanelDataTable);

             }
             using (DbCommand saveStylePanelCommand = _dbHelper.GetStoredProcCommand("rx_del_style_panel"))
             {

                 _dbHelper.AddInParameter(saveStylePanelCommand, "@style_panel_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelCommand, DeletedStylePanelDataTable);

             }
             using (DbCommand saveStylePanelColorCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_panel_color"))
             {


                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_pantone", DbType.String, "ColorPantone", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelColorCommand, AddedStylePanelColorDataTable);

             }
             using (DbCommand saveStylePanelColorCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_panel_color"))
             {

                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_pantone", DbType.String, "ColorPantone", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelColorCommand, UpdatedStylePanelColorDataTable);

             }
             using (DbCommand saveStylePanelColorCommand = _dbHelper.GetStoredProcCommand("rx_del_style_panel_color"))
             {

                 _dbHelper.AddInParameter(saveStylePanelColorCommand, "@style_panel_color_style_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 
                 _dbHelper.Fill(saveStylePanelColorCommand, DeletedStylePanelColorDataTable);

             }
             using (DbCommand saveStylePanelProcessCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_panel_process"))
             {


                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_process_id", DbType.Int64, "ProcessSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelProcessCommand, AddedStylePanelProcessDataTable);

             }

             using (DbCommand saveStylePanelProcessCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_panel_process"))
             {

                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_process_id", DbType.Int64, "ProcessSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_style_panel_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_style_panel_id", DbType.Int64, "StylePanelSequenceNumber", DataRowVersion.Current);
                 _dbHelper.Fill(saveStylePanelProcessCommand, UpdatedStylePanelProcessDataTable);

             }
             using (DbCommand saveStylePanelProcessCommand = _dbHelper.GetStoredProcCommand("rx_del_style_panel_process"))
             {


                 _dbHelper.AddInParameter(saveStylePanelProcessCommand, "@style_panel_process_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStylePanelProcessCommand, DeletedStylePanelProcessDataTable);

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

                 _dbHelper.Fill(saveStyleProcessSourcesCommand, AddedStyleProcessSourcesDataTable);

             }
             
             using (DbCommand saveStyleProcessSourcesCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_process_sources"))
             {
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_style_id", DbType.Int64, styleModel.StyleSequenceNumber);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_sources_id", DbType.Int64, "SourcesSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_process_id", DbType.Int64, "ProcessSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_temp_id", DbType.String, "ProcessSourcesTempGuid", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_lottype_seqno", DbType.Int64, "LotSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_sizeapplicable", DbType.Boolean, "IsSizeApplicable", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleProcessSourcesCommand, UpdatedStyleProcessSourcesDataTable);

             }
             using (DbCommand saveStyleProcessSourcesCommand = _dbHelper.GetStoredProcCommand("rx_del_style_process_sources"))
             {
                 _dbHelper.AddInParameter(saveStyleProcessSourcesCommand, "@style_process_sources_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
       
                 _dbHelper.Fill(saveStyleProcessSourcesCommand, DeletedStyleProcessSourcesDataTable);

             }
             using (DbCommand saveStyleProcessSourcesColorCommand = _dbHelper.GetStoredProcCommand("rx_ins_style_process_sources_color"))
             {


                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_primary_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_style_process_sources_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleProcessSourcesColorCommand, AddedStyleProccessSourcesColorDataTable);

             }
             using (DbCommand saveStyleProcessSourcesColorCommand = _dbHelper.GetStoredProcCommand("rx_upd_style_process_sources_color"))
             {

                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_primary_color_id", DbType.Int64, "StyleColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_color_id", DbType.Int64, "ColorSequenceNumber", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_style_process_sources_temp_id", DbType.String, "TempGuid", DataRowVersion.Current);
                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "style_process_sources_color_style_process_sources_id", DbType.Int64, "StyleProcessSourcesSequenceNumber", DataRowVersion.Current);

                 _dbHelper.Fill(saveStyleProcessSourcesColorCommand, UpdatedStyleProccessSourcesColorDataTable);

             }
             using (DbCommand saveStyleProcessSourcesColorCommand = _dbHelper.GetStoredProcCommand("rx_del_style_process_sources_color"))
             {

                 _dbHelper.AddInParameter(saveStyleProcessSourcesColorCommand, "@style_process_sources_color_id", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                 _dbHelper.Fill(saveStyleProcessSourcesColorCommand, DeletedStyleProccessSourcesColorDataTable);

             }

             return PopulateDropDown();
         }

 
        #endregion
}
}
