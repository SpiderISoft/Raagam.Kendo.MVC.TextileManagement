using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleProccessSourcesColorModel
    {

        public StyleProccessSourcesColorModel()
        {
            StyleColorName = "";
            ColorPantone = "";
            IsDeleted = false;
            State = CommonUtility.EnumConstants.ModelCurrentState.Added;
        }
 

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StyleProcessSourcesSequenceNumber { get; set; }

        [DataMember]
        public long StyleColorSequenceNumber { get; set; }

        [DataMember]
        public string StyleColorName { get; set; }

        [DataMember]
        public long ColorSequenceNumber { get; set; }

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
