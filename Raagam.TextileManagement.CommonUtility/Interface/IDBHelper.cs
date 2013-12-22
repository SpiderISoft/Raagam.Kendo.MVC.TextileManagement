using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raagam.TextileManagement.CommonUtility
{
    public interface IDBHelper
    {
        #region IDBHelper Members

        void AddInParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, object value);
        void AddOutParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, int size);
        void AddParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, System.Data.ParameterDirection direction, string sourceColumn, System.Data.DataRowVersion sourceVersion, object value);
        int ExecuteNonQuery(System.Data.Common.DbCommand command);
        object ExecuteScalar(System.Data.Common.DbCommand command);
        System.Data.IDataReader ExecuteReader(System.Data.Common.DbCommand command);
        System.Data.Common.DbCommand GetStoredProcCommand(string storedProcedureName);
        System.Data.DataSet ExecuteDataSet(System.Data.Common.DbCommand command);

        #endregion IDBHelper Members
    }
}
