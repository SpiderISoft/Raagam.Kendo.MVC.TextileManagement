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

    }
}
