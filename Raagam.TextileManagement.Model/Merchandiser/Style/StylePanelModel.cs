using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StylePanelModel
    {

        public StylePanelModel()
        {
            PanelName = "";
            PanelDescription = "";
            TempGuid = Guid.NewGuid().ToString();
            IsDeleted = false;
            StylePanelColorModelList = new List<StylePanelColorModel>();
            PanelColorModel = new StylePanelColorModel();
        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public string PanelName { get; set; }

        [DataMember]
        public string PanelDescription { get; set; }

        [DataMember]
        public string TempGuid { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }


        [DataMember]
        public StylePanelColorModel PanelColorModel { get; set; }

        [DataMember]
        public List<StylePanelColorModel> StylePanelColorModelList { get; set; }
 

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
    }
}
