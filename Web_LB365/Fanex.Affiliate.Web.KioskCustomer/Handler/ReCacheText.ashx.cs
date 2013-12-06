namespace Fanex.Web.LiveBet365.Affiliate.Handler
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Utility;

    public class ReCacheText : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            IList<string> urlReferers = Common.GetStringIList(System.Configuration.ConfigurationManager.AppSettings["Recache.UrlReferer"], ",");

            CacheFactory.GetCacheManager().Remove("e946bd02-81b4-4f91-a9ab-e433cf93f59d");

            context.Response.Write("ok");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}