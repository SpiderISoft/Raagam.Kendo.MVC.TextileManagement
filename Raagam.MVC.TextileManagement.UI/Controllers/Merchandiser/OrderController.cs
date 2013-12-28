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
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Web.Script.Serialization;


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

            DataRow drColorSequenceRow = dcm.NewRow();
            drColorSequenceRow["name"] = "StyleColorSequence";
            drColorSequenceRow["index"] = "StyleColorSequence";  
            drColorSequenceRow["editable"] = false;
            drColorSequenceRow["width"] = 0;
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
                drSizeRow["name"] = "Col_" + orderServiceMainModel.StyleSizeList[i].SizeName;
                drSizeRow["index"] =  orderServiceMainModel.StyleSizeList[i].StyleSizeSequence; 
                drSizeRow["editable"] = true;
                drSizeRow["width"] = 100;
                 

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

        [HttpPost]
        public ActionResult GetOrderDetails(long OrderNumber)
        {

            StringWriter SW = new StringWriter();
            string jResult = string.Empty;
            OrderMainModel orderMainModel = new OrderMainModel();
            orderMainModel = (OrderMainModel)TempData["orderMainModel"];
            orderMainModel = merchandiserServiceClient.GetOrderDetails(OrderNumber);

            
            
            DataTable dcm = new DataTable("colModel");
            dcm.Columns.Add("name");
            dcm.Columns.Add("index");
            dcm.Columns.Add("editable", typeof(bool));
            dcm.Columns.Add("width");

            DataRow drColorSequenceRow = dcm.NewRow();
            drColorSequenceRow["name"] = "StyleColorSequence";
            drColorSequenceRow["index"] = "StyleColorSequence";
            drColorSequenceRow["editable"] = false;
            drColorSequenceRow["width"] = 0;
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


            for (int i = 0; i < orderMainModel.StyleSizeList.Count; i++)
            {
                DataRow drSizeRow = dcm.NewRow();
                drSizeRow["name"] = "Col_" + orderMainModel.StyleSizeList[i].SizeName;
                drSizeRow["index"] = orderMainModel.StyleSizeList[i].StyleSizeSequence;
                drSizeRow["editable"] = true;
                drSizeRow["width"] = 100;


                dcm.Rows.Add(drSizeRow);
                ColNamesList.Add(orderMainModel.StyleSizeList[i].SizeName);
            }

            orderMainModel.StyleColorList = orderMainModel.StyleColorList;

            dcm.WriteXml(SW);
            string Con = SW.ToString();
            XmlDocument XDoc = new XmlDocument();
            XDoc.LoadXml(Con);
            if (dcm.Columns.Count > 0)
            {
                jResult = JsonConvert.SerializeXmlNode(XDoc.DocumentElement);
            }
            orderMainModel.Mode = Raagam.TextileManagement.CommonUtility.EnumConstants.ScreenMode.Edit;

            TempData["orderMainModel"] = orderMainModel;
            return Json(new
            {
                success = true,
                data = new
                {
                    Mode = orderMainModel.Mode.ToString(),
                    OrderDate = orderMainModel.OrderDate.ToString("dd/MM/yyyy"),
                    DeliveryDate = orderMainModel.DeliveryDate.ToString("dd/MM/yyyy"),
                    BuyerReferenceNumber = orderMainModel.BuyerReferenceNumber,
                    BuyerSequenceNumber = orderMainModel.BuyerSequenceNumber,
                    ExcessPercentage = orderMainModel.ExcessPercentage,
                    OrderQuantity = orderMainModel.OrderQuantity,
                    ExcessQuantity = orderMainModel.ExcessQuantity,
                    StyleSequenceNumber = orderMainModel.StyleSequenceNumber,
                    PackingType = orderMainModel.PackingType,
                    PackingInstructions = orderMainModel.PackingInstructions,
                    AssortmentDetails = orderMainModel.AssortmentDetails,
                    Comments = orderMainModel.Comments,
                    IsCompleted = orderMainModel.IsCompleted,
                    OrderDetailModelList = orderMainModel.OrderDetailModelList,
                    styleColorList = orderMainModel.StyleColorList,
                    ColModel = jResult.ToString(),
                    ColNames = ColNamesList.ToArray()

                }
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveOrder(OrderMainModel orderMainModel, List<OrderDetailModel> orderDetailModel)
        {
            orderMainModel.OrderDetailModelList = orderDetailModel;
            long OrderNumber = merchandiserServiceClient.SaveOrder(orderMainModel);
            return Json(new
            {
                success = true,
                data = new
                {
                   OrderNumber = OrderNumber
                }
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
