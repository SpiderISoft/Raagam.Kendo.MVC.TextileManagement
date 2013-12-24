using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.TextileManagement.Model;
using Newtonsoft.Json;
using Raagam.MVC.TextileManagement.UI.ProductionService;
using Raagam.TextileManagement.CommonUtility;



namespace Raagam.MVC.TextileManagement.UI.Controllers
{
    public class FabricCuttingChartController : Controller
    {

        ProductionClient productionServiceClient;

        public FabricCuttingChartController()
        {
            productionServiceClient = new ProductionClient();
        }

        public ActionResult FabricCuttingChart()
        {
            string url = "~/Views/Production/FabricCuttingChart.cshtml";
            FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();
            return View(url, fabricCuttingChartModel);
        }

        public JsonResult LoadGrid()
        {
         
            FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();
            fabricCuttingChartModel.fabricCuttingChartMainList = new List<FabricCuttingChartMainModel>();


            return Json(new
            {
                fabricCuttingChartMainList = fabricCuttingChartModel.fabricCuttingChartMainList
                 
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrderDetails(long OrderNumber)
        {
            FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();
            fabricCuttingChartModel = productionServiceClient.GetOrderDetails(OrderNumber);
            TempData["fabricCuttingChartModel"] = fabricCuttingChartModel;

            return Json(new { success = true, data = new { 
                                                                OrderDate = fabricCuttingChartModel.OrderDate.ToString("dd/MM/yyyy"), 
                                                                DeliveryDate = fabricCuttingChartModel.DeliveryDate.ToString("dd/MM/yyyy"), 
                                                                BuyerReferenceNumber = fabricCuttingChartModel.BuyerReferenceNumber, 
                                                                OrderQuantity = fabricCuttingChartModel.OrderQuantity, 
                                                                BuyerDropDownList = fabricCuttingChartModel.BuyerDropDownList, 
                                                                BuyerSequenceNumber = fabricCuttingChartModel.BuyerSequenceNumber, 
                                                                StyleDropDownList = fabricCuttingChartModel.StyleDropDownList
                                                          } 
                            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeFabricPanelSize(int? style)
        {
            FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();
            fabricCuttingChartModel = (FabricCuttingChartModel)TempData["fabricCuttingChartModel"];

            var fabricList = fabricCuttingChartModel.FabricLinkDropDownList.AsQueryable();
            var panelList = fabricCuttingChartModel.PanelLinkDropDownList.AsQueryable();
            var styleSizeList = fabricCuttingChartModel.StyleSizeLinkDropDownList.AsQueryable();
            


            if (style != null)
            {
                fabricList = fabricList.Where(f => f.ForeignKey == style);
                panelList = panelList.Where(f => f.ForeignKey == style);
                styleSizeList = styleSizeList.Where(f => f.ForeignKey == style);
            }

            TempData["fabricCuttingChartModel"] = fabricCuttingChartModel;

            return Json(new
            {
                success = true,
                data = new
                {
                    FabricLinkDropDownList = fabricList,
                    PanelLinkDropDownList = panelList,
                    StyleSizeLinkDropDownList = styleSizeList,
                    fabricCuttingChartMainList = fabricCuttingChartModel.fabricCuttingChartMainList
                }
            }, JsonRequestBehavior.AllowGet);
        }

 

         
        //public ActionResult PopulateFabricCuttingChartGrid([DataSourceRequest]DataSourceRequest request, long styleSequenceNumber, long? fabricSequenceNumber)
        //{

        //    FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();
            
        //    fabricCuttingChartModel = (FabricCuttingChartModel)TempData["fabricCuttingChartModel"];

        //    IQueryable<FabricCuttingChartMainModel> fabricCuttingChartMainModel = fabricCuttingChartModel.fabricCuttingChartMainList.AsQueryable();

        //    if ((fabricSequenceNumber!= null)  && (fabricSequenceNumber != 0))
        //    {
        //        fabricCuttingChartMainModel = fabricCuttingChartMainModel.Where(f => f.StyleFabricSequenceNumber == fabricSequenceNumber);
        //    }

        //    DataSourceResult result = fabricCuttingChartMainModel.ToDataSourceResult(request);

        //    TempData["fabricCuttingChartModel"] = fabricCuttingChartModel;

        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public ActionResult AddFabricCuttingChart(string StyleSizeSelectedList, string PanelSelectedList, long styleSequenceNumber, long orderSequenceNumber, long styleFabricSequenceNumber, decimal weight, decimal tableDia, string loopLength, string knitGSM)
        {

            string sPanelName = "";

            List<long> StyleSizeSelected = StyleSizeSelectedList.Split(',').Select(long.Parse).ToList();
            List<long> PanelSelected = PanelSelectedList.Split(',').Select(long.Parse).ToList();

            List<FabricCuttingChartPanelColorModel> panelColorMatchedModelList = new List<FabricCuttingChartPanelColorModel>();
            List<long> panelColorMatchedSequenceNumberList = new List<long>();
            List<long> panelColorNotMatchedSequenceNumberList = new List<long>();


            FabricCuttingChartModel fabricCuttingChartModel = new FabricCuttingChartModel();

            fabricCuttingChartModel = (FabricCuttingChartModel)TempData["fabricCuttingChartModel"];

            var _fabricCuttingChartMainList = fabricCuttingChartModel.fabricCuttingChartMainList.AsQueryable();
            var _fabricCuttingChartDetailList = fabricCuttingChartModel.fabricCuttingChartDetailList.AsQueryable();

            var _fabricCuttingChartList = _fabricCuttingChartMainList.Join(_fabricCuttingChartDetailList, m => m.SequenceNumber, d => d.FabricCuttingChartMainSequenceNumber, (m, d) => new { MainList = m, DetailList = d }).AsQueryable();

            if (_fabricCuttingChartList.Where(x => StyleSizeSelected.Contains(x.MainList.StyleSizeSequenceNumber) && PanelSelected.Contains(x.DetailList.StylePanelSequenceNumber) && x.MainList.StyleFabricSequenceNumber == styleFabricSequenceNumber && x.MainList.OrderSequenceNumber == orderSequenceNumber).ToList().Count > 0)
            {
                TempData["fabricCuttingChartModel"] = fabricCuttingChartModel;
                return Json(new {
                                    success = false,
                                    errors =   "Already Some of this Combination is Available" 
                                }, JsonRequestBehavior.AllowGet);
            }




            List<FabricCuttingChartPanelColorModel> panelColorModelList = new List<FabricCuttingChartPanelColorModel>();

            var panelColorModelQueryable = fabricCuttingChartModel.panelColorModelList.AsQueryable();

            if (panelColorModelQueryable != null)
            {

                panelColorMatchedModelList = panelColorModelQueryable.Where(p => PanelSelected.Contains(p.StylePanelSequenceNumber) && p.StyleSequenceNumber == styleSequenceNumber).ToList();

                panelColorMatchedSequenceNumberList = panelColorMatchedModelList.Select(x => x.StylePanelSequenceNumber).ToList();

                panelColorNotMatchedSequenceNumberList = PanelSelected.Except(panelColorMatchedSequenceNumberList).ToList();


                foreach (long panelColorNotMatched in panelColorNotMatchedSequenceNumberList)
                {
                    string PanelName = fabricCuttingChartModel.PanelLinkDropDownList.Where(x => x.Key == panelColorNotMatched).Select(x => x.Value).First().ToString();

                    if (sPanelName.Length == 0)
                        sPanelName = PanelName;
                    else
                        sPanelName += "/" + PanelName;

                }
            }

            foreach (long itemSize in StyleSizeSelected)
            {
                int orderExcessQty = fabricCuttingChartModel.orderDetailsModelList.Where(x => x.StyleSizeSequenceNumber == itemSize).Select(x => x.OrderExcessQty).First();
                string sizeName = fabricCuttingChartModel.orderDetailsModelList.Where(x => x.StyleSizeSequenceNumber == itemSize).Select(x => x.SizeName).First();
                string fabricName = fabricCuttingChartModel.FabricLinkDropDownList.Where(x => x.Key == styleFabricSequenceNumber).Select(x => x.Value).First();
                string TempGUID = Guid.NewGuid().ToString();

                if (sPanelName.Length > 0)
                {

                    FabricCuttingChartMainModel fabricCuttingChartMain = new FabricCuttingChartMainModel();
                    fabricCuttingChartMain.OrderSequenceNumber = orderSequenceNumber;
                    fabricCuttingChartMain.StyleFabricSequenceNumber = styleFabricSequenceNumber;
                    fabricCuttingChartMain.StyleSizeSequenceNumber = itemSize;
                    fabricCuttingChartMain.StyleColorSequenceNumber = 0;
                    fabricCuttingChartMain.ColorName = "";
                    fabricCuttingChartMain.Weight = weight;
                    fabricCuttingChartMain.TableDia = tableDia;
                    fabricCuttingChartMain.KnitDia = tableDia;
                    fabricCuttingChartMain.Pieces = orderExcessQty;
                    fabricCuttingChartMain.WastagePercentage = 7;
                    fabricCuttingChartMain.TotalWeight = (weight + ((weight * 7) / 100)) * orderExcessQty;
                    fabricCuttingChartMain.ProductName = fabricName;
                    fabricCuttingChartMain.StylePanelName = sPanelName;
                    fabricCuttingChartMain.SizeName = sizeName;
                    fabricCuttingChartMain.TempGUID = TempGUID;
                    fabricCuttingChartMain.LoopLength = loopLength;
                    fabricCuttingChartMain.KnitGSM = knitGSM;
                    fabricCuttingChartMain.State = EnumConstants.ModelCurrentState.Added;

                    fabricCuttingChartModel.fabricCuttingChartMainList.Add(fabricCuttingChartMain);

                    foreach (long panelColorNotMatched in panelColorNotMatchedSequenceNumberList)
                    {
                        string PanelName = fabricCuttingChartModel.PanelLinkDropDownList.Where(x => x.Key == panelColorNotMatched).Select(x => x.Value).First().ToString();

                        FabricCuttingChartDetailModel fabricCuttingChartDetailModel = new FabricCuttingChartDetailModel();
                        fabricCuttingChartDetailModel.SequenceNumber = 0;
                        fabricCuttingChartDetailModel.StylePanelSequenceNumber = panelColorNotMatched;
                        fabricCuttingChartDetailModel.PanelName = PanelName;
                        fabricCuttingChartDetailModel.TempGUID = TempGUID;
                        fabricCuttingChartDetailModel.State = EnumConstants.ModelCurrentState.Added;
                        fabricCuttingChartModel.fabricCuttingChartDetailList.Add(fabricCuttingChartDetailModel);

                    }


                }



            }

            foreach (FabricCuttingChartPanelColorModel _fabricCuttingChartPanelColorModel in panelColorMatchedModelList)
            {

                if (panelColorModelQueryable.Where(p => p.StylePanelSequenceNumber == _fabricCuttingChartPanelColorModel.StylePanelSequenceNumber && p.StyleSequenceNumber == styleSequenceNumber && p.StyleSizeSequenceNumber == _fabricCuttingChartPanelColorModel.StyleSizeSequenceNumber).ToList().Count == 0)
                {
                    break;
                }
                string PanelName = fabricCuttingChartModel.PanelLinkDropDownList.Where(x => x.Key == _fabricCuttingChartPanelColorModel.StylePanelSequenceNumber).Select(x => x.Value).First().ToString();
                int orderExcessQty = fabricCuttingChartModel.orderDetailsModelList.Where(x => x.StyleSizeSequenceNumber == _fabricCuttingChartPanelColorModel.StyleSizeSequenceNumber).Select(x => x.OrderExcessQty).First();
                string fabricName = fabricCuttingChartModel.FabricLinkDropDownList.Where(x => x.Key == styleFabricSequenceNumber).Select(x => x.Value).First();


                FabricCuttingChartMainModel fabricCuttingChartMain = new FabricCuttingChartMainModel();
                fabricCuttingChartMain.OrderSequenceNumber = orderSequenceNumber;
                fabricCuttingChartMain.StyleFabricSequenceNumber = styleFabricSequenceNumber;
                fabricCuttingChartMain.StyleSizeSequenceNumber = _fabricCuttingChartPanelColorModel.StyleSizeSequenceNumber;
                fabricCuttingChartMain.StyleColorSequenceNumber = _fabricCuttingChartPanelColorModel.StyleColorSequenceNumber;
                fabricCuttingChartMain.ColorName = _fabricCuttingChartPanelColorModel.ColorName;
                fabricCuttingChartMain.Weight = weight;
                fabricCuttingChartMain.TableDia = tableDia;
                fabricCuttingChartMain.KnitDia = tableDia;
                fabricCuttingChartMain.Pieces = orderExcessQty;
                fabricCuttingChartMain.WastagePercentage = 7;
                fabricCuttingChartMain.TotalWeight = (weight + ((weight * 7) / 100)) * orderExcessQty;
                fabricCuttingChartMain.ProductName = fabricName;
                fabricCuttingChartMain.StylePanelName = sPanelName;
                fabricCuttingChartMain.SizeName = _fabricCuttingChartPanelColorModel.SizeName;
                fabricCuttingChartMain.TempGUID = Guid.NewGuid().ToString();
                fabricCuttingChartMain.LoopLength = loopLength;
                fabricCuttingChartMain.KnitGSM = knitGSM;
                fabricCuttingChartMain.State = EnumConstants.ModelCurrentState.Added;

                fabricCuttingChartModel.fabricCuttingChartMainList.Add(fabricCuttingChartMain);


                FabricCuttingChartDetailModel fabricCuttingChartDetailModel = new FabricCuttingChartDetailModel();
                fabricCuttingChartDetailModel.SequenceNumber = 0;
                fabricCuttingChartDetailModel.StylePanelSequenceNumber = _fabricCuttingChartPanelColorModel.StylePanelSequenceNumber;
                fabricCuttingChartDetailModel.PanelName = PanelName;
                fabricCuttingChartDetailModel.TempGUID = _fabricCuttingChartPanelColorModel.TempGUID;
                fabricCuttingChartDetailModel.State = EnumConstants.ModelCurrentState.Added;
                fabricCuttingChartModel.fabricCuttingChartDetailList.Add(fabricCuttingChartDetailModel);
            }



            IQueryable<FabricCuttingChartMainModel> fabricCuttingChartMainModel = fabricCuttingChartModel.fabricCuttingChartMainList.AsQueryable();

 


            TempData["fabricCuttingChartModel"] = fabricCuttingChartModel;

            return Json(new
            {
                success = true,
                data = new
                {
                    fabricCuttingChartMainList = fabricCuttingChartModel.fabricCuttingChartMainList
                }
            }, JsonRequestBehavior.AllowGet);



        }

    }
}
