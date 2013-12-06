using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.Core;
using System.Configuration;

namespace Fanex.Web.LiveBet365.AffLink
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

            AppCommon.SendEmail(message, ConfigurationManager.AppSettings["toMail"], ConfigurationManager.AppSettings["MailErrorTitle"], ConfigurationManager.AppSettings["MailErrorTitle"]);
            //DNAide.Web.DNAideLog.Write(message.Replace("<br />", "\r\n"));
        }   
    }
}