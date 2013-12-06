using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Web.LiveBet365.AffLink
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Link",
                url: "{AffId}/{PageId}/{Lang}/index.htm",
                defaults: new { controller = "Home", action = "Link", AffId = UrlParameter.Optional, PageId = UrlParameter.Optional, Lang = UrlParameter.Optional }
            );


        }
    }
}