using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.CommonUtility;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class StyleBusiness : IStyleBusiness
    {
         IStyleDataAccess _styleDataAccess;

         public StyleBusiness()
        {
            _styleDataAccess = new StyleDataAccess();
        }

         public StyleListModel PopulateDropDown()
        {
            return _styleDataAccess.PopulateDropDown();
        }

         public StyleListModel SaveStyle(StyleModel styleModel)
         {
             return _styleDataAccess.SaveStyle(styleModel);
         }

         public StyleModel EditStyle(long styleSequenceNumber)
         {
             return _styleDataAccess.EditStyle(styleSequenceNumber);
         }
    }
}
