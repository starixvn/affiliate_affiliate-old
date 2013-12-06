namespace Fanex.Web.Bwinning.Admin.Handler
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

            context.Response.Write(Affiliate.UnLockedOut(affList, Utilities.GetIPAddressOfClient(), Fanex.Affiliate.Core.AppSession.Language));
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
