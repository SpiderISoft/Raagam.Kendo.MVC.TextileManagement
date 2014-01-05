using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.MVC.TextileManagement.UI.MerchandiserService;
using Raagam.TextileManagement.Model;

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
            StyleModel styleModel = new StyleModel();
            styleModel = merchandiserServiceClient.StylePopulateDropDown(styleModel);
            ViewBag.ColorDropDownList = styleModel.ColorDropDownList;
            ViewBag.ProcessDropDownList = styleModel.ProcessDropDownList;
            ViewBag.ProductGroupDropDownList = styleModel.ProductGroupDropDownList;
            TempData["styleModel"] = styleModel;
            return View(url, styleModel);
        }

        public JsonResult GetColors()
        {
             
            StyleModel styleModel = new StyleModel();

            styleModel = (StyleModel)TempData["styleModel"];

           TempData["styleModel"] = styleModel;


           return Json(new
           {
               success = true,
               data = new
               {
                   ColorDropDownList = styleModel.ColorDropDownList,
 
               }
           }, JsonRequestBehavior.AllowGet);
 
        }

        public JsonResult GetStyleColor(int page = 1, int rows = 10, string sord = "asc", string sidx = "Id")
        {
            StyleModel styleModel = new StyleModel();

            styleModel = (StyleModel) TempData["styleModel"];

            var recordCount = styleModel.StyleColorModelList.Count;
            IEnumerable<object> final;

            final = styleModel.StyleColorModelList.Select(e => new
            {
                SequenceNumber = e.SequenceNumber,
                StyleSequenceNumber = e.StyleSequenceNumber,
                ColorSequenceNumber = e.ColorSequenceNumber,
                ColorName = e.ColorName,
                ColorPantone = e.ColorPantone,
                IsDeleted = e.IsDeleted.ToString(),
                State = e.State.ToString()
            }).ToArray();

            TempData["styleModel"] = styleModel;

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
            StyleModel styleModel = new StyleModel();

            styleModel = (StyleModel)TempData["styleModel"];

            var recordCount = styleModel.StylePanelModelList.Count;
            IEnumerable<object> final;

            final = styleModel.StylePanelModelList.Select(e => new
            {
                SequenceNumber = e.SequenceNumber,
                StyleSequenceNumber = e.StyleSequenceNumber,
                PanelName = e.PanelName,
                PanelDescription = e.PanelDescription,
                IsDeleted = e.IsDeleted.ToString(),
                State = e.State.ToString(),
                TempGuid = e.TempGuid.ToString()
            }).ToArray();

            TempData["styleModel"] = styleModel;

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
            return null;
        }

        public ActionResult SavePanel(StylePanelModel panelModel, List<StylePanelColorModel> stylePanelColorModel, List<StylePanelProcessModel> stylePanelProcessModel)
        {
            StyleModel styleModel = new StyleModel();

            styleModel = (StyleModel)TempData["styleModel"];

            if (styleModel.StylePanelModelList.Where(x => x.TempGuid == panelModel.TempGuid).Count() > 0)
            {
                styleModel.StylePanelModelList.RemoveAll(x => x.TempGuid == panelModel.TempGuid);
            }


            if (stylePanelColorModel != null)
                panelModel.StylePanelColorModelList.AddRange(stylePanelColorModel);

            if (stylePanelProcessModel != null)
                panelModel.StylePanelProcessModelList.AddRange(stylePanelProcessModel);

            styleModel.StylePanelModelList.Add(panelModel);

            TempData["styleModel"] = styleModel;

            return Json(new
            {
                success =  true,
                 data = new
                {
                    stylePanelModel = styleModel.StylePanelModelList 
                }
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditPanel(string TempGuid)
        {
            StyleModel styleModel = new StyleModel();

            styleModel = (StyleModel)TempData["styleModel"];

            var stylePanelList = styleModel.StylePanelModelList.AsQueryable();

            StylePanelModel stylePanelModel = new StylePanelModel();

            if ((TempGuid != null) && (TempGuid != ""))
            {
                stylePanelModel = stylePanelList.Where(f => f.TempGuid == TempGuid).FirstOrDefault();
            }

            TempData["styleModel"] = styleModel;

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
    }
}
