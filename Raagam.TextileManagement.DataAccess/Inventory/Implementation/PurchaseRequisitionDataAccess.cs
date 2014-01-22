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
    public class PurchaseRequisitionDataAccess : IPurchaseRequisitionDataAccess 
    {
                
        IDBHelper _dbHelper;

        public PurchaseRequisitionDataAccess()
        {
            _dbHelper = new DBHelper();
        }

        #region IPurchaseRequisitionDataAccess Members

        public PurchaseRequisitionHeaderModel PurReqPopulateDropDown()
        {
            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();

            using (DbCommand getPurchaseRequisitionCommand = _dbHelper.GetStoredProcCommand("rx_lst_purchaserequisition"))
            {

                using (DataSet getPurchaseRequisitionDataSet = _dbHelper.ExecuteDataSet(getPurchaseRequisitionCommand))
                {

                    purchaseRequisitionHeaderModel.ProcessDropDownList = new List<SelectListItem>();
                    purchaseRequisitionHeaderModel.ProductGroupModelList = new List<ProductGroupModel>();
                    purchaseRequisitionHeaderModel.ProductModelList = new List<ProductModel>();
                    purchaseRequisitionHeaderModel.OrderDropDownList = new List<SelectListItem>();
                    purchaseRequisitionHeaderModel.LotTypeModelList = new List<LotTypeModel>();
                    purchaseRequisitionHeaderModel.SupplierDropDownList = new List<SelectListItem>();
                    purchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList  = new List<PurchaseRequisitionTrailerModel>();


                    foreach (DataRow orderDataRow in getPurchaseRequisitionDataSet.Tables[0].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = orderDataRow["order_id"].ToString();
                        dropDown.Text = orderDataRow["order_id"].ToString();
                        dropDown.Selected = false;
                        purchaseRequisitionHeaderModel.OrderDropDownList.Add(dropDown);
                    }


                    foreach (DataRow productGroupDataRow in getPurchaseRequisitionDataSet.Tables[1].Rows)
                    {
                        ProductGroupModel productGroupModel = new ProductGroupModel();
                        productGroupModel.ProductGroupSequenceNumber = (long)productGroupDataRow["prodgrp_seqno"];
                        productGroupModel.ProductGroupName = productGroupDataRow["prodgrp_name"].ToString();
                        productGroupModel.ProductSupplierSequenceNumber = (long)productGroupDataRow["prodsupp_supplier"];
                        purchaseRequisitionHeaderModel.ProductGroupModelList.Add(productGroupModel);
                    }


                    foreach (DataRow productDataRow in getPurchaseRequisitionDataSet.Tables[2].Rows)
                    {
                        ProductModel productModel = new ProductModel();
                        productModel.ProductSequenceNumber = (long)productDataRow["prod_seqno"];
                        productModel.ProductName = productDataRow["prod_name"].ToString();
                        productModel.ProductGroupSequenceNumber = (long)productDataRow["prod_productgroup"];
                        productModel.ProductSupplierSequenceNumber = (long)productDataRow["prodsupp_supplier"];
                        productModel.ProductCategorySequenceNumber = (long)productDataRow["prod_catseqno"];
                        productModel.ProductTax = (decimal)productDataRow["prod_tax"];
                        productModel.CostPrice = (decimal)productDataRow["prodsupp_costprice"];
                        purchaseRequisitionHeaderModel.ProductModelList.Add(productModel);
                    }

                    foreach (DataRow supplierDataRow in getPurchaseRequisitionDataSet.Tables[3].Rows)
                    {
                        SelectListItem dropDown = new SelectListItem();
                        dropDown.Value = supplierDataRow["sup_seqno"].ToString();
                        dropDown.Text = supplierDataRow["sup_name"].ToString();
                        dropDown.Selected = false;
                        purchaseRequisitionHeaderModel.SupplierDropDownList.Add(dropDown);
                    }


                    foreach (DataRow lotTypeDataRow in getPurchaseRequisitionDataSet.Tables[4].Rows)
                    {
                        LotTypeModel lotTypeModel = new LotTypeModel();
                        lotTypeModel.LotTypeSequenceNumber = (long)lotTypeDataRow["lottyp_seqno"];
                        lotTypeModel.LotTypeName = lotTypeDataRow["lottyp_name"].ToString();
                        lotTypeModel.LotTypeQuantity = (decimal)lotTypeDataRow["lottyp_actualqty"];
                        lotTypeModel.LotTypeLinkSequenceNumber = (long)lotTypeDataRow["lottyp_lottypseqno"];
                        lotTypeModel.UnitCategorySequenceNumber = (long)lotTypeDataRow["unit_catseqno"];
                        purchaseRequisitionHeaderModel.LotTypeModelList.Add(lotTypeModel);
                    }


                    foreach (DataRow purReqDataRow in getPurchaseRequisitionDataSet.Tables[5].Rows)
                    {
                        PurchaseRequisitionTrailerModel purchaseRequisitionTrailerModel = new  PurchaseRequisitionTrailerModel();
                        purchaseRequisitionTrailerModel.SequenceNumber = (long)purReqDataRow["purreqtrail_seqno"];
                        purchaseRequisitionTrailerModel.PurchaseRequisitionHeaderSequenceNumber = (long)purReqDataRow["purreqtrail_headseqno"];
                        purchaseRequisitionTrailerModel.ProductGroupSequenceNumber = (long)purReqDataRow["purreqtrail_productgroup"];
                        purchaseRequisitionTrailerModel.ProductSequenceNumber = (long)purReqDataRow["purreqtrail_product"];
                        purchaseRequisitionTrailerModel.Quantity = (decimal)purReqDataRow["purreqtrail_qty"];
                        purchaseRequisitionTrailerModel.LotTypeSequenceNumber = (long)purReqDataRow["purreqtrail_lottypseqno"];
                        purchaseRequisitionTrailerModel.LotQuantity = (decimal)purReqDataRow["lottyp_actualqty"];
                        purchaseRequisitionTrailerModel.Color = purReqDataRow["purreqtrail_color"].ToString();
                        purchaseRequisitionTrailerModel.Size = purReqDataRow["purreqtrail_size"].ToString();
                        purchaseRequisitionTrailerModel.SizeSpec = purReqDataRow["purreqtrail_sizespec"].ToString();
                        purchaseRequisitionTrailerModel.RejectReason = purReqDataRow["purreqtrail_rejectreason"].ToString();
                        purchaseRequisitionTrailerModel.Status = (byte)purReqDataRow["purreqtrail_status"];
                        purchaseRequisitionTrailerModel.DepartmentPurchaseRequisitionTrailerSequenceNumber = (long)purReqDataRow["purreqtrail_deptpurreqtrail_seqno"];
                        purchaseRequisitionTrailerModel.DepartmentPurchaseRequisitionHeaderOrderSequenceNumber = (long)purReqDataRow["deptpurreqhead_order_seqno"];
                        purchaseRequisitionTrailerModel.ManagerApprovalStatus = (bool)purReqDataRow["purreqtrail_manager_approvalstatus"];
                        purchaseRequisitionTrailerModel.ManagerProcessedStatus = (bool)purReqDataRow["purreqtrail_manager_processedstatus"];
                        purchaseRequisitionTrailerModel.ManagerRejectReason = purReqDataRow["purreqtrail_manager_rejectreason"].ToString();
                        purchaseRequisitionTrailerModel.PurchaseOrderStatus= (bool)purReqDataRow["purreqtrail_po_status"];
                        purchaseRequisitionTrailerModel.ProductGroupName = purReqDataRow["prodgrp_name"].ToString();
                        purchaseRequisitionTrailerModel.ProductName = purReqDataRow["prod_name"].ToString();
                        purchaseRequisitionTrailerModel.LotType = purReqDataRow["lottyp_name"].ToString();
                        
                        purchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList.Add(purchaseRequisitionTrailerModel);
                    }

                }
            }

            return purchaseRequisitionHeaderModel;
        }

        public PurchaseRequisitionHeaderModel SelectPurchaseRequisition(long purchaseRequisitionNumber)
        {
            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();

            using (DbCommand getPurchaseRequisitionCommand = _dbHelper.GetStoredProcCommand("rx_sel_departmentpurchaserequisition"))
            {
                _dbHelper.AddInParameter(getPurchaseRequisitionCommand, "@deptpurreqhead_no",
                                    DbType.Int64, purchaseRequisitionNumber);

                using (DataSet getPurchaseRequisitionDataSet = _dbHelper.ExecuteDataSet(getPurchaseRequisitionCommand))
                {

                    //purchaseRequisitionHeaderModel.ProcessedStatus = (bool)getPurchaseRequisitionDataSet.Tables[0].Rows[0]["deptpurreqhead_processedstatus"];

                    foreach (DataRow purchaseRequisitionHeaderDataRow in getPurchaseRequisitionDataSet.Tables[0].Rows)
                    {
                        purchaseRequisitionHeaderModel.SequenceNumber = (long)purchaseRequisitionHeaderDataRow["purreqhead_seqno"];
                        purchaseRequisitionHeaderModel.PurchaseRequisitionNumber = (long)purchaseRequisitionHeaderDataRow["deptpurreqhead_no"];
                        purchaseRequisitionHeaderModel.PurchaseRequisitionDate = (DateTime)purchaseRequisitionHeaderDataRow["deptpurreqhead_date"];
                        //purchaseRequisitionHeaderModel.ProcessSequenceNumber = (long)departmentPurchaseRequisitionHeaderDataRow["deptpurreqhead_process_seqno"];
                        purchaseRequisitionHeaderModel.OrderNumber = (long)purchaseRequisitionHeaderDataRow["purreqhead_order_seqno"];
                        purchaseRequisitionHeaderModel.Remarks = purchaseRequisitionHeaderDataRow["purreqhead_remarks"].ToString();
                        purchaseRequisitionHeaderModel.Mode = EnumConstants.ScreenMode.Edit;
                    }
                    foreach (DataRow purchaseRequisitionTrailerDataRow in getPurchaseRequisitionDataSet.Tables[1].Rows)
                    {
                        PurchaseRequisitionTrailerModel purchaseRequisitionTrailerModel = new PurchaseRequisitionTrailerModel();
                        purchaseRequisitionTrailerModel.SequenceNumber = (long)purchaseRequisitionTrailerDataRow["deptpurreqtrail_seqno"];
                        purchaseRequisitionTrailerModel.PurchaseRequisitionHeaderSequenceNumber = (long)purchaseRequisitionTrailerDataRow["deptpurreqtrail_headseqno"];
                        purchaseRequisitionTrailerModel.ProductGroupSequenceNumber = (long)purchaseRequisitionTrailerDataRow["deptpurreqtrail_productgroup"];
                        purchaseRequisitionTrailerModel.ProductSequenceNumber = (long)purchaseRequisitionTrailerDataRow["deptpurreqtrail_product"];
                        purchaseRequisitionTrailerModel.Color = purchaseRequisitionTrailerDataRow["deptpurreqtrail_color"].ToString();
                        purchaseRequisitionTrailerModel.Size = purchaseRequisitionTrailerDataRow["deptpurreqtrail_size"].ToString();
                        purchaseRequisitionTrailerModel.SizeSpec = purchaseRequisitionTrailerDataRow["deptpurreqtrail_sizespec"].ToString();
                        purchaseRequisitionTrailerModel.Quantity = (decimal)purchaseRequisitionTrailerDataRow["deptpurreqtrail_qty"];
                        purchaseRequisitionTrailerModel.LotTypeSequenceNumber = (long)purchaseRequisitionTrailerDataRow["deptpurreqtrail_lottypseqno"];
                        purchaseRequisitionTrailerModel.ApprovalStatus = (bool)purchaseRequisitionTrailerDataRow["deptpurreqtrail_approvalstatus"];
                        purchaseRequisitionTrailerModel.Status = (byte)purchaseRequisitionTrailerDataRow["deptpurreqtrail_status"];
                        purchaseRequisitionTrailerModel.ManagerApprovalStatus = (bool)purchaseRequisitionTrailerDataRow["deptpurreqtrail_manager_approvalstatus"];
                        purchaseRequisitionTrailerModel.TransitBefore = Convert.ToDateTime(purchaseRequisitionTrailerDataRow["deptpurreqtrail_transitbefore"]).ToString("dd/MM/yyyy");
                        purchaseRequisitionTrailerModel.ProductGroupName = purchaseRequisitionTrailerDataRow["prodgrp_name"].ToString();
                        purchaseRequisitionTrailerModel.ProductName = purchaseRequisitionTrailerDataRow["prod_name"].ToString();
                        purchaseRequisitionTrailerModel.LotType = purchaseRequisitionTrailerDataRow["lottyp_name"].ToString();
                        purchaseRequisitionTrailerModel.LotQuantity = (decimal)purchaseRequisitionTrailerDataRow["lottyp_actualqty"];
                        purchaseRequisitionTrailerModel.State = EnumConstants.ModelCurrentState.UnChanged;
                        purchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList.Add(purchaseRequisitionTrailerModel);

                    }
                }
            }

            return purchaseRequisitionHeaderModel;
        }

        public PurchaseRequisitionHeaderModel SavePurchaseRequisition(PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel)
        {

            //if (purchaseRequisitionHeaderModel.Mode == EnumConstants.ScreenMode.New)
            //{
            //    List<DepartmentPurchaseRequisitionTrailerModel> departmentPurchaseRequisitionTrailerModel = new List<DepartmentPurchaseRequisitionTrailerModel>();
            //    departmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();

            //    DataTable departmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(departmentPurchaseRequisitionTrailerModel);

            //    using (DbCommand saveDepartmentPurchaseRequisitionHeaderCommand = _dbHelper.GetStoredProcCommand("rx_ins_departmentpurchaserequisition_header"))
            //    {

            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_process_seqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.ProcessSequenceNumber);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_order_seqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.OrderNumber);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_remarks", DbType.String,  departmentPurchaseRequisitionHeaderModel.Remarks);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_createdby", DbType.String, departmentPurchaseRequisitionHeaderModel.CreatedBy);


            //        IDataReader dbReader = _dbHelper.ExecuteReader(saveDepartmentPurchaseRequisitionHeaderCommand);
            //        dbReader.Read();
            //        departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber = long.Parse(dbReader["deptpurreqhead_no"].ToString());
            //        departmentPurchaseRequisitionHeaderModel.SequenceNumber = long.Parse(dbReader["deptpurreqhead_seqno"].ToString());

            //    }

            //    using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_ins_departmentpurchaserequisition_trailer"))
            //    {


            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_headseqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_productgroup", DbType.Int64, "ProductGroupSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_product", DbType.Int64, "ProductSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_lottypseqno", DbType.Int64, "LotTypeSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_color", DbType.String, "Color", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_size", DbType.String, "Size", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_sizespec", DbType.String, "SizeSpec", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_transitbefore", DbType.DateTime, "TransitBefore", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);
            //        _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, departmentPurchaseRequisitionTrailerDataTable);

            //    }
            //}
            //else
            //{
            //    List<DepartmentPurchaseRequisitionTrailerModel> addedDepartmentPurchaseRequisitionTrailerModel = new List<DepartmentPurchaseRequisitionTrailerModel>();
            //    addedDepartmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Added).ToList();

            //    List<DepartmentPurchaseRequisitionTrailerModel> updatedDepartmentPurchaseRequisitionTrailerModel = new List<DepartmentPurchaseRequisitionTrailerModel>();
            //    updatedDepartmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Updated).ToList();

            //    List<DepartmentPurchaseRequisitionTrailerModel> deletedDepartmentPurchaseRequisitionTrailerModel = new List<DepartmentPurchaseRequisitionTrailerModel>();
            //    deletedDepartmentPurchaseRequisitionTrailerModel = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList.Where(x => x.State == EnumConstants.ModelCurrentState.Deleted).ToList();

            //    DataTable addedDepartmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(addedDepartmentPurchaseRequisitionTrailerModel);
            //    DataTable updatedDepartmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(updatedDepartmentPurchaseRequisitionTrailerModel);
            //    DataTable deletedDepartmentPurchaseRequisitionTrailerDataTable = UtilityMethods.ToDataTable(deletedDepartmentPurchaseRequisitionTrailerModel);

            //    using (DbCommand saveDepartmentPurchaseRequisitionHeaderCommand = _dbHelper.GetStoredProcCommand("rx_upd_departmentpurchaserequisition_header"))
            //    {

            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_seqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_remarks", DbType.String, departmentPurchaseRequisitionHeaderModel.Remarks);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionHeaderCommand, "@deptpurreqhead_updatedby", DbType.String, departmentPurchaseRequisitionHeaderModel.UpdatedBy);

            //       _dbHelper.ExecuteNonQuery(saveDepartmentPurchaseRequisitionHeaderCommand);
                   
            //    }

            //    using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_ins_departmentpurchaserequisition_trailer"))
            //    {
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_headseqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_productgroup", DbType.Int64, "ProductGroupSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_product", DbType.Int64, "ProductSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_lottypseqno", DbType.Int64, "LotTypeSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_color", DbType.String, "Color", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_size", DbType.String, "Size", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_sizespec", DbType.String, "SizeSpec", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_transitbefore", DbType.DateTime, "TransitBefore", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);
            //        _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, addedDepartmentPurchaseRequisitionTrailerDataTable);

            //    }


            //    using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_upd_departmentpurchaserequisition_trailer"))
            //    {
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_seqno", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_headseqno", DbType.Int64, departmentPurchaseRequisitionHeaderModel.SequenceNumber);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_productgroup", DbType.Int64, "ProductGroupSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_product", DbType.Int64, "ProductSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_lottypseqno", DbType.Int64, "LotTypeSequenceNumber", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_color", DbType.String, "Color", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_size", DbType.String, "Size", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_sizespec", DbType.String, "SizeSpec", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_transitbefore", DbType.DateTime, "TransitBefore", DataRowVersion.Current);
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_qty", DbType.Decimal, "Quantity", DataRowVersion.Current);
            //        _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, updatedDepartmentPurchaseRequisitionTrailerDataTable);

            //    }


            //    using (DbCommand saveDepartmentPurchaseRequisitionDetailCommand = _dbHelper.GetStoredProcCommand("rx_del_departmentpurchaserequisition_trailer"))
            //    {
            //        _dbHelper.AddInParameter(saveDepartmentPurchaseRequisitionDetailCommand, "@deptpurreqtrail_seqno", DbType.Int64, "SequenceNumber", DataRowVersion.Current);
            //        _dbHelper.Fill(saveDepartmentPurchaseRequisitionDetailCommand, deletedDepartmentPurchaseRequisitionTrailerDataTable);

            //    }

            //}


            return purchaseRequisitionHeaderModel;
        }
        #endregion
    }
}
