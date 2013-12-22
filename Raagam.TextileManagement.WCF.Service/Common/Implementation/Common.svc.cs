using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Raagam.TextileManagement.BusinessAccess;
using Raagam.TextileManagement.Model;

namespace Raagam.TextileManagement.WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Common" in code, svc and config file together.
    public class Common : ICommon
    {

        #region ICommon Members

        ILoginBusiness _loginBusiness;

        public LoginModel CheckLogin(LoginModel loginModel)
        {
            _loginBusiness = new LoginBusiness();
            return _loginBusiness.CheckLogin(loginModel);
        }

        #endregion
    }
}
