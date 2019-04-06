using System.Web;
using System.Web.Mvc;

namespace BlogSitesiMVC_Proje_YaseminGoktas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
