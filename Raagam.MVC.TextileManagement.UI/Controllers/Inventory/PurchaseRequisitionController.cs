using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using Raagam.TextileManagement.Model;
using Raagam.MVC.TextileManagement.UI.Filters.Helpers;
using System.Text;
using Raagam.MVC.TextileManagement.UI.InventoryService;

namespace Raagam.MVC.TextileManagement.UI.Controllers
{
    public class PurchaseRequisitionController : Controller
    {
        InventoryClient inventoryServiceClient;

        public PurchaseRequisitionController()
        {
            inventoryServiceClient = new InventoryClient();
        }

        public ActionResult PurchaseRequisition()
        {
            string url = "~/Views/Inventory/PurchaseRequisition.cshtml";
            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();
            purchaseRequisitionHeaderModel = inventoryServiceClient.PurReqPopulateDropDown();
            purchaseRequisitionHeaderModel.Mode = Raagam.TextileManagement.CommonUtility.EnumConstants.ScreenMode.New;
            TempData["purchaseRequisitionHeaderModel"] = purchaseRequisitionHeaderModel;
            return View(url, purchaseRequisitionHeaderModel);
        }


        public ActionResult SelectProduct()
        {
            string url = "~/Views/Production/SelectProduct.cshtml";
            return PartialView(url);
        }

        public JsonResult GetLotQuantity(long LotTypeSequenceNumber)
        {
            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();
            purchaseRequisitionHeaderModel = (PurchaseRequisitionHeaderModel)TempData["purchaseRequisitionHeaderModel"];

            decimal LotTypeQuantity = purchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.LotTypeSequenceNumber == LotTypeSequenceNumber).FirstOrDefault().LotTypeQuantity;

            TempData["purchaseRequisitionHeaderModel"] = purchaseRequisitionHeaderModel;
            return Json(new
            {
                success = true,
                data = LotTypeQuantity
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSelectedProduct(List<SelectProductGridModel> selectProductGridModelList)
        {
            List<PurchaseRequisitionTrailerModel> purchaseRequisitionTrailerModelList = new List<PurchaseRequisitionTrailerModel>();
            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();
            purchaseRequisitionHeaderModel = (PurchaseRequisitionHeaderModel)TempData["purchaseRequisitionHeaderModel"];

            if (selectProductGridModelList != null)
            {
                foreach (SelectProductGridModel selectProductGridModel in selectProductGridModelList)
                {
                    if (selectProductGridModel.Select == true)
                    {
                        PurchaseRequisitionTrailerModel purchaseRequisitionTrailerModel = new PurchaseRequisitionTrailerModel();
                        purchaseRequisitionTrailerModel.ProductGroupSequenceNumber = selectProductGridModel.ProductGroupSequenceNumber;
                        purchaseRequisitionTrailerModel.ProductSequenceNumber = selectProductGridModel.ProductSequenceNumber;
                        purchaseRequisitionTrailerModel.ProductGroupName = purchaseRequisitionHeaderModel.ProductGroupModelList.Where(x => x.ProductGroupSequenceNumber == selectProductGridModel.ProductGroupSequenceNumber).Select(x => x.ProductGroupName).FirstOrDefault();
                        purchaseRequisitionTrailerModel.ProductName = purchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSequenceNumber == selectProductGridModel.ProductSequenceNumber).Select(x => x.ProductName).FirstOrDefault();
                        purchaseRequisitionTrailerModel.LotType = purchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.LotTypeSequenceNumber == selectProductGridModel.LotTypeSequenceNumber).Select(x => x.LotTypeName).FirstOrDefault();

                        //long CategorySequenceNumber = departmentPurchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSequenceNumber == selectProductGridModel.ProductSequenceNumber).Select(x => x.ProductCategorySequenceNumber).FirstOrDefault();
                        decimal LotTypeQuantity = purchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.LotTypeSequenceNumber == selectProductGridModel.LotTypeSequenceNumber).FirstOrDefault().LotTypeQuantity;

                        purchaseRequisitionTrailerModel.LotQuantity = LotTypeQuantity;
                        purchaseRequisitionTrailerModel.Quantity = 1;
                        purchaseRequisitionTrailerModel.LotTypeSequenceNumber = selectProductGridModel.LotTypeSequenceNumber;
                        purchaseRequisitionTrailerModel.Color = "";
                        purchaseRequisitionTrailerModel.Size = "";
                        purchaseRequisitionTrailerModel.SizeSpec = "";
                        purchaseRequisitionTrailerModel.TransitBefore = DateTime.Now.ToString("dd/MM/yyyy");
                        purchaseRequisitionTrailerModel.State = Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState.Added;
                        purchaseRequisitionTrailerModelList.Add(purchaseRequisitionTrailerModel);
                    }

                }
            }
            var count = purchaseRequisitionTrailerModelList.Count();

            //paging
            var data = purchaseRequisitionTrailerModelList.ToArray();


            var result = new
            {
                total = 1,
                page = 1,
                records = count,
                rows = (from host in data
                        select new
                        {
                            SequenceNumber = host.SequenceNumber,
                            PurchaseRequisitionHeaderSequenceNumber = host.PurchaseRequisitionHeaderSequenceNumber,
                            ProductGroupSequenceNumber = host.ProductGroupSequenceNumber,
                            ProductSequenceNumber = host.ProductSequenceNumber,
                            ProductGroupName = host.ProductGroupName,
                            ProductName = host.ProductName,
                            Color = host.Color,
                            Size = host.Size,
                            SizeSpec = host.SizeSpec,
                            LotQuantity = host.LotQuantity,
                            Quantity = host.Quantity,
                            ApprovedQuantity = host.ApprovedQuantity,
                            ActiveStatus = host.ActiveStatus,
                            ApprovalStatus = host.ApprovalStatus,
                            RejectReason = host.RejectReason,
                            LotTypeSequenceNumber = host.LotTypeSequenceNumber,
                            LotType = host.LotType,
                            Status = host.Status,
                            ManagerApprovalStatus = host.ManagerApprovalStatus,
                            ManagerApprovedQuantity = host.ManagerApprovedQuantity,
                            ManagerRejectReason = host.ManagerRejectReason,
                            ReceivedQuantity = host.ReceivedQuantity,
                            TransitBefore = host.TransitBefore,
                            State = host.State
                        }).ToArray()
            };

            TempData["purchaseRequisitionHeaderModel"] = purchaseRequisitionHeaderModel;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
 
        public JsonResult GetSuppliers(long productSequenceNumber)
        {
            StringBuilder SuppliersStringBuilder = new StringBuilder();

            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();

            purchaseRequisitionHeaderModel = (PurchaseRequisitionHeaderModel)TempData["purchaseRequisitionHeaderModel"];

            List<long> SupplierSequenceNumbers = purchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSequenceNumber == productSequenceNumber).Select(x => x.ProductSupplierSequenceNumber).ToList();

            var SuppliersModelList = purchaseRequisitionHeaderModel.SupplierDropDownList.Where(x => SupplierSequenceNumbers.Contains(Convert.ToInt64(x.Value))).AsQueryable();

            SuppliersStringBuilder.Append(0); // instead of sb.Append("ID");
            SuppliersStringBuilder.Append(':');
            SuppliersStringBuilder.Append("Select Supplier");

            foreach (var q in SuppliersModelList)
            {
                if (SuppliersStringBuilder.Length != 0)
                    SuppliersStringBuilder.Append(';');
                SuppliersStringBuilder.Append(q.Value); // instead of sb.Append("ID");
                SuppliersStringBuilder.Append(':');
                SuppliersStringBuilder.Append(q.Text);
            }


            TempData["purchaseRequisitionHeaderModel"] = purchaseRequisitionHeaderModel;

            return Json(new
            {
                success = true,
                data = SuppliersStringBuilder.ToString()
            }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetSupplierInformation(long supplierSequenceNumber)
        {
            
            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();

            purchaseRequisitionHeaderModel = (PurchaseRequisitionHeaderModel)TempData["purchaseRequisitionHeaderModel"];

            ProductModel productModel = purchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSupplierSequenceNumber == supplierSequenceNumber).FirstOrDefault();
        
            TempData["purchaseRequisitionHeaderModel"] = purchaseRequisitionHeaderModel;

            return Json(new
            {
                success = true,
                data = new
                {
                    ProductTax = productModel.ProductTax,
                    CostPrice = productModel.CostPrice 

                }
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SavePurchaseRequisition(PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel, List<PurchaseRequisitionTrailerModel> purchaseRequisitionTrailerModel)
        {

            purchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList = purchaseRequisitionTrailerModel;


            purchaseRequisitionHeaderModel = inventoryServiceClient.SavePurchaseRequisition(purchaseRequisitionHeaderModel);
 
            return Json(new
            {
                success = true,
                data = new
                {
                    SequenceNumber = purchaseRequisitionHeaderModel.SequenceNumber,
                    PurchaseRequisitionNumber = purchaseRequisitionHeaderModel.PurchaseRequisitionNumber 
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectPurchaseRequisition(long PurchaseRequisitionNumber)
        {
            PurchaseRequisitionHeaderModel TempPurchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();

            TempPurchaseRequisitionHeaderModel = (PurchaseRequisitionHeaderModel)TempData["purchaseRequisitionHeaderModel"];

            PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = inventoryServiceClient.SelectPurchaseRequisition(PurchaseRequisitionNumber);

            TempPurchaseRequisitionHeaderModel.Mode = purchaseRequisitionHeaderModel.Mode;
            TempPurchaseRequisitionHeaderModel.SequenceNumber = purchaseRequisitionHeaderModel.SequenceNumber;
            TempPurchaseRequisitionHeaderModel.PurchaseRequisitionNumber = purchaseRequisitionHeaderModel.PurchaseRequisitionNumber;
            TempPurchaseRequisitionHeaderModel.PurchaseRequisitionDate = purchaseRequisitionHeaderModel.PurchaseRequisitionDate;
            //TempPurchaseRequisitionHeaderModel.ProcessSequenceNumber = purchaseRequisitionHeaderModel.ProcessSequenceNumber;
            TempPurchaseRequisitionHeaderModel.OrderNumber = purchaseRequisitionHeaderModel.OrderNumber;
            TempPurchaseRequisitionHeaderModel.Remarks = purchaseRequisitionHeaderModel.Remarks;
            TempPurchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList = purchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList;


            TempData["purchaseRequisitionHeaderModel"] = TempPurchaseRequisitionHeaderModel;

            return Json(new
            {
                success = true,
                data = new
                {
                    Mode = purchaseRequisitionHeaderModel.Mode.ToString(),
                    SequenceNumber = purchaseRequisitionHeaderModel.SequenceNumber,
                    DepartmentPurchaseRequisitionNumber = purchaseRequisitionHeaderModel.PurchaseRequisitionNumber,
                    DepartmentPurchaseRequisitionDate = purchaseRequisitionHeaderModel.PurchaseRequisitionDate.ToString("dd/MM/yyyy"),
                    //ProcessSequenceNumber = purchaseRequisitionHeaderModel.ProcessSequenceNumber,
                    OrderNumber = purchaseRequisitionHeaderModel.OrderNumber,
                    Remarks = purchaseRequisitionHeaderModel.Remarks,
                    PurchaseRequisitionTrailerModelList = purchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList 

                }
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SelectOrder(long OrderNumber)
        {
            PurchaseRequisitionHeaderModel TempPurchaseRequisitionHeaderModel = new PurchaseRequisitionHeaderModel();

            TempPurchaseRequisitionHeaderModel = (PurchaseRequisitionHeaderModel)TempData["purchaseRequisitionHeaderModel"];

            //PurchaseRequisitionHeaderModel purchaseRequisitionHeaderModel = inventoryServiceClient.SelectOrderForApproval(OrderNumber);

            //TempPurchaseRequisitionHeaderModel.Mode = purchaseRequisitionHeaderModel.Mode;
            //TempPurchaseRequisitionHeaderModel.SequenceNumber = purchaseRequisitionHeaderModel.SequenceNumber;
            //TempPurchaseRequisitionHeaderModel.PurchaseRequisitionNumber = purchaseRequisitionHeaderModel.PurchaseRequisitionNumber;
            //TempPurchaseRequisitionHeaderModel.PurchaseRequisitionDate = purchaseRequisitionHeaderModel.PurchaseRequisitionDate;
            //TempPurchaseRequisitionHeaderModel.ProcessSequenceNumber = purchaseRequisitionHeaderModel.ProcessSequenceNumber;
            //TempPurchaseRequisitionHeaderModel.OrderNumber = purchaseRequisitionHeaderModel.OrderNumber;
            //TempPurchaseRequisitionHeaderModel.Remarks = purchaseRequisitionHeaderModel.Remarks;
            TempPurchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList = TempPurchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList.Where(x => x.DepartmentPurchaseRequisitionHeaderOrderSequenceNumber == OrderNumber).ToList();


            TempData["purchaseRequisitionHeaderModel"] = TempPurchaseRequisitionHeaderModel;

            return Json(new
            {
                success = true,
                data = new
                {
                    Mode = TempPurchaseRequisitionHeaderModel.Mode.ToString(),
                    //SequenceNumber = purchaseRequisitionHeaderModel.SequenceNumber,
                    PurchaseRequisitionNumber = TempPurchaseRequisitionHeaderModel.PurchaseRequisitionNumber,
                    PurchaseRequisitionDate = TempPurchaseRequisitionHeaderModel.PurchaseRequisitionDate.ToString("dd/MM/yyyy"),
                    //ProcessSequenceNumber = purchaseRequisitionHeaderModel.ProcessSequenceNumber,
                    OrderNumber = TempPurchaseRequisitionHeaderModel.OrderNumber,
                    Remarks = TempPurchaseRequisitionHeaderModel.Remarks,
                    PurchaseRequisitionTrailerModelList = TempPurchaseRequisitionHeaderModel.PurchaseRequisitionTrailerModelList

                }
            }, JsonRequestBehavior.AllowGet);
        }
 

    }
}
