using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StylePanelColorModel
    {

        public StylePanelColorModel()
        {
            ColorPantone = "";
            IsDeleted = false;
            ColorDropDownList = new List<SelectListItem>();
        }



        [DataMember]
        public List<SelectListItem> ColorDropDownList { get; set; }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StylePanelSequenceNumber { get; set; }

        [DataMember]
        public long StyleColorSequenceNumber { get; set; }

        [DataMember]
        public string ColorSequenceNumber { get; set; }

        [DataMember]
        public string ColorPantone { get; set; }

        [DataMember]
        public string TempGuid { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }
 
        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
    }
}
