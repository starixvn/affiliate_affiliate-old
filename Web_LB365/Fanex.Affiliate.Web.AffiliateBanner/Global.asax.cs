using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fanex.Affiliate.Core;

namespace Fanex.Web.LiveBet365.AffiliateBanner
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError().GetBaseException();
           
            if (ex is HttpException)
            {
                return;
            }

            string message = ex.Message +
                            "<br />SOURCE: " + ex.Source +
                            "<br />FORM: " + Request.Form.ToString() +
                            "<br />QUERYSTRING: " + Request.QueryString.ToString() +
                            "<br />TARGETSITE: " + ex.TargetSite +
                            "<br />STACKTRACE: " + ex.StackTrace;

            if (Common.CacheContainsException(message))
            {
                return;
            }

            try
            {
                AppCommon.SendEmail(message, AppSettings.ToMail, AppSettings.MailErrorTitle, AppSettings.MailErrorTitle,
                AppSettings.BCCMail,AppSettings.CCMail    );
            }
            catch (Exception ex1)
            {
                AppCommon.WriteErrorLog(ex.Message, ex.StackTrace, Fanex.Affiliate.Core.AppSettings.SiteID);
                AppCommon.WriteErrorLog(ex1.Message, ex1.StackTrace, Fanex.Affiliate.Core.AppSettings.SiteID);
            }
        }
    }
}