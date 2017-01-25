using System.Web;
using System.Web.Mvc;

namespace Asp.net_MVC_TestpreparationAppDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute()); //added https filter in 1/25/2017 update

        }
    }
}
