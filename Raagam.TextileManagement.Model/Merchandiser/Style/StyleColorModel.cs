using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raagam.TextileManagement.Model 
{

    public class StyleColorModel
    {

        public long StyleColorSequenceNumber { get; set; }
        public long StyleSequenceNumber { get; set; }
        public long ColorSequenceNumber { get; set; }
        public string ColorPantone { get; set; }
        public bool IsDeleted { get; set; }
    }
}
