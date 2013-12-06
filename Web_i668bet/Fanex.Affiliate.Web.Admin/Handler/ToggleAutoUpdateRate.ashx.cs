namespace Fanex.Web.i668bet.Admin.Handler
{
    using System.Web;
    using Fanex.Affiliate.DataObjects;

    public class ToggleAutoUpdateRate : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = int.Parse(context.Request["id"]);
            context.Response.Write(ExchangeRate.ToggleAutoUpdateRate(id));
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
