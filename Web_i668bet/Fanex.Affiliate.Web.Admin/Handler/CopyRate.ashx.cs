namespace Fanex.Web.i668bet.Admin.Handler
{
    using System.Web;
    using Fanex.Affiliate.DataObjects;

    public class CopyRate : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string ids = context.Request["ids"];
            context.Response.Write(ExchangeRate.UpdateRatesFromMarket(ids));
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
