using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.MVC.TextileManagement.UI.ProductionService;
using Raagam.TextileManagement.Model;
using Raagam.MVC.TextileManagement.UI.Filters.Helpers;
using System.Text;

namespace Raagam.MVC.TextileManagement.UI.Controllers
{
    public class GeneralDepartmentPurchaseRequisitionController : Controller
    {
        ProductionClient productionServiceClient;

        public GeneralDepartmentPurchaseRequisitionController()
        {
            productionServiceClient = new ProductionClient();
        }

        public ActionResult GeneralDepartmentPurchaseRequisition()
        {
            string url = "~/Views/Production/GeneralDepartmentPurchaseRequisition.cshtml";
            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();
            departmentPurchaseRequisitionHeaderModel = productionServiceClient.GeneralDepartmentPurReqPopulateDropDown();
            departmentPurchaseRequisitionHeaderModel.Mode = Raagam.TextileManagement.CommonUtility.EnumConstants.ScreenMode.New;
            TempData["departmentPurchaseRequisitionHeaderModel"] = departmentPurchaseRequisitionHeaderModel;
            return View(url, departmentPurchaseRequisitionHeaderModel);
        }


        public ActionResult SelectProduct()
        {
            string url = "~/Views/Production/SelectGeneralProduct.cshtml";
            return PartialView(url);
        }

        public JsonResult GetLotQuantity(long LotTypeSequenceNumber)
        {
            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();
            departmentPurchaseRequisitionHeaderModel = (GeneralDepartmentPurchaseRequisitionHeaderModel)TempData["departmentPurchaseRequisitionHeaderModel"];

            decimal LotTypeQuantity = departmentPurchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.LotTypeSequenceNumber == LotTypeSequenceNumber).FirstOrDefault().LotTypeQuantity;

            TempData["departmentPurchaseRequisitionHeaderModel"] = departmentPurchaseRequisitionHeaderModel;
            return Json(new
            {
                success = true,
                data = LotTypeQuantity
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSelectedProduct(List<SelectProductGridModel> selectProductGridModelList)
        {
            List<GeneralDepartmentPurchaseRequisitionTrailerModel> departmentPurchaseRequisitionTrailerModelList = new List<GeneralDepartmentPurchaseRequisitionTrailerModel>();
            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();
            departmentPurchaseRequisitionHeaderModel = (GeneralDepartmentPurchaseRequisitionHeaderModel)TempData["departmentPurchaseRequisitionHeaderModel"];

            if (selectProductGridModelList != null)
            {
                foreach (SelectProductGridModel selectProductGridModel in selectProductGridModelList)
                {
                    if (selectProductGridModel.Select == true)
                    {
                        GeneralDepartmentPurchaseRequisitionTrailerModel departmentPurchaseRequisitionTrailerModel = new GeneralDepartmentPurchaseRequisitionTrailerModel();
                        departmentPurchaseRequisitionTrailerModel.ProductGroupSequenceNumber = selectProductGridModel.ProductGroupSequenceNumber;
                        departmentPurchaseRequisitionTrailerModel.ProductSequenceNumber = selectProductGridModel.ProductSequenceNumber;
                        departmentPurchaseRequisitionTrailerModel.ProductGroupName = departmentPurchaseRequisitionHeaderModel.ProductGroupModelList.Where(x => x.ProductGroupSequenceNumber == selectProductGridModel.ProductGroupSequenceNumber).Select(x => x.ProductGroupName).FirstOrDefault();
                        departmentPurchaseRequisitionTrailerModel.ProductName = departmentPurchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSequenceNumber == selectProductGridModel.ProductSequenceNumber).Select(x => x.ProductName).FirstOrDefault();
                        departmentPurchaseRequisitionTrailerModel.LotType = departmentPurchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.LotTypeSequenceNumber == selectProductGridModel.LotTypeSequenceNumber).Select(x => x.LotTypeName).FirstOrDefault();

                        //long CategorySequenceNumber = departmentPurchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSequenceNumber == selectProductGridModel.ProductSequenceNumber).Select(x => x.ProductCategorySequenceNumber).FirstOrDefault();
                        decimal LotTypeQuantity = departmentPurchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.LotTypeSequenceNumber == selectProductGridModel.LotTypeSequenceNumber).FirstOrDefault().LotTypeQuantity;

                        departmentPurchaseRequisitionTrailerModel.LotQuantity = LotTypeQuantity;
                        departmentPurchaseRequisitionTrailerModel.Quantity = 1;
                        departmentPurchaseRequisitionTrailerModel.LotTypeSequenceNumber = selectProductGridModel.LotTypeSequenceNumber;
                        //departmentPurchaseRequisitionTrailerModel.Color = "";
                        //departmentPurchaseRequisitionTrailerModel.Size = "";
                        //departmentPurchaseRequisitionTrailerModel.SizeSpec = "";
                        //departmentPurchaseRequisitionTrailerModel.TransitBefore = DateTime.Now.ToString("dd/MM/yyyy");
                        departmentPurchaseRequisitionTrailerModel.State = Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState.Added;
                        departmentPurchaseRequisitionTrailerModelList.Add(departmentPurchaseRequisitionTrailerModel);
                    }

                }
            }
            var count = departmentPurchaseRequisitionTrailerModelList.Count();

            //paging
            var data = departmentPurchaseRequisitionTrailerModelList.ToArray();


            var result = new
            {
                total = 1,
                page = 1,
                records = count,
                rows = (from host in data
                        select new
                        {
                            SequenceNumber  = host.SequenceNumber ,
                            DepartmentPurchaseRequisitionHeaderSequenceNumber=host.DepartmentPurchaseRequisitionHeaderSequenceNumber,
                            ProductGroupSequenceNumber=host.ProductGroupSequenceNumber,
                            ProductSequenceNumber=host.ProductSequenceNumber,
                            ProductGroupName= host.ProductGroupName,
                            ProductName = host.ProductName,
                            //Color= host.Color,
                            //Size= host.Size,
                            //SizeSpec=host.SizeSpec,
                            LotQuantity = host.LotQuantity,
                            Quantity= host.Quantity,
                            ApprovedQuantity= host.ApprovedQuantity,
                            ActiveStatus= host.ActiveStatus,
                            ApprovalStatus= host.ApprovalStatus,
                            RejectReason= host.RejectReason,
                            LotTypeSequenceNumber=host.LotTypeSequenceNumber,
                            LotType = host.LotType,
                            Status=host.Status,
                            ManagerApprovalStatus=host.ManagerApprovalStatus,
                            ManagerApprovedQuantity=host.ManagerApprovedQuantity,
                            ManagerRejectReason=host.ManagerRejectReason,
                            ReceivedQuantity=host.ReceivedQuantity,
                            //TransitBefore= host.TransitBefore,
                            State = host.State
                        }).ToArray()
            };

            TempData["departmentPurchaseRequisitionHeaderModel"] = departmentPurchaseRequisitionHeaderModel;
 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLotType(long productSequenceNumber)
        {
            StringBuilder LotTypeStringBuilder = new StringBuilder();

            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();

            departmentPurchaseRequisitionHeaderModel = (GeneralDepartmentPurchaseRequisitionHeaderModel)TempData["departmentPurchaseRequisitionHeaderModel"];

            long CategorySequenceNumber = departmentPurchaseRequisitionHeaderModel.ProductModelList.Where(x => x.ProductSequenceNumber == productSequenceNumber).Select(x => x.ProductCategorySequenceNumber).FirstOrDefault();

            var LotTypeModelList = departmentPurchaseRequisitionHeaderModel.LotTypeModelList.Where(x => x.UnitCategorySequenceNumber == CategorySequenceNumber).AsQueryable();

            foreach (var q in LotTypeModelList)
            {
                if (LotTypeStringBuilder.Length != 0)
                    LotTypeStringBuilder.Append(';');
                LotTypeStringBuilder.Append(q.LotTypeSequenceNumber); // instead of sb.Append("ID");
                LotTypeStringBuilder.Append(':');
                LotTypeStringBuilder.Append(q.LotTypeName);
            }


            TempData["departmentPurchaseRequisitionHeaderModel"] = departmentPurchaseRequisitionHeaderModel;

            return Json(new
            {
                success = true,
                data = LotTypeStringBuilder.ToString()
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveDepartmentPurchaseRequisition(GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel, List<GeneralDepartmentPurchaseRequisitionTrailerModel> departmentPurchaseRequisitionTrailerModel)
        {

            departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList = departmentPurchaseRequisitionTrailerModel;


            departmentPurchaseRequisitionHeaderModel = productionServiceClient.SaveGeneralDepartmentPurchaseRequisition(departmentPurchaseRequisitionHeaderModel);
 
            return Json(new
            {
                success = true,
                data = new
                {
                    SequenceNumber = departmentPurchaseRequisitionHeaderModel.SequenceNumber,
                    DepartmentPurchaseRequisitionNumber = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber 
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectDepartmentPurchaseRequisition(long DepartmentPurchaseRequisitionNumber)
        {
            GeneralDepartmentPurchaseRequisitionHeaderModel TempDepartmentPurchaseRequisitionHeaderModel = new GeneralDepartmentPurchaseRequisitionHeaderModel();

            TempDepartmentPurchaseRequisitionHeaderModel = (GeneralDepartmentPurchaseRequisitionHeaderModel)TempData["departmentPurchaseRequisitionHeaderModel"];

            GeneralDepartmentPurchaseRequisitionHeaderModel departmentPurchaseRequisitionHeaderModel = productionServiceClient.SelectGeneralDepartmentPurchaseRequisition(DepartmentPurchaseRequisitionNumber);

            TempDepartmentPurchaseRequisitionHeaderModel.Mode = departmentPurchaseRequisitionHeaderModel.Mode;
            TempDepartmentPurchaseRequisitionHeaderModel.SequenceNumber = departmentPurchaseRequisitionHeaderModel.SequenceNumber;
            TempDepartmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber;
            TempDepartmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionDate = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionDate;
            TempDepartmentPurchaseRequisitionHeaderModel.ProcessSequenceNumber = departmentPurchaseRequisitionHeaderModel.ProcessSequenceNumber;
            //TempDepartmentPurchaseRequisitionHeaderModel.OrderNumber = departmentPurchaseRequisitionHeaderModel.OrderNumber;
            TempDepartmentPurchaseRequisitionHeaderModel.Remarks = departmentPurchaseRequisitionHeaderModel.Remarks;
            TempDepartmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList;

            
            TempData["departmentPurchaseRequisitionHeaderModel"] = TempDepartmentPurchaseRequisitionHeaderModel;

            return Json(new
            {
                success = true,
                data = new
                {
                    Mode = departmentPurchaseRequisitionHeaderModel.Mode.ToString(),
                    SequenceNumber = departmentPurchaseRequisitionHeaderModel.SequenceNumber,
                    DepartmentPurchaseRequisitionNumber = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionNumber,
                    DepartmentPurchaseRequisitionDate = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionDate.ToString("dd/MM/yyyy"),
                    ProcessSequenceNumber = departmentPurchaseRequisitionHeaderModel.ProcessSequenceNumber,
                    //OrderNumber = departmentPurchaseRequisitionHeaderModel.OrderNumber,
                    Remarks = departmentPurchaseRequisitionHeaderModel.Remarks,
                    DepartmentPurchaseRequisitionTrailerModelList = departmentPurchaseRequisitionHeaderModel.DepartmentPurchaseRequisitionTrailerModelList 

                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProduct(GridSettings grid)
        {
            List<SelectProductGridModel> selectProductGridModelList = new List<SelectProductGridModel>();
            selectProductGridModelList = productionServiceClient.SelectProductGrid(0, "");

            var selectProductGridModel = selectProductGridModelList.AsQueryable();


            if (grid.IsSearch)
            {
                 
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        selectProductGridModel = selectProductGridModel.Where<SelectProductGridModel>(rule.field, rule.data, (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                     
                    var temp = (new List<SelectProductGridModel>()).AsQueryable();
                    foreach (var rule in grid.Where.rules)
                    {
                        var t = selectProductGridModel.Where<SelectProductGridModel>(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        temp = temp.Concat<SelectProductGridModel>(t);
                    }

                    selectProductGridModel = temp.Distinct<SelectProductGridModel>();
                }
            }


            //sorting
            selectProductGridModel = selectProductGridModel.OrderBy<SelectProductGridModel>(grid.SortColumn,grid.SortOrder);

            //count
            var count = selectProductGridModel.Count();

            //paging
            var data = selectProductGridModel.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize).ToArray();


            //converting in grid format
            var result = new
            {
                total = (int)Math.Ceiling((double)count / grid.PageSize),
                page = grid.PageIndex,
                records = count,
                rows = (from host in data
                        select new
                        {
                            Select = host.Select,
                            ProductGroupSequenceNumber = host.ProductGroupSequenceNumber,
                            ProductSequenceNumber = host.ProductSequenceNumber,
                            ProductName = host.ProductName,
                            ManufacturerName = host.ManufacturerName,
                            StockCurrentQuantity = host.StockCurrentQuantity,
                            ProductReOrderQuantity = host.ProductReOrderQuantity,
                            LotTypeSequenceNumber = host.LotTypeSequenceNumber,
                            ProductTax = host.ProductTax
                        }).ToArray()
            };

            //convert to JSON and return to client
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
