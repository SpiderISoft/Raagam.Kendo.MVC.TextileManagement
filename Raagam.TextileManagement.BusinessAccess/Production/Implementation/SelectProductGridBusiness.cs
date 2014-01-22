using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.BusinessAccess 
{
    public class SelectProductGridBusiness : ISelectProductGridBusiness
    {

        ISelectProductGridDataAccess _selectProductGridDataAccess;

        public SelectProductGridBusiness()
        {
            _selectProductGridDataAccess = new SelectProductGridDataAccess();
        }

        #region ISelectProductGridBusiness Members

        public List<SelectProductGridModel> SelectProductGrid(long SupplierSequenceNumber, string ProductName)
        {
            return _selectProductGridDataAccess.SelectProductGrid(SupplierSequenceNumber, ProductName);
        }

         
        #endregion
    }
}
