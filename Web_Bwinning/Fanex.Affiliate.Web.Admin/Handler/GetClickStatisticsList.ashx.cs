namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core.Admin;
    public class GetClickStatisticsList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string trackingcode = context.Request.Params["params"];
                context.Response.Write(Fanex.Affiliate.Core.Admin.GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(AdminSession.AffId, trackingcode));

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
