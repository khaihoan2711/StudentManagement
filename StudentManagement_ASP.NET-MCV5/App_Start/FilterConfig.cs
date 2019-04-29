using System.Web;
using System.Web.Mvc;

namespace StudentManagement_ASP.NET_MCV5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
