using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Raagam.TextileManagement.Model
{
    [DataContract]
    public class LoginModel
    {
        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public long ProcessID { get; set; }

        [DataMember]
        public long LoginID { get; set; }

        [DataMember]
        public string Process { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsLoginSuccess { get; set; }


    }
}
