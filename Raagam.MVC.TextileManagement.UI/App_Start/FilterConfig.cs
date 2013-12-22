using System.Web;
using System.Web.Mvc;

namespace Raagam.MVC.TextileManagement.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}