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
            TempData["styleModel"] = styleModel;
            return View(url, styleModel);
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
                ColorName = e.ColorName.ToString(),
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
        public ActionResult SaveStyle(StyleModel styleModel, List<StyleColorModel> styleColorModel)
        {
            return null;
        }
    }
}
