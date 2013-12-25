using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.MVC.TextileManagement.UI.MerchandiserService;
using Raagam.TextileManagement.Model;
using System.Data;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

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
            TempData["orderMainModel"] = orderMainModel;
            return View(url, orderMainModel);
        }

        public ActionResult SelectOrderColorSize(long StyleSequenceNumber)
        {
            StringWriter SW = new StringWriter();
            string jResult = string.Empty;
            OrderMainModel orderMainModel = new OrderMainModel();
            OrderMainModel orderServiceMainModel = new OrderMainModel();
            orderMainModel = (OrderMainModel) TempData["orderMainModel"];

            orderServiceMainModel = merchandiserServiceClient.SelectColorSize(StyleSequenceNumber);

            DataTable dcm = new DataTable("colModel");
            dcm.Columns.Add("name");
            dcm.Columns.Add("index");
            dcm.Columns.Add("editable",typeof(bool));
            dcm.Columns.Add("width");
            //dcm.Columns.Add("editoptions");
            dcm.Columns.Add("sorttype");

            DataRow drColorSequenceRow = dcm.NewRow();
            drColorSequenceRow["name"] = "StyleColorSequence";
            drColorSequenceRow["index"] = "StyleColorSequence";
            drColorSequenceRow["editable"] = false;
            drColorSequenceRow["width"] = 0;
            drColorSequenceRow["sorttype"] = "'int'";
            dcm.Rows.Add(drColorSequenceRow);

            DataRow drColorRow = dcm.NewRow();
            drColorRow["name"] = "ColorName";
            drColorRow["index"] = "ColorName";
            drColorRow["editable"] = false;
            drColorRow["width"] = 200;
            dcm.Rows.Add(drColorRow);

            List<string> ColNamesList = new List<string>();
            ColNamesList.Add("StyleColorSequence");
            ColNamesList.Add("ColorName");


            for (int i = 0; i < orderServiceMainModel.StyleSizeList.Count; i++)
            {
                DataRow drSizeRow = dcm.NewRow();
                drSizeRow["name"] = orderServiceMainModel.StyleSizeList[i].SizeName;
                drSizeRow["index"] = orderServiceMainModel.StyleSizeList[i].SizeName;
                drSizeRow["editable"] = true;
                drSizeRow["width"] = 100;
                //drSizeRow["editoptions"] = "{size:'20',maxlength:'30'}";

                dcm.Rows.Add(drSizeRow);
                ColNamesList.Add(orderServiceMainModel.StyleSizeList[i].SizeName);
            }

            orderMainModel.StyleColorList = orderServiceMainModel.StyleColorList;

            dcm.WriteXml(SW);
            string Con = SW.ToString();
            XmlDocument XDoc = new XmlDocument();
            XDoc.LoadXml(Con);
            if (dcm.Columns.Count > 0)
            {
                jResult = JsonConvert.SerializeXmlNode(XDoc.DocumentElement);
            }

            TempData["orderMainModel"] = orderMainModel;
            return Json(new
            {
                success = true,
                data = new
                {
                    styleColorList = orderMainModel.StyleColorList ,
                    ColModel = jResult.ToString(),
                    ColNames = ColNamesList.ToArray()

                }
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
