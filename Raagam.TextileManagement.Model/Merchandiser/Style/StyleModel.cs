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
        public List<SelectListItem> StyleTypeDropDownList { get; set; }


        [DataMember]
        public List<SelectListItem> StyleDropDownList { get; set; }

        [DataMember]
        public EnumConstants.ScreenMode Mode { get; set; }
 
    }
}
 