namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Admin;

    public class ToggleLockedOut : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string affList = context.Request["AffList"];
            if (string.IsNullOrEmpty(affList))
            {
                affList = AdminSession.AffId.ToString();
            }

            context.Response.Write(Affiliate.UnLockedOut(affList, Utilities.GetIPAddressOfClient()));
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
