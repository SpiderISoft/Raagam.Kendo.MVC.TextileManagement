using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Raagam.TextileManagement.CommonUtility;
using System.Web;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleModel
    {

        public StyleModel()
        {
            StyleName = "";
            StyleDescription = "";
            StyleTypeName = "";
            IsCompleted = false;
            Mode = EnumConstants.ScreenMode.New;
            
            

            StyleColorModelList = new List<StyleColorModel>();
            StyleSizeModelList = new List<StyleSizeModel>();

            SelectedComboSizeList = new List<long>();
            
            StylePanelModelList = new List<StylePanelModel>();

            PanelModel = new StylePanelModel();

            ProcessSourcesModel = new StyleProcessSourcesModel();
            StyleProcessSourcesModelList = new List<StyleProcessSourcesModel>();

            StyleFabricModelList = new List<StyleFabricModel>();
            SelectedFabricList = new List<long>();

            SelectedComboColorList = new List<long>();

            StyleImage = new byte[0];
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
        public List<long> SelectedComboSizeList { get; set; }

        [DataMember]
        public List<long> SelectedComboColorList { get; set; }
 

        [DataMember]
        public List<StyleColorModel> StyleColorModelList { get; set; }

        [DataMember]
        public List<StyleSizeModel> StyleSizeModelList { get; set; }

        [DataMember]
        public List<StylePanelModel> StylePanelModelList { get; set; }

        [DataMember]
        public StylePanelModel PanelModel  { get; set; }

        [DataMember]
        public StyleProcessSourcesModel ProcessSourcesModel { get; set; }

        [DataMember]
        public List<StyleProcessSourcesModel> StyleProcessSourcesModelList { get; set; }

        [DataMember]
        public List<StyleFabricModel> StyleFabricModelList { get; set; }

        [DataMember]
        public string StyleTypeName { get; set; }

        [DataMember]
        public List<long> SelectedFabricList { get; set; }

        [DataMember]
        public EnumConstants.ScreenMode Mode { get; set; }
 
    }
}
 