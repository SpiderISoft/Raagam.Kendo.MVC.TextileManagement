using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public interface IStyleBusiness
    {
        StyleModel PopulateDropDown(StyleModel styleModel);
    }
}
