using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.CommonUtility;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Configuration;


namespace Raagam.TextileManagement.CommonUtility
{
    public class DBHelper : IDBHelper
    {
            #region Variables Declarations
            private Database _database;
            private const string DATABASE_SECTION_NAME = UtilityConstants.DatabaseSectionName;
            #endregion

            #region Constructors

            public DBHelper()
            {
                _database = DatabaseFactory.CreateDatabase();
            }

            public DBHelper(string connectionStringName)
            {
                _database = DatabaseFactory.CreateDatabase(connectionStringName);

            }

            #endregion

            #region DBHelper Methods

            #region GetStoredProcCommand

            public virtual DbCommand GetStoredProcCommand(string storedProcedureName)
            {
                
                DbCommand cmd = _database.GetStoredProcCommand(storedProcedureName);

                if (ConfigurationManager.AppSettings.Get("CommandTimeout") != null)
                {
                    int value;
                    if (int.TryParse(ConfigurationManager.AppSettings.Get("CommandTimeout"), out value))
                    {
                        cmd.CommandTimeout = value;
                    }
                }

                return cmd;
            }


            #endregion

            #region Add Parameter

            public void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction,
                string sourceColumn, DataRowVersion sourceVersion, object value)
            {
                _database.AddParameter(command, name, dbType, direction, sourceColumn, sourceVersion, value);
            }

            public void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
            {
                _database.AddOutParameter(command, name, dbType, size);
            }

            public void AddInParameter(DbCommand command, string name, DbType dbType)
            {
                _database.AddInParameter(command, name, dbType);
            }

            public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
            {
                _database.AddInParameter(command, name, dbType, value);
            }

            public void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn,
                DataRowVersion sourceVersion)
            {
                _database.AddInParameter(command, name, dbType, sourceColumn, sourceVersion);
            }

            #endregion

            #region ExecuteScalar

            public virtual object ExecuteScalar(DbCommand command)
            {
                object returnvalue = -1;
                for (int i = 0; i <= ciMaxRetryCount; i++)
                {
                    try
                    {
                        returnvalue = _database.ExecuteScalar(command);
                        return returnvalue;
                    }
                    catch (SqlException ex)
                    {
                        if ((!IsSqlClusterException(ex.Number)) || (i >= ciMaxRetryCount))
                            throw;
                        else
                            System.Threading.Thread.Sleep(15000);
                    }
                }
                return returnvalue;
            }

            #endregion

            #region Cluster Awared retry

            const int ciMaxRetryCount = 3;

            private bool IsSqlClusterException(int errornum)
            {
                return ((errornum == 4060) || (errornum == 10060) || (errornum == 121) || (errornum == 10054));
            }


            #endregion

            #region ExecuteNonQuery

            public virtual int ExecuteNonQuery(DbCommand command)
            {
                int returnvalue = -1;
                for (int i = 0; i <= ciMaxRetryCount; i++)
                {
                    try
                    {
                        returnvalue = _database.ExecuteNonQuery(command);
                        return returnvalue;
                    }
                    catch (SqlException ex)
                    {
                        if ((!IsSqlClusterException(ex.Number)) || (i >= ciMaxRetryCount))
                            throw;
                        else
                            System.Threading.Thread.Sleep(15000);
                    }
                }
                return returnvalue;
            }

            #endregion

            #region ExecuteReader

            public virtual IDataReader ExecuteReader(DbCommand command)
            {
                IDataReader returnvalue = null;
                for (int i = 0; i <= ciMaxRetryCount; i++)
                {
                    try
                    {
                        returnvalue = _database.ExecuteReader(command);
                        return returnvalue;
                    }
                    catch (SqlException ex)
                    {
                        if ((!IsSqlClusterException(ex.Number)) || (i >= ciMaxRetryCount))
                            throw;
                        else
                            System.Threading.Thread.Sleep(15000);
                    }
                }
                return returnvalue;
            }

            public virtual DataSet ExecuteDataSet(DbCommand command)
            {
                DataSet returnvalue = null;

                try
                {
                    returnvalue = _database.ExecuteDataSet(command);
                    return returnvalue;
                }
                catch (SqlException ex)
                {
                    throw ex;

                }

            }

            #endregion

            #endregion DBHelper Methods
        }
}
