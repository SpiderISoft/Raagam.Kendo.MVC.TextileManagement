using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.MVC.TextileManagement.UI.MerchandiserService;
using Raagam.TextileManagement.Model;

namespace Raagam.MVC.TextileManagement.UI.Controllers
{
    public class OrderController : Controller
    {
        MerchandiserClient merchandiserServiceClient;

        public OrderController()
        {
            merchandiserServiceClient = new MerchandiserClient();
        }


        public ActionResult Order()
        {
            string url = "~/Views/Merchandiser/Order.cshtml";
            OrderMainModel orderMainModel = new OrderMainModel();
            orderMainModel = merchandiserServiceClient.PopulateDropDown(orderMainModel);
            return View(url, orderMainModel);
        }

    }
}
