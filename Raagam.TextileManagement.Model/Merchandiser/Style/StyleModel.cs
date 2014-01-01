using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleModel
    {
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
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
 
    }
}
 