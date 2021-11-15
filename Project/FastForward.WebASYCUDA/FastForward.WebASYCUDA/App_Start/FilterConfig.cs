using System.Web;
using System.Web.Mvc;

namespace FastForward.WebASYCUDA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
