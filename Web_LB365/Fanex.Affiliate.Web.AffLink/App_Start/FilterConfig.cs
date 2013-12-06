using System.Web;
using System.Web.Mvc;

namespace Fanex.Web.LiveBet365.AffLink
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}