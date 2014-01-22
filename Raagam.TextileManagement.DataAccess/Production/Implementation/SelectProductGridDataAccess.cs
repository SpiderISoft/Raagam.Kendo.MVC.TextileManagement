using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using System.Data.Common;
using Raagam.TextileManagement.CommonUtility;
using System.Data;

namespace Raagam.TextileManagement.DataAccess 
{
    public class SelectProductGridDataAccess : ISelectProductGridDataAccess
    {

         IDBHelper _dbHelper;

         public SelectProductGridDataAccess()
        {
            _dbHelper = new DBHelper();
        }

        #region SelectProductGridDataAccess Members

            public List<SelectProductGridModel> SelectProductGrid(long SupplierSequenceNumber, string ProductName)
            {
                List<SelectProductGridModel> selectProductGridModelList = new List<SelectProductGridModel>();

                using (DbCommand getSelectProductGridCommand = _dbHelper.GetStoredProcCommand("rx_sel_selectproduct"))
                {
                    _dbHelper.AddInParameter(getSelectProductGridCommand, "@sup_seqno",
                                         DbType.Int64, SupplierSequenceNumber);

                    _dbHelper.AddInParameter(getSelectProductGridCommand, "@prod_name",
                     DbType.String, ProductName);

                    using (DataSet getSelectProductGridDataSet = _dbHelper.ExecuteDataSet(getSelectProductGridCommand))
                    {


                        foreach (DataRow selectProductGridDataRow in getSelectProductGridDataSet.Tables[0].Rows)
                        {
                           SelectProductGridModel selectProductGridModel = new SelectProductGridModel();
                           selectProductGridModel.Select = (bool)selectProductGridDataRow["stk_select"];
                           selectProductGridModel.ProductGroupSequenceNumber = (long)selectProductGridDataRow["prod_productgroup"];
                           selectProductGridModel.ProductSequenceNumber = (long)selectProductGridDataRow["prod_seqno"];
                           selectProductGridModel.ProductName = selectProductGridDataRow["prod_name"].ToString();
                           selectProductGridModel.ManufacturerName =  selectProductGridDataRow["mft_name"].ToString();
                           selectProductGridModel.StockCurrentQuantity = (decimal)selectProductGridDataRow["stk_currentqty"];
                           selectProductGridModel.ProductReOrderQuantity = (decimal)selectProductGridDataRow["prod_reorder"];
                           selectProductGridModel.LotTypeSequenceNumber = (long)selectProductGridDataRow["prod_lottypseqno"];
                           selectProductGridModel.ProductTax = (decimal)selectProductGridDataRow["prod_tax"];
                           selectProductGridModelList.Add(selectProductGridModel);
                        }

                         
                    }
                }
                return selectProductGridModelList;
            }



        #endregion
    }
}
