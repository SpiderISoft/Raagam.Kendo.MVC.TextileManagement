using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;

namespace Raagam.TextileManagement.CommonUtility
{
    public static class UtilityConstants
    {
        public const string DatabaseSectionName = "dataConfiguration";
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["TextileManagementConnection"].ToString();

      
    }
}
