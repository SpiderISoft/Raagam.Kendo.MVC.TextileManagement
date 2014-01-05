using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class StyleProcessSourcesModel
    {

        public StyleProcessSourcesModel()
        {
            IsDeleted = false;
            IsSizeApplicable = false;
            State = CommonUtility.EnumConstants.ModelCurrentState.Added;
        }


        [DataMember]
        public long SequenceNumber { get; set; }

        [DataMember]
        public long StyleSequenceNumber { get; set; }

        [DataMember]
        public long ProductGroupSequenceNumber { get; set; }

        [DataMember]
        public long SourcesSequenceNumber { get; set; }

        [DataMember]
        public long ProcessSequenceNumber { get; set; }

        [DataMember]
        public long LotSequenceNumber { get; set; }
        
        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public string TempGuid { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public bool IsSizeApplicable { get; set; }

        [DataMember]
        public Raagam.TextileManagement.CommonUtility.EnumConstants.ModelCurrentState State { get; set; }
    }
}
