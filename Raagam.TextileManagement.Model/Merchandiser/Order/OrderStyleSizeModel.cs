using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model 
{
    [DataContract]
    public class OrderStyleSizeModel
    {
        public OrderStyleSizeModel()
        {
            SizeName = "";
        }
        
        [DataMember]
        public long StyleSizeSequence { get; set; }

        [DataMember]
        public string SizeName { get; set; }
    }
}
