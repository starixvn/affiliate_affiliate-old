namespace Fanex.Web.LiveBet365.Customer.Handler
{
    using System;
    using System.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using System.Text;
    using System.Data;
    using DNAide.Data;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Culture;

    public class GetClickStatisticsList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string trackingcode = context.Request.Params["params"];
                context.Response.Write(Fanex.Affiliate.Core.Customer.GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(trackingcode));

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