using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raagam.TextileManagement.DataAccess;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.BusinessAccess
{
    public class LoginBusiness : ILoginBusiness 
    {

        ILoginDataAccess _loginDataAccess;

        public LoginBusiness()
        {
            _loginDataAccess = new LoginDataAccess();
        }

        #region ILoginBusiness Members

        public LoginModel CheckLogin(LoginModel loginModel)
        {
            return _loginDataAccess.CheckLogin(loginModel);
        }

        #endregion
    }
}
