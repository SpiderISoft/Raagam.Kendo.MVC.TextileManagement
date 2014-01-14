using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.MVC.TextileManagement.UI.MerchandiserService;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.MVC.TextileManagement.UI.Controllers.Merchandiser
{
    public class StyleController : Controller
    {
       MerchandiserClient merchandiserServiceClient;

       public StyleController()
        {
            merchandiserServiceClient = new MerchandiserClient();
        }

        public ActionResult Style()
        {
            string url = "~/Views/Merchandiser/Style.cshtml";
            StyleListModel styleModelList = new StyleListModel();
            styleModelList = merchandiserServiceClient.StylePopulateDropDown();
            ViewBag.SizeDropDownList = styleModelList.SizeDropDownList;
            ViewBag.ColorDropDownList = styleModelList.ColorDropDownList;
            ViewBag.ProcessDropDownList = styleModelList.ProcessDropDownList;
            ViewBag.ProductGroupDropDownList = styleModelList.ProductGroupDropDownList;
            ViewBag.StyleTypeDropDownList = styleModelList.StyleTypeDropDownList;
            ViewBag.FabricDropDownList = styleModelList.FabricDropDownList;
 
            TempData["styleModelList"] = styleModelList;
            return View(url, styleModelList.styleModel);
        }

        public JsonResult GetCascadeProduct(int? productGroupSequence)
        {

            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];



            var productList = styleModelList.ProductDropDownList.AsQueryable();



            if (productGroupSequence != null)
            {
                productList = productList.Where(f => f.ForeignKey == productGroupSequence);
                 
            }

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success = true,
                data = new
                {
                    ProductLinkDropDownList = productList 
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeLotType(int? product)
        {

            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];


            var lotTypeList = styleModelList.LotTypeDropDownList.AsQueryable();

            if (product != null)
            {
                lotTypeList = lotTypeList.Where(f => f.ForeignKey == product);

            }

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success = true,
                data = new
                {
                    LotTypeLinkDropDownList = lotTypeList
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetColors()
        {

            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

            TempData["styleModelList"] = styleModelList;



           return Json(new
           {
               success = true,
               data = new
               {
                   ColorDropDownList = styleModelList.ColorDropDownList,
 
               }
           }, JsonRequestBehavior.AllowGet);
 
        }

        public JsonResult GetStyleColor(int page = 1, int rows = 10, string sord = "asc", string sidx = "Id")
        {
            StyleListModel styleModelList = (StyleListModel)TempData["styleModelList"];

            TempData["styleModelList"] = styleModelList;


            var recordCount = styleModelList.styleModel.StyleColorModelList.Count;
            IEnumerable<object> final;

            final = styleModelList.styleModel.StyleColorModelList.Select(e => new
            {
                SequenceNumber = e.SequenceNumber,
                StyleSequenceNumber = e.StyleSequenceNumber,
                ColorSequenceNumber = e.ColorSequenceNumber,
                ColorName = e.ColorName,
                ColorPantone = e.ColorPantone,
                IsDeleted = e.IsDeleted.ToString(),
                State = e.State.ToString()
            }).ToArray();


            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                total = Math.Ceiling((Decimal)recordCount / (Decimal)rows),
                page = page,
                records = recordCount,
                rows = final
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStylePanel(int page = 1, int rows = 10, string sord = "asc", string sidx = "Id")
        {
            StyleListModel styleModelList = (StyleListModel)TempData["styleModelList"];

            TempData["styleModelList"] = styleModelList;

            var recordCount = styleModelList.styleModel.StylePanelModelList.Count;
            IEnumerable<object> final;

            final = styleModelList.styleModel.StylePanelModelList.Select(e => new
            {
                SequenceNumber = e.SequenceNumber,
                StyleSequenceNumber = e.StyleSequenceNumber,
                PanelName = e.PanelName,
                PanelDescription = e.PanelDescription,
                IsDeleted = e.IsDeleted.ToString(),
                State = e.State.ToString(),
                TempGuid = e.TempGuid.ToString()
            }).ToArray();

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                total = Math.Ceiling((Decimal)recordCount / (Decimal)rows),
                page = page,
                records = recordCount,
                rows = final
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStyle(int page = 1, int rows = 10, string sord = "asc", string sidx = "Id")
        {
            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

            var recordCount = styleModelList.styleModelList.Count;
            IEnumerable<object> final;

            final = styleModelList.styleModelList.Select(e => new
            {
                StyleSequenceNumber = e.StyleSequenceNumber,
                StyleName = e.StyleName,
                StyleDescription = e.StyleDescription,
                StyleType = e.StyleTypeName,
                StyleTypeSequenceNumber = e.StyleTypeSequenceNumber,
                IsCompleted = e.IsCompleted.ToString() 
            }).ToArray();

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                total = Math.Ceiling((Decimal)recordCount / (Decimal)rows),
                page = page,
                records = recordCount,
                rows = final
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveStyle(StyleModel styleModel, List<StyleColorModel> styleColorModel)
        {
            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

            styleModelList.styleModel.StyleSequenceNumber = styleModel.StyleSequenceNumber;
            styleModelList.styleModel.StyleName = styleModel.StyleName;
            styleModelList.styleModel.StyleDescription = styleModel.StyleDescription;
            styleModelList.styleModel.StyleTypeSequenceNumber = styleModel.StyleTypeSequenceNumber;
            styleModelList.styleModel.IsCompleted = styleModel.IsCompleted;
            styleModelList.styleModel.Mode = styleModel.Mode;

            styleModelList.styleModel.StyleColorModelList = styleColorModel;


            foreach (long selectedSize in styleModel.SelectedComboSizeList)
            {
                if (styleModelList.styleModel.StyleSizeModelList.Where(x => x.SizeSequenceNumber == selectedSize).Count() > 0)
                {
                    continue;
                }
                StyleSizeModel styleSizeModel = new StyleSizeModel();
                styleSizeModel.SequenceNumber = 0;
                styleSizeModel.StyleSequenceNumber = 0;
                styleSizeModel.SizeSequenceNumber = selectedSize;
                styleSizeModel.SizeName = "";
                styleSizeModel.IsDeleted = false;
                styleSizeModel.State = EnumConstants.ModelCurrentState.Added;
                styleModelList.styleModel.StyleSizeModelList.Add(styleSizeModel);
            }

            foreach (long selectedFabric in styleModel.SelectedFabricList)
            {
                if (styleModelList.styleModel.StyleFabricModelList.Where(x => x.SourcesSequenceNumber == selectedFabric).Count() > 0)
                {
                    continue;
                }
                StyleFabricModel styleFabricModel = new StyleFabricModel();
                styleFabricModel.SequenceNumber = 0;
                styleFabricModel.StyleSequenceNumber = 0;
                styleFabricModel.SourcesSequenceNumber = selectedFabric;
                styleFabricModel.IsDeleted = false;
                styleFabricModel.State = EnumConstants.ModelCurrentState.Added;
                styleModelList.styleModel.StyleFabricModelList.Add(styleFabricModel);
            }
 
            styleModelList = merchandiserServiceClient.SaveStyle(styleModelList.styleModel);
            ViewBag.SizeDropDownList = styleModelList.SizeDropDownList;
            ViewBag.ColorDropDownList = styleModelList.ColorDropDownList;
            ViewBag.ProcessDropDownList = styleModelList.ProcessDropDownList;
            ViewBag.ProductGroupDropDownList = styleModelList.ProductGroupDropDownList;
            ViewBag.StyleTypeDropDownList = styleModelList.StyleTypeDropDownList;
            ViewBag.FabricDropDownList = styleModelList.FabricDropDownList;

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success = true ,
                data = "Saved Successfully" 
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePanel(StylePanelModel panelModel, List<StylePanelColorModel> stylePanelColorModel, List<StylePanelProcessModel> stylePanelProcessModel)
        {
            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

            if (styleModelList.styleModel.StylePanelModelList.Where(x => x.TempGuid == panelModel.TempGuid).Count() > 0)
            {
                styleModelList.styleModel.StylePanelModelList.RemoveAll(x => x.TempGuid == panelModel.TempGuid);
            }


            if (stylePanelColorModel != null)
                panelModel.StylePanelColorModelList.AddRange(stylePanelColorModel);

            if (stylePanelProcessModel != null)
                panelModel.StylePanelProcessModelList.AddRange(stylePanelProcessModel);

            styleModelList.styleModel.StylePanelModelList.Add(panelModel);

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success =  true,
                 data = new
                {
                    stylePanelModel = styleModelList.styleModel.StylePanelModelList 
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveProcessSources(StyleProcessSourcesModel processSourcesModel, List<StyleProccessSourcesColorModel> processSourcesColorModel)
        {
            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

            if (styleModelList.styleModel.StyleProcessSourcesModelList.Where(x => x.ProcessSourcesTempGuid == processSourcesModel.ProcessSourcesTempGuid).Count() > 0)
            {
                styleModelList.styleModel.StyleProcessSourcesModelList.RemoveAll(x => x.ProcessSourcesTempGuid == processSourcesModel.ProcessSourcesTempGuid);
            }


            if (processSourcesColorModel != null)
                processSourcesModel.StyleProccessSourcesColorModelList.AddRange(processSourcesColorModel);

            styleModelList.styleModel.StyleProcessSourcesModelList.Add(processSourcesModel);

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success = true,
                data = new
                {
                    styleProcessSourcesModel = styleModelList.styleModel.StyleProcessSourcesModelList
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPanel(string TempGuid)
        {

            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

 
            var stylePanelList = styleModelList.styleModel.StylePanelModelList.AsQueryable();

            StylePanelModel stylePanelModel = new StylePanelModel();

            if ((TempGuid != null) && (TempGuid != ""))
            {
                stylePanelModel = stylePanelList.Where(f => f.TempGuid == TempGuid).FirstOrDefault();
            }

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success = true,
                data = new
                {
                    stylePanelModel = stylePanelModel,
                    SelectedPanelProcessList = stylePanelModel.SelectedPanelProcessList,
                    StylePanelColorModelList = stylePanelModel.StylePanelColorModelList
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditStyle(long styleSequenceNumber)
        {
            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];
            
            styleModelList.styleModel = merchandiserServiceClient.EditStyle(styleSequenceNumber);

            TempData["styleModelList"] = styleModelList;
            return Json(new
            {
                success = true,
                data = new
                {
                    StyleModel = styleModelList.styleModel,
                    StylePanelModelList = styleModelList.styleModel.StylePanelModelList,
                    StyleProcessSourcesModelList = styleModelList.styleModel.StyleProcessSourcesModelList 
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditProcessSources(string processSourcesTempGuid)
        {
            StyleListModel styleModelList = new StyleListModel();

            styleModelList = (StyleListModel)TempData["styleModelList"];

            var styleProcessSourcesList = styleModelList.styleModel.StyleProcessSourcesModelList.AsQueryable();

            StyleProcessSourcesModel styleProcessSourcesModel = new StyleProcessSourcesModel();

            if ((processSourcesTempGuid != null) && (processSourcesTempGuid != ""))
            {
                styleProcessSourcesModel = styleProcessSourcesList.Where(f => f.ProcessSourcesTempGuid == processSourcesTempGuid).FirstOrDefault();
            }

            TempData["styleModelList"] = styleModelList;

            return Json(new
            {
                success = true,
                data = new
                {
                    styleProcessSourcesModel = styleProcessSourcesModel,
                    styleProccessSourcesColorModelList = styleProcessSourcesModel.StyleProccessSourcesColorModelList 
                }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
