using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.DataAccess 
{
    public interface ISelectProductGridDataAccess
    {
        #region ISelectProductGridDataAccess Members

        List<SelectProductGridModel> SelectProductGrid(long SupplierSequenceNumber,string ProductName);

        #endregion
    }
}
