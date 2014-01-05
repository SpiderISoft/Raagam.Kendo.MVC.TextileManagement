using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleFabricModel
    {

        public StyleFabricModel()
        {
             
            IsDeleted = false;
            State = CommonUtility.EnumConstants.ModelCurrentState.Added;
        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StyleSequenceNumber { get; set; }


        [DataMember]
        public long SourcesSequenceNumber { get; set; }


        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }

    }
}
