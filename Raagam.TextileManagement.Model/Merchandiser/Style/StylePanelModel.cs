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
            State = CommonUtility.EnumConstants.ModelCurrentState.Added;
            IsDeleted = false;
            StylePanelColorModelList = new List<StylePanelColorModel>();
            PanelColorModel = new StylePanelColorModel();

            StylePanelProcessModelList = new List<StylePanelProcessModel>();
            SelectedPanelProcessList = new List<long>();
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
        public List<StylePanelProcessModel> StylePanelProcessModelList { get; set; }




        [DataMember]
        public List<long> SelectedPanelProcessList { get; set; }
 
 

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
    }
}
