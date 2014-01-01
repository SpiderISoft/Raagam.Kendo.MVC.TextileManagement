using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface IStyleDataAccess
    {

        StyleModel PopulateDropDown(StyleModel styleModel);
 
        
    }
}
