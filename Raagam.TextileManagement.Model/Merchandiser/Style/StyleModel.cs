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
    public class StyleModel
    {

        public StyleModel()
        {
            StyleName = "";
            StyleDescription = "";
            IsCompleted = false;
            Mode = EnumConstants.ScreenMode.New;
            StyleTypeDropDownList = new List<SelectListItem>();
            StyleDropDownList = new List<SelectListItem>();
            StyleColorModelList = new List<StyleColorModel>();
            ColorDropDownList = new List<SelectListItem>();
            StyleSizeModelList = new List<StyleSizeModel>();
            SizeDropDownList = new List<SelectListItem>();
            SelectedComboSizeList = new List<long>();
            
            StylePanelModelList = new List<StylePanelModel>();
            PanelModel = new StylePanelModel();
        }

        [DataMember]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public string StyleName { get; set; }

        [DataMember]
        public string StyleDescription { get; set; }

        [DataMember]
        public long StyleTypeSequenceNumber { get; set; }

        [DataMember]
        public byte[] StyleImage { get; set; }

        [DataMember]
        public bool IsCompleted { get; set; }

      
        [DataMember]
        public List<SelectListItem> ColorDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> SizeDropDownList { get; set; }

        
        [DataMember]
        public List<long> SelectedComboSizeList { get; set; }


        [DataMember]
        public List<StyleColorModel> StyleColorModelList { get; set; }

        [DataMember]
        public List<StyleSizeModel> StyleSizeModelList { get; set; }

        [DataMember]
        public List<StylePanelModel> StylePanelModelList { get; set; }

        [DataMember]
        public StylePanelModel PanelModel  { get; set; }

        [DataMember]
        public List<SelectListItem> StyleTypeDropDownList { get; set; }

        [DataMember]
        public List<SelectListItem> StyleDropDownList { get; set; }

        [DataMember]
        public EnumConstants.ScreenMode Mode { get; set; }
 
    }
}
 