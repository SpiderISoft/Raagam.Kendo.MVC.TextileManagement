using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StylePanelProcessModel
    {

        public StylePanelProcessModel()
        {
             
            IsDeleted = false;
            State = CommonUtility.EnumConstants.ModelCurrentState.Added;
        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StylePanelSequenceNumber { get; set; }


        [DataMember]
        public long ProcessSequenceNumber { get; set; }

        
        [DataMember]
        public string TempGuid { get; set; }


        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }

    }
}
