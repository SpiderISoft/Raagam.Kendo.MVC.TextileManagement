using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public interface IStyleBusiness
    {
        StyleListModel PopulateDropDown();
        StyleListModel SaveStyle(StyleModel styleModel);
        StyleModel EditStyle(long styleSequenceNumber);
    }
}
