using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raagam.MVC.TextileManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult HomePage()
        {
            string url = "~/Views/Common/Default.cshtml";
            return View(url);
        }

    }
}
