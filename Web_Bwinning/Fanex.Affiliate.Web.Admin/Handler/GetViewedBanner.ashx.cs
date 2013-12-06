namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Core.Culture;

    public class GetViewedBanner : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                int bannerID = Convert.ToInt32(context.Request["bannerid"].Trim());
                int noRow = Convert.ToInt32(context.Request["noRow"].Trim());
                context.Response.Write(Fanex.Affiliate.Core.Admin.GenerateHTML.Banner.GetViewedBanner(bannerID, noRow));
            }

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
