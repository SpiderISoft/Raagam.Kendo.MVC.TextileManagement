using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class StyleBusiness : IStyleBusiness
    {
         IStyleDataAccess _styleDataAccess;

         public StyleBusiness()
        {
            _styleDataAccess = new StyleDataAccess();
        }

         public StyleModel PopulateDropDown(StyleModel styleModel)
        {
            return _styleDataAccess.PopulateDropDown(styleModel);
        }

 
    }
}
