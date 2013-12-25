using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Raagam.TextileManagement.CommonUtility
{
    public interface IDBHelper
    {
        #region IDBHelper Members

        void AddInParameter(DbCommand command, string name, DbType dbType, object value);
        void AddOutParameter(DbCommand command, string name, DbType dbType, int size);
        void AddParameter(DbCommand command, string name, DbType dbType, System.Data.ParameterDirection direction, string sourceColumn, System.Data.DataRowVersion sourceVersion, object value);
        void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn,DataRowVersion sourceVersion);
        int ExecuteNonQuery(DbCommand command);
        object ExecuteScalar(DbCommand command);
        IDataReader ExecuteReader(DbCommand command);
        DbCommand GetStoredProcCommand(string storedProcedureName);
        DataSet ExecuteDataSet(DbCommand command);
        int Fill(DbCommand command, DataTable dataTable);

        #endregion IDBHelper Members
    }
}
