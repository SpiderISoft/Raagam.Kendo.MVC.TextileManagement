using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raagam.TextileManagement.CommonUtility
{
    public static class EnumConstants
    {
        public enum ModelCurrentState
        {
            UnChanged,
            Updated,
            Added,
            Deleted
        }


        public enum SaveStatus
        {
            Saved,
            NotSaved
        }
    }
}
