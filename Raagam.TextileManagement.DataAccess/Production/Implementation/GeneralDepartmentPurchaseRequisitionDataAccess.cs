using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using System.Data.Common;
using System.Web.Mvc;
using Raagam.TextileManagement.CommonUtility;
using System.Data;

namespace Raagam.TextileManagement.DataAccess
{
    public class GeneralDepartmentPurchaseRequisitionDataAccess : IGeneralDepartmentPurchaseRequisitionDataAccess 
    {
                
        IDBHelper _dbHelper;

        public GeneralDepartmentPurchaseRequisitionDataAccess()
        {
            _dbHelper = new DBHelper();
        }

        #region IGeneralDepartmentPurchaseRequisitionDataAccess Members

        public GeneralDepartmentPurchaseRequisitionHeaderModel DeptPurReqPopulateDropDown()
        {
            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();

            using (DbCommand getDepartmentPurchaseRequisitionCommand = _dbHelper.GetStoredProcCommand("rx_lst_departmentgeneral_purchaserequisition"))
            {

                using (DataSet getDepartmentPurchaseRequisitionDataSet = _dbHelper.ExecuteDataSet(getDepartmentPurchaseRequisitionCommand))
                {

                    departmentPurchaseRequisitionHeaderModel.ProcessDropDownList = new List<SelectListItem>();
                    departmentPurchaseRequisitionHeaderModel.ProductGroupModelList = new List<ProductGroupModel>();
                    departmentPurchaseRequisitionHeaderModel.ProductModelList = new List<ProductModel>();
                    departmentPurchaseRequisitionHeaderModel.OrderDropDownList = new List<SelectListItem>();
                    departmentPurchaseRequisitionHeaderModel.LotTypeModelList = new List<LotTypeModel>();


                    foreach (DataRow productGroupDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[0].Rows)
                    {
                        ProductGroupModel productGroupModel = new ProductGroupModel();
                        productGroupModel.ProductGroupSequenceNumber = (long)productGroupDataRow["prodgrp_seqno"];
                        productGroupModel.ProductGroupName = productGroupDataRow["prodgrp_name"].ToString();
                        productGroupModel.ProductSupplierSequenceNumber = (long)productGroupDataRow["prodsupp_supplier"];
                        departmentPurchaseRequisitionHeaderModel.ProductGroupModelList.Add(productGroupModel);
                    }


                    foreach (DataRow productDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[1].Rows)
                    {
                        ProductModel productModel = new ProductModel();
                        productModel.ProductSequenceNumber = (long)productDataRow["prod_seqno"];
                        productModel.ProductName = productDataRow["prod_name"].ToString();
                        productModel.ProductGroupSequenceNumber = (long)productDataRow["prod_productgroup"];
                        productModel.ProductSupplierSequenceNumber = (long)productDataRow["prodsupp_supplier"];
                        productModel.ProductCategorySequenceNumber = (long)productDataRow["prod_catseqno"];
                        productModel.ProductTax = (decimal)productDataRow["prod_tax"];
                        departmentPurchaseRequisitionHeaderModel.ProductModelList.Add(productModel);
                    }


                    foreach (DataRow processDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[2].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = processDataRow["process_id"].ToString();
                        dropDown.Text = processDataRow["process_name"].ToString();
                        dropDown.Selected = false;
                        departmentPurchaseRequisitionHeaderModel.ProcessDropDownList.Add(dropDown);
                    }

                    foreach (DataRow orderDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[3].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = orderDataRow["order_id"].ToString();
                        dropDown.Text = orderDataRow["order_id"].ToString();
                        dropDown.Selected = false;
                        departmentPurchaseRequisitionHeaderModel.OrderDropDownList.Add(dropDown);
                    }


                    foreach (DataRow lotTypeDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[4].Rows)
                    {
                        LotTypeModel lotTypeModel = new LotTypeModel();
                        lotTypeModel.LotTypeSequenceNumber = (long)lotTypeDataRow["lottyp_seqno"];
                        lotTypeModel.LotTypeName = lotTypeDataRow["lottyp_name"].ToString();
                        lotTypeModel.LotTypeQuantity = (decimal)lotTypeDataRow["lottyp_actualqty"];
                        lotTypeModel.LotTypeLinkSequenceNumber = (long)lotTypeDataRow["lottyp_lottypseqno"];
                        lotTypeModel.UnitCategorySequenceNumber = (long)lotTypeDataRow["unit_catseqno"];
                        departmentPurchaseRequisitionHeaderModel.LotTypeModelList.Add(lotTypeModel);
                    }

                }
            }

            return departmentPurchaseRequisitionHeaderModel;
        }

        public GeneralDepartmentPurchaseRequisitionHeaderModel SelectDepartmentPurchaseRequisition(long departmentPurchaseRequisitionNumber)
        {
            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();

            using (DbCommand getDepartmentPurchaseRequisitionCommand = _dbHelper.GetStoredProcCommand("rx_sel_departmentgeneral_purchaserequisition"))
            {
                _dbHelper.AddInParameter(getDepartmentPurchaseRequisitionCommand, "@deptgen_purreqhead_no",
                                    DbType.Int64, departmentPurchaseRequisitionNumber);

                using (DataSet getDepartmentPurchaseRequisitionDataSet = _dbHelper.ExecuteDataSet(getDepartmentPurchaseRequisitionCommand))
                {

                    departmentPurchaseRequisitionHeaderModel.ProcessedStatus = (bool)getDepartmentPurchaseRequisitionDataSet.Tables[0].Rows[0]["deptgen_purreqhead_processedstatus"];

                    foreach (DataRow departmentPurchaseRequisitionHeaderDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[1].Rows)
                    {
                        departmentPurchaseRequisitionHeaderModel.SequenceNumber = (long)departmentPurchaseRequisitionHeaderDataRow["deptgen_purreqhead_seqno"];
                        departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber = (long)departmentPurchaseRequisitionHeaderDataRow["deptgen_purreqhead_no"];
                        departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionDate = (DateTime)departmentPurchaseRequisitionHeaderDataRow["deptgen_purreqhead_date"];
                        departmentPurchaseRequisitionHeaderModel.ProcessSequenceNumber = (long)departmentPurchaseRequisitionHeaderDataRow["deptgen_purreqhead_process_seqno"];
                        //departmentPurchaseRequisitionHeaderModel.OrderNumber = (long)departmentPurchaseRequisitionHeaderDataRow["deptpurreqhead_order_seqno"];
                        departmentPurchaseRequisitionHeaderModel.Remarks = departmentPurchaseRequisitionHeaderDataRow["deptgen_purreqhead_remarks"].ToString();
                        departmentPurchaseRequisitionHeaderModel.Mode = EnumConstants.ScreenMode.Edit;
                    }
                    foreach (DataRow departmentPurchaseRequisitionTrailerDataRow in getDepartmentPurchaseRequisitionDataSet.Tables[2].Rows)
                    {
                        GeneralDepartmentPurchaseRequisitionTrailerModel departmentPurchaseRequisitionTrailerModel = new GeneralDepartmentPurchaseRequisitionTrailerModel();
                        departmentPurchaseRequisitionTrailerModel.SequenceNumber = (long)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_seqno"];
                        departmentPurchaseRequisitionTrailerModel.DepartmentPurchaseRequisitionHeaderSequenceNumber = (long)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_headseqno"];
                        departmentPurchaseRequisitionTrailerModel.ProductGroupSequenceNumber = (long)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_productgroup"];
                        departmentPurchaseRequisitionTrailerModel.ProductSequenceNumber = (long)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_product"];
                        //departmentPurchaseRequisitionTrailerModel.Color = departmentPurchaseRequisitionTrailerDataRow["deptpurreqtrail_color"].ToString();
                        //departmentPurchaseRequisitionTrailerModel.Size = departmentPurchaseRequisitionTrailerDataRow["deptpurreqtrail_size"].ToString();
                        //departmentPurchaseRequisitionTrailerModel.SizeSpec = departmentPurchaseRequisitionTrailerDataRow["deptpurreqtrail_sizespec"].ToString();
                        departmentPurchaseRequisitionTrailerModel.Quantity = (decimal)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_qty"];
                        departmentPurchaseRequisitionTrailerModel.LotTypeSequenceNumber = (long)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_lottypseqno"];
                        departmentPurchaseRequisitionTrailerModel.ApprovalStatus = (bool)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_approvalstatus"];
                        departmentPurchaseRequisitionTrailerModel.Status = (byte)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_status"];
                        departmentPurchaseRequisitionTrailerModel.ManagerApprovalStatus = (bool)departmentPurchaseRequisitionTrailerDataRow["deptgen_purreqtrail_manager_approvalstatus"];
                       // departmentPurchaseRequisitionTrailerModel.TransitBefore = Convert.ToDateTime(departmentPurchaseRequisitionTrailerDataRow["deptpurreqtrail_transitbefore"]).ToString("dd/MM/yyyy");
                        departmentPurchaseRequisitionTrailerModel.ProductGroupName = departmentPurchaseRequisitionTrailerDataRow["prodgrp_name"].ToString();
                        departmentPurchaseRequisitionTrailerModel.ProductName = departmentPurchaseRequisitionTrailerDataRow["prod_name"].ToString();
                        departmentPurchaseRequisitionTrailerModel.LotType = departmentPurchaseRequisitionTrailerDataRow["lottyp_name"].ToString();
                        departmentPurchaseRequisitionTrailerModel.LotQuantity = (decimal) departmentPurchaseRequisitionTrailerDataRow["lottyp_actualqty"];
                        departmentPurchaseRequisitionTrailerModel.State = EnumConstants.ModelCurrentState.UnChanged;
                        departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Add(departmentPurchaseRequisitionTrailerModel);

                    }
                }
            }

            return departmentPurchaseRequisitionHeaderModel;
        }

        public GeneralDepartmentPurchaseRequisitionHeaderModel SaveDepartmentPurchaseRequisition(GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel)
        {

            if (departmentPurchaseRequisitionHeaderModel.Mode == EnumConstants.ScreenMode.New)
            {
                List<GeneralDepartmentPurchaseRequisitionTrailerModel> departmentPurchaseRequisitionTrailerModel = new List<GeneralDepartmentPurchaseRequisitionTrailerModel>();
                departmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();

                DataTable departmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(departmentPurchaseRequisitionTrailerModel);

                using (DbCommand saveDepartmentPurchaseRequisitionHeaderCommand = _dbHelper.GetStoredProcCommand("rx_ins_departmentgeneral_purchaserequisition_header"))
                {

                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptgen_purreqhead_process_seqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.ProcessSequenceNumber);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_order_seqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.OrderNumber);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptgen_purreqhead_remarks", DbType.String, departmentPurchaseRequisitionHeaderModel.Remarks);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptgen_purreqhead_createdby", DbType.String, departmentPurchaseRequisitionHeaderModel.CreatedBy);


                    IDataReader dbReader = _dbHelper.ExecuteReader(saveDepartmentPurchaseRequisitionHeaderCommand);
                    dbReader.Read();
                    departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber = long.Parse(dbReader["deptgen_purreqhead_no"].ToString());
                    departmentPurchaseRequisitionHeaderModel.SequenceNumber = long.Parse(dbReader["deptgen_purreqhead_seqno"].ToString());

                }

                using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_ins_departmentgeneral_purchaserequisition_trailer"))
                {


                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_headseqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_productgroup", DbType.Int64, "ProductGroupSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_product", DbType.Int64, "ProductSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_lottypseqno", DbType.Int64, "LotTypeSequenceNumber", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_color", DbType.String, "Color", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_size", DbType.String, "Size", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_sizespec", DbType.String, "SizeSpec", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_transitbefore", DbType.DateTime, "TransitBefore", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);
                    _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, departmentPurchaseRequisitionTrailerDataTable);

                }
            }
            else
            {
                List<GeneralDepartmentPurchaseRequisitionTrailerModel> addedDepartmentPurchaseRequisitionTrailerModel = new List<GeneralDepartmentPurchaseRequisitionTrailerModel>();
                addedDepartmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();

                List<GeneralDepartmentPurchaseRequisitionTrailerModel> updatedDepartmentPurchaseRequisitionTrailerModel = new List<GeneralDepartmentPurchaseRequisitionTrailerModel>();
                updatedDepartmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();

                List<GeneralDepartmentPurchaseRequisitionTrailerModel> deletedDepartmentPurchaseRequisitionTrailerModel = new List<GeneralDepartmentPurchaseRequisitionTrailerModel>();
                deletedDepartmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted).ToList();

                DataTable addedDepartmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(addedDepartmentPurchaseRequisitionTrailerModel);
                DataTable updatedDepartmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(updatedDepartmentPurchaseRequisitionTrailerModel);
                DataTable deletedDepartmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(deletedDepartmentPurchaseRequisitionTrailerModel);

                using (DbCommand saveDepartmentPurchaseRequisitionHeaderCommand = _dbHelper.GetStoredProcCommand("rx_upd_departmentgeneral_purchaserequisition_header"))
                {

                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptgen_purreqhead_seqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptgen_purreqhead_remarks", DbType.String, departmentPurchaseRequisitionHeaderModel.Remarks);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptgen_purreqhead_updatedby", DbType.String, departmentPurchaseRequisitionHeaderModel.UpdatedBy);

                   _dbHelper.ExecuteNonQuery(saveDepartmentPurchaseRequisitionHeaderCommand);
                   
                }

                using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_ins_departmentgeneral_purchaserequisition_trailer"))
                {
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_headseqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_productgroup", DbType.Int64, "ProductGroupSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_product", DbType.Int64, "ProductSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_lottypseqno", DbType.Int64, "LotTypeSequenceNumber", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_color", DbType.String, "Color", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_size", DbType.String, "Size", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_sizespec", DbType.String, "SizeSpec", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_transitbefore", DbType.DateTime, "TransitBefore", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);
                    _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, addedDepartmentPurchaseRequisitionTrailerDataTable);

                }


                using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_upd_departmentgeneral_purchaserequisition_trailer"))
                {
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_seqno", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_headseqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_productgroup", DbType.Int64, "ProductGroupSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_product", DbType.Int64, "ProductSequenceNumber", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_lottypseqno", DbType.Int64, "LotTypeSequenceNumber", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_color", DbType.String, "Color", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_size", DbType.String, "Size", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_sizespec", DbType.String, "SizeSpec", DataRowVersion.Current);
                    //_dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_transitbefore", DbType.DateTime, "TransitBefore", DataRowVersion.Current);
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);
                    _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, updatedDepartmentPurchaseRequisitionTrailerDataTable);

                }


                using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_del_departmentgeneral_purchaserequisition_trailer"))
                {
                    _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptgen_purreqtrail_seqno", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
                    _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, deletedDepartmentPurchaseRequisitionTrailerDataTable);

                }

            }


            return departmentPurchaseRequisitionHeaderModel;
        }
        #endregion
    }
}
