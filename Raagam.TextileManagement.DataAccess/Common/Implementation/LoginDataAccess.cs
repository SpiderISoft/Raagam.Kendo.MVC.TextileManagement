using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.Model;
using Raagam.TextileManagement.CommonUtility;
using System.Data.Common;
using System.Data;

namespace Raagam.TextileManagement.DataAccess
{
    public class LoginDataAccess : ILoginDataAccess
    {

         IDBHelper _dbHelper;

         public LoginDataAccess()
        {
            _dbHelper = new DBHelper();
        }


        #region ILoginDataAccess Members

        public LoginModel CheckLogin(LoginModel loginModel)
        {
            loginModel.IsLoginSuccess = false;

            using (DbCommand loginCommand = _dbHelper.GetStoredProcCommand("VerifyLogin"))
            {
                _dbHelper.AddInParameter(loginCommand, "@LoginName",
                                     DbType.String, loginModel.LoginName);
                _dbHelper.AddInParameter(loginCommand, "@LoginPassword",
                                     DbType.String, loginModel.Password);

                using (IDataReader loginDetailsDataReader = _dbHelper.ExecuteReader(loginCommand))
                {
                    while (loginDetailsDataReader.Read())
                    {
                        if (loginDetailsDataReader["FirstName"].ToString() != "")
                        {
                            loginModel.FirstName = loginDetailsDataReader["FirstName"].ToString();
                            loginModel.ProcessID = Convert.ToInt64(loginDetailsDataReader["ProcessID"]);
                            loginModel.LoginID = Convert.ToInt64(loginDetailsDataReader["LoginID"]);
                            loginModel.Process = loginDetailsDataReader["Process"].ToString();
                            loginModel.IsAdmin = Convert.ToBoolean(loginDetailsDataReader["IsAdmin"]);
                            loginModel.IsLoginSuccess = true;
                        }
                    }
                }
            }

            return loginModel;
        }

        #endregion
    }
}
