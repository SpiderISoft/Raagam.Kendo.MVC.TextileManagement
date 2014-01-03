using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleSizeModel
    {

        public StyleSizeModel()
        {
            IsDeleted = false;

        }

        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StyleSequenceNumber { get; set; }


        [DataMember]
        public long SizeSequenceNumber { get; set; }

        [DataMember]
        public string SizeName { get; set; }


        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
    }
}
