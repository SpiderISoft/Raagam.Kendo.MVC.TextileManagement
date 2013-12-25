using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface IOrderDataAccess
    {

        OrderMainModel PopulateDropDown(OrderMainModel orderMainModel);
        
    }
}
