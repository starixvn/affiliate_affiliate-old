using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Fanex.Web.LiveBet365.AffiliateBanner
{
    public class Common
    {
        public static string FullDomainName
        {
            get
            {
                NameValueCollection variables = HttpContext.Current.Request.ServerVariables;

                string protocol=HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://";
                string domain = variables["SERVER_NAME"];
                string port = variables["SERVER_PORT"];
                var fulldomain = protocol + domain;
                if (!ConfigurationManager.AppSettings["Environment"].ToString().Equals("Production", StringComparison.OrdinalIgnoreCase))
                    fulldomain = fulldomain + ":" + port;
                fulldomain = fulldomain + (HttpRuntime.AppDomainAppVirtualPath == "/" ? string.Empty : HttpRuntime.AppDomainAppVirtualPath);
                return fulldomain;
            }
        }

        public static bool CacheContainsException(String message)
        {
            var key = message.GetHashCode().ToString(); //GetUniqueCode(message);
            if (CacheFactory.GetCacheManager().Contains(key))
                return true;
            CacheFactory.GetCacheManager().Add(key, null,CacheItemPriority.None,null, new AbsoluteTime(TimeSpan.FromDays(1)));
            return false;
        }              
    }
}