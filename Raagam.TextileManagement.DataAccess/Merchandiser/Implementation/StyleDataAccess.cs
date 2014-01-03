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

         public StyleModel PopulateDropDown(StyleModel styleModel)
        {
           
            using (DbCommand getDropDownCommand = _dbHelper.GetStoredProcCommand("rx_lst_style"))
            {
                using (DataSet DropDownDataSet = _dbHelper.ExecuteDataSet(getDropDownCommand))
                {
                    foreach (DataRow colorDataRow in DropDownDataSet.Tables[3].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = colorDataRow["color_id"].ToString();
                        dropDown.Text = colorDataRow["color_name"].ToString();
                        dropDown.Selected = false;
                        styleModel.ColorDropDownList.Add(dropDown);
                    }

                    foreach (DataRow colorDataRow in DropDownDataSet.Tables[4].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = colorDataRow["size_id"].ToString();
                        dropDown.Text = colorDataRow["size_name"].ToString();
                        dropDown.Selected = false;
                        styleModel.SizeDropDownList.Add(dropDown);
                    }

                    foreach (DataRow styleDataRow in DropDownDataSet.Tables[6].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = styleDataRow["style_id"].ToString();
                        dropDown.Text = styleDataRow["style_name"].ToString();
                        dropDown.Selected = false;
                        styleModel.StyleDropDownList.Add(dropDown);
                    }

                    foreach (DataRow styleTypeDataRow in DropDownDataSet.Tables[7].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = styleTypeDataRow["style_type_id"].ToString();
                        dropDown.Text = styleTypeDataRow["style_type_name"].ToString();
                        dropDown.Selected = false;
                        styleModel.StyleTypeDropDownList.Add(dropDown);
                    }

 
                }
            }

            return styleModel;
        }
 
        #endregion
}
}
