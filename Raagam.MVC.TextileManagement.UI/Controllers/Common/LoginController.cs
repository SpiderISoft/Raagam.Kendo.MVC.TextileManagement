using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raagam.TextileManagement.Model;
using Raagam.MVC.TextileManagement.UI.CommonService;

namespace Raagam.MVC.TextileManagement.UI.Controllers
{
    public class LoginController : Controller
    {
    
        CommonClient commonServiceClient;

        public LoginController()
        {
            commonServiceClient = new CommonClient();
        }

        public ActionResult Login()
        {
            string url = "~/Views/Common/Login.cshtml";
            return View(url);
        }

        public ActionResult CheckLogin(string LoginName,string Password)
        {
            string url = "~/Views/Common/Default.cshtml";
            LoginModel _loginModel = new LoginModel();

            
            
            _loginModel.LoginName = LoginName.ToUpper();
            _loginModel.Password = Password.ToUpper();
            _loginModel = commonServiceClient.CheckLogin(_loginModel);

            if (_loginModel.IsLoginSuccess)
            {
                return View(url);
            }
            else
            {
                throw new Exception("Invalid User Name / Password!!! Please Enter it Correctly");
            }

        }

    }
}
