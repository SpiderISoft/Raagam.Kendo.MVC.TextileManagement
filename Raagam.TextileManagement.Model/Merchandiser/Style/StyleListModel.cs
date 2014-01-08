using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleListModel
    {

        public StyleListModel()
        {
            styleModelList = new List<StyleModel>();
            styleModel = new StyleModel();
            StyleTypeDropDownList = new List<SelectListItem>();
            StyleDropDownList = new List<SelectListItem>();
            SizeDropDownList = new List<SelectListItem>();
            ColorDropDownList = new List<SelectListItem>();
            ProcessDropDownList = new List<SelectListItem>();
            ProductGroupDropDownList = new List<SelectListItem>();
            ProductDropDownList = new List<LinkDropDownModel>();
            LotTypeDropDownList = new List<LinkDropDownModel>();
            FabricDropDownList = new List<SelectListItem>();
            
        }
        [DataMember]
        public List<StyleModel> styleModelList { get; set; }

        [DataMember]
        public StyleModel styleModel { get; set; }


        [DataMember]
        public List<SelectListItem> ColorDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> SizeDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> ProcessDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> StyleTypeDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> StyleDropDownList { get; set; }


        [DataMember]
        public List<SelectListItem> ProductGroupDropDownList { get; set; }

        [DataMember]
        public List<LinkDropDownModel> ProductDropDownList { get; set; }

        [DataMember]
        public List<LinkDropDownModel> LotTypeDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> FabricDropDownList { get; set; }
    }
}
 